using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockPickGame : MonoBehaviour
{
    [SerializeField] AudioSource neutralClick;
    [SerializeField] AudioSource correctClick;
    [SerializeField] AudioSource failSound;
    [SerializeField] AudioSource successSound;
    [SerializeField] AudioSource ambientNoises;
    [SerializeField] float timeBetweenClicks;
    [SerializeField] RectTransform rakingPick;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] RectTransform shackle;
    Coroutine pickAnimation;
    int lockLength = 10;
    int correctIndex;
    int currentIndex;
    float timeSinceStart;
    bool playingGame = false;
    

    // Start is called before the first frame update
    void Start()
    {
        StartLockGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playingGame)
        {
            return;
        }
        timeSinceStart += Time.deltaTime;
        int oldIndex = currentIndex;
        currentIndex = (int)(timeSinceStart / timeBetweenClicks);
        Debug.Log("current index: " + currentIndex + "frame to click? = " + (currentIndex == correctIndex));
        //For now, consider this a menu submit or something, figure out what actually want later
        if(InputWrapper.GetMenuSubmit() == 1f)
        {
            if(currentIndex == correctIndex)
            {
                successSound.Play();
                ExitGame(true);
                return;
            }
            failSound.Play();
            ExitGame(false);
            return;
        }
        if(oldIndex != currentIndex)
        {
            if(currentIndex == correctIndex)
            {
                correctClick.Play();
            }
            else
            {
                neutralClick.Play();
            }
        }
        if(currentIndex > lockLength)
        {
            failSound.Play();
            ExitGame(false);
            return;
        }
    }

    public void StartLockGame()
    {
        //RandomizeGame();
        //move to different spot later?
        InputWrapper.ChangeState(InputWrapper.InputStates.Menus);


        correctIndex = Random.Range(2, lockLength);
        playingGame = true;
        timeSinceStart = 0f;
        currentIndex = 0;
        //Begin Animation
        gameCanvas.SetActive(true);
        pickAnimation = StartCoroutine(WigglePick());
        ambientNoises.loop = true;
        ambientNoises.PlayDelayed(0.5f);
        Debug.Log("Start game with index: " + correctIndex);
    }

    /*
    private void RandomizeGame()
    {
        correctIndex = Random.Range(2, lockLength);
    }
    */

    private void ExitGame(bool success)
    {
        //End Animation
        StopCoroutine(pickAnimation);

        if(success)
        {
            StartCoroutine(RaiseShackle());
        }
        else
        {
            StartCoroutine(FailTimeout());
        }
        
        ambientNoises.Stop();
        playingGame = false;
        //Pass on info about whether game was success or fail after shackle animation?
        return;
    }

    IEnumerator WigglePick()
    {
        float pos = rakingPick.rotation.eulerAngles.z;
        float speed = 1f;
        while(true)
        {
            while(pos <= 50f)
            {
                pos += speed;
                rakingPick.localRotation = Quaternion.Euler(Vector3.forward * pos);
                yield return null;
            }
            while (pos >= 20f)
            {
                pos -= speed;
                rakingPick.localRotation = Quaternion.Euler(Vector3.forward * pos);
                yield return null;
            }
        }
    }

    IEnumerator RaiseShackle()
    {
        Vector3 pos = shackle.anchoredPosition;
        float speed = 1f;
        while(pos.y <= -60f)
        {
            pos += speed * Vector3.up;
            shackle.anchoredPosition = pos;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        //Change to different spot later?
        InputWrapper.ChangeState(InputWrapper.InputStates.Gameplay);
        gameCanvas.SetActive(false);
    }

    IEnumerator FailTimeout()
    {
        yield return new WaitForSeconds(1.5f);
        InputWrapper.ChangeState(InputWrapper.InputStates.Gameplay);
        gameCanvas.SetActive(false);
    }
}
