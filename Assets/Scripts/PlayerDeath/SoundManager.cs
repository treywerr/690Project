using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource liveMusic;
    public AudioSource deathMusic;

    public bool liveSound;
    public bool deathSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenAlive(){
        liveSound = true;
        deathSound = false;
        liveMusic.Play();
    }

    public void whenDead(){
        if(liveMusic.isPlaying){
            liveSound = false;
            liveMusic.Stop();
        }

        if(!deathMusic.isPlaying && deathSound == false){
            deathMusic.Play();
            deathSound = true;
        }
    }

}
