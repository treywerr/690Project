using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    public string scene; // The scene to transfer to

    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            Debug.Log("Portaled");
            InputWrapper.ChangeState(InputWrapper.InputStates.Cutscene);
            SceneManager.LoadScene(scene);
        }
    }
    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("Collided");
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Portaled");
            InputWrapper.ChangeState(InputWrapper.InputStates.Cutscene);
            SceneManager.LoadScene(scene);
        }
    }
}
