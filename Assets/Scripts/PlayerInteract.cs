using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script detects when the player is looking at an object. 
/// If the player is, show some prompt (tbd) and if they press the interact key, activate the thing they look at.
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    [SerializeField] GameObject interactPrompt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(InputWrapper.currentState == InputWrapper.InputStates.Gameplay)
        {
            CastRay();
        }
    }

    void CastRay()
    {
        interactPrompt.SetActive(false);
        Vector3 direction = transform.rotation * Vector3.forward;
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(transform.position, direction, out hitInfo, 5f);
        if(hit)
        {
            PlayerInteractionEffect effect = hitInfo.collider.gameObject.GetComponent<PlayerInteractionEffect>();
            if (effect && InputWrapper.GetAxis("Interact", InputWrapper.InputStates.Gameplay) == 1f)
            {
                effect.ActivateEffect();
            }
            else if(effect)
            {
                interactPrompt.SetActive(true);
            }
        }
        Debug.DrawRay(transform.position, direction * 5f);
    }
}
