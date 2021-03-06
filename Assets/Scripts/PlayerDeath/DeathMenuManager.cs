using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject PperImage;

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
        Cursor.lockState = CursorLockMode.Confined;

        FindObjectOfType<SoundManager>().whenDead();
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<SoundManager>().whenAlive();
        InputWrapper.ChangeState(InputWrapper.InputStates.Gameplay);
        
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene("MainMenu");
        FindObjectOfType<SoundManager>().whenAlive();
        InputWrapper.ChangeState(InputWrapper.InputStates.Gameplay);
    }

    public void EnablePepper(){
        PperImage.SetActive(true);
        gameOverMenu.SetActive(false);
    }

    public void DisablePepper(){
        PperImage.SetActive(false);
        gameOverMenu.SetActive(true);
    }
}
