using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickGame : MonoBehaviour
{
    [SerializeField] AudioSource neutralClick;
    [SerializeField] AudioSource correctClick;
    [SerializeField] AudioSource failClick;
    [SerializeField] float timeBetweenClicks;
    int lockLength = 10;
    int correctIndex;
    int currentIndex;
    float timeSinceStart;
    bool playingGame = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
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
        //For now, consider this a menu submit or something, figure out what actually want later
        if(InputWrapper.GetMenuSubmit() == 1f)
        {
            if(currentIndex == correctIndex)
            {
                correctClick.Play();
                ExitGame(true);
                return;
            }
            failClick.Play();
            ExitGame(false);
            return;
        }
        if(oldIndex != currentIndex)
        {
            neutralClick.Play();
        }
    }

    public void StartLockGame()
    {
        //RandomizeGame();
        correctIndex = Random.Range(2, lockLength);
        playingGame = true;
        timeSinceStart = 0f;
        currentIndex = 0;
        //Begin Animation
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
        //Pass on info about whether game was success or fail
        return;
    }
}
