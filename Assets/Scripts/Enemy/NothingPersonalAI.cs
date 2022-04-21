using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NothingPersonalAI : MonoBehaviour
{

    public GameObject player;

    public float Distance;

    public bool ENEMYSPOTTED;
    public bool touched;
    public AudioClip clip;

    public NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

 

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(player.transform.position, this.transform.position);

        if(Distance <= 15){
            ENEMYSPOTTED = true;
        }
        if(Distance > 15f){
            ENEMYSPOTTED = false;
        }

        if(ENEMYSPOTTED && touched == false){
            agent.isStopped = false;
            touched = true;
            agent.Warp(player.transform.position);
            AudioSource.PlayClipAtPoint(clip, player.transform.position);
        }
        if(!ENEMYSPOTTED){
            agent.isStopped = true;
        }  
    }
}
