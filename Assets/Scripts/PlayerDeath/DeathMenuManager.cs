using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour
{
    public GameObject gameOverMenu;

    private void OnEnable(){
        PlayerTakeDamage.OnPlayerDeath += EnableGameOverMenu;
    }

    private void OnDisable(){
        PlayerTakeDamage.OnPlayerDeath -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu(){
        gameOverMenu.SetActive(true);
        InputWrapper.ChangeState(InputWrapper.InputStates.Menus);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartLevel(){

        Debug.Log("I got clicked!");
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
