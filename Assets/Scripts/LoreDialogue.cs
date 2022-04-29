using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Import this to quickly access Unity's UI classes
using VIDE_Data; //Import this to use VIDE Dialogue's VD class

public class LoreDialogue : MonoBehaviour
{

    public Text NPC_text; //References
    public Text[] PLAYER_text; //References
    public Text Item_Name; //References
    public Image Item_Sprite;
    public Image Background;
    //public KeyCode continueButton; //Button to continue

    private bool keyDown = false;
    private bool lastFrameDown = false;
    void Start()
    {
        //Disable UI when starting just in case
        NPC_text.gameObject.SetActive(false);
        Item_Name.gameObject.SetActive(false);
        Item_Sprite.gameObject.SetActive(false);
        Background.gameObject.SetActive(false);
        foreach (Text t in PLAYER_text)
            t.transform.parent.gameObject.SetActive(false);

        //Subscribe to some events and Begin the Dialogue
        //VD.OnNodeChange += UpdateUI;
        //VD.OnEnd += End;
        //VD.BeginDialogue(GetComponent<VIDE_Assign>()); //This is the first most important method when using VIDE
    }

    //Check if a dialogue is active and if we are NOT in a player node in order to continue
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.J))
        {
            BeginLore(0);
        }
        else if(Input.GetKeyDown(KeyCode.K))
        {
            BeginLore(1);
        }
        */
        if (VD.isActive)
        {
            if (!VD.nodeData.isPlayer && lastFrameDown && InputWrapper.GetAxis("Submit", InputWrapper.InputStates.Menus) == 0f)
            {
                if (keyDown)
                {
                    keyDown = false;
                }
                else
                {
                    VD.Next(); //Second most important method when using VIDE
                }
            }
        }
        else
        {
            if (lastFrameDown && InputWrapper.GetAxis("Submit", InputWrapper.InputStates.Menus) == 0f)
            {
                //Start();
            }
        }
        lastFrameDown = InputWrapper.GetAxis("Submit", InputWrapper.InputStates.Menus) == 1f;
    }

    //This method is called by the UI Buttons! Check their button component in the Inspector!
    public void SelectChoiceAndGoToNext(int playerChoice)
    {
        keyDown = true;
        VD.nodeData.commentIndex = playerChoice; //Setting this when on a player node will decide which node we go next
        VD.Next();
    }

    //Every time VD.nodeData is updated, this method will be called. (Because we subscribed it to OnNodeChange event)
    void UpdateUI(VD.NodeData data)
    {
        Debug.Log("Update UI called");
        WipeAll(); //Turn stuff off first

        if (!data.isPlayer) //For NPC. Activate text gameobject and set its text
        {
            NPC_text.gameObject.SetActive(true);
            NPC_text.text = data.comments[data.commentIndex];
            Item_Name.gameObject.SetActive(true);
            Item_Name.text = data.extraVars["Title"].ToString();
            Item_Sprite.gameObject.SetActive(true);
            Item_Sprite.sprite = data.sprite;
            Background.gameObject.SetActive(true);
        }
        else //For Player. It will activate the required Buttons and set their text
        {
            for (int i = 0; i < PLAYER_text.Length; i++)
            {
                if (i < data.comments.Length)
                {
                    PLAYER_text[i].transform.parent.gameObject.SetActive(true);
                    PLAYER_text[i].text = data.comments[i];
                }
                else
                {
                    PLAYER_text[i].transform.parent.gameObject.SetActive(false);
                }

                PLAYER_text[0].transform.parent.GetComponent<Button>().Select();
            }
        }
    }

    //Set all UI references off
    void WipeAll()
    {
        NPC_text.gameObject.SetActive(false);
        Item_Name.gameObject.SetActive(false);
        Item_Sprite.gameObject.SetActive(false);
        Background.gameObject.SetActive(false);
        foreach (Text t in PLAYER_text)
            t.transform.parent.gameObject.SetActive(false);
    }

    //This will be called when we reach the end of the dialogue.
    //Very important that this gets called before we call BeginDialogue again!
    void End(VD.NodeData data)
    {
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue(); //Third most important method when using VIDE     
        WipeAll();
        InputWrapper.ChangeState(InputWrapper.InputStates.Gameplay);
        Debug.Log("End called");
    }

    //Just in case something happens to this script
    void OnDisable()
    {
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
    }

    public void BeginLore(int loreIndex)
    {
        Debug.Log("Lore began");
        InputWrapper.ChangeState(InputWrapper.InputStates.Menus);
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        GetComponent<VIDE_Assign>().overrideStartNode = loreIndex;
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
    }
}
