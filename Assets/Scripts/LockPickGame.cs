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
    }

    public void StartLockGame()
    {
        //RandomizeGame();
        correctIndex = Random.Range(2, lockLength);
        playingGame = true;
        timeSinceStart = 0f;
        currentIndex = 0;
        //Begin Animation
        gameCanvas.SetActive(true);
        pickAnimation = StartCoroutine(WigglePick());
        ambientNoises.loop = true;
        ambientNoises.Play();
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
        gameCanvas.SetActive(false);
        ambientNoises.Stop();
        playingGame = false;
        //Pass on info about whether game was success or fail
        return;
    }

    IEnumerator WigglePick()
    {
        float pos = rakingPick.rotation.eulerAngles.z;
        float speed = 0.5f;
        while(true)
        {
            while(pos <= -15f)
            {
                pos += speed;
                rakingPick.rotation = Quaternion.Euler(Vector3.forward * pos);
                yield return null;
            }
            while (pos >= -25f)
            {
                pos -= speed;
                rakingPick.rotation = Quaternion.Euler(Vector3.forward * pos);
                yield return null;
            }
        }
    }
}
