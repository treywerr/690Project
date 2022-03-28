using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemy : MonoBehaviour
{

    public GameObject player;

    public float Distance;

    public bool ENEMYSPOTTED;


    public NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(player.transform.position, this.transform.position);

        if(Distance <= 20){
            ENEMYSPOTTED = true;
        }
        if(Distance > 20f){
            ENEMYSPOTTED = false;
        }

        if(ENEMYSPOTTED){
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
        if(!ENEMYSPOTTED){
            agent.isStopped = true;
        }
    }
}
