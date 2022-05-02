using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTakeDamage : MonoBehaviour
{

    public static event Action OnPlayerDeath;

    public float health = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float amount){
        health -= amount;

        if(health <= 0){
            health = 0;

            OnPlayerDeath?.Invoke();
        }
    }
}
