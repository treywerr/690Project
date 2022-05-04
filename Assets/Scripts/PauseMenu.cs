using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        InputWrapper.ChangeState(InputWrapper.InputStates.Menus);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //FindObjectOfType<SoundManager>().whenAlive();
        InputWrapper.ChangeState(InputWrapper.InputStates.Gameplay);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        FindObjectOfType<SoundManager>().whenAlive();
        InputWrapper.ChangeState(InputWrapper.InputStates.Gameplay);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }
}
