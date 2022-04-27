using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class It4Enemy : MonoBehaviour {

    public GameObject PlayerMove;
    public bool playerIsInLOS = false;

    public float fieldOfViewAngle = 160f;
    public static float hearMult = 1f;
    public float hearingRadius = 4f;
    public float losRadius = 20f;

    private bool aiMemorizesPlayer = false;

    public float memoryStartTime = 10f;
    private float increasingMemoryTime;

    Vector3 noisePosition;

    private bool aiHeardPlayer = false;
    public float noiseTravelDistance = 50f;
    public float spinSpeed = 3f;
    private bool canSpin = false;
    private float isSpinningTime;
    public float spinTime = 3f;

    public Transform[] moveSpots; 
    private int randomSpot;

    private float waitTime; 
    public float startWaitTime = 1f;
    NavMeshAgent nav;

    //Ai strafe 
    public float distToPlayer = 5.0f; // straferadius
    private float randomStrafeStartTime; 
    private float waitStrafeTime;
    public float t_minStrafe;
    public float t_maxStrafe;

    public Transform strafeRight; 
    public Transform strafeLeft; 
    private int randomStrafeDir;
    //when to chase 
    public float chaseRadius = 20f;
    public float facePlayerFactor = 20f;

    // Audio
    public AudioSource source;
    public AudioClip alertStinger;
    public AudioClip[] alertSounds; // add an array of possible sounds for variety
    private bool playerSeen = false;
    public AudioClip caughtStinger;
    private bool playerCaught = false;

    private void Awake(){
        nav = GetComponent<NavMeshAgent>(); 
        nav.enabled = true;
    }

    void Start(){
        waitTime = startWaitTime; 
        randomSpot = Random.Range(0, moveSpots.Length); 
        randomStrafeDir = Random.Range(0, 2);
        if (PlayerMove == null)
        {
            PlayerMove = GameObject.Find("Player");
        }
    }


    void Update() {

        float distance = Vector3.Distance(PlayerMove.transform.position, transform.position);
        if(distance <= losRadius){
            CheckLOS();
        }

        if(nav.isActiveAndEnabled){
            if(playerIsInLOS == false && aiMemorizesPlayer == false && aiHeardPlayer == false){
                Patrol();

                NoiseCheck();

                StopCoroutine(AiMemory());

                playerSeen = false;
            }else if(aiHeardPlayer == true && playerIsInLOS == false && aiMemorizesPlayer == false){
                canSpin = true;
                GoToNoisePosition();

                playerSeen = false;
            }else if(playerIsInLOS == true){
                aiMemorizesPlayer = true;

                FacePlayer();

                if (!playerSeen)
                {
                    playerSeen = true;

                    AudioSource.PlayClipAtPoint(alertStinger,PlayerMove.transform.position);
                    AudioClip newClip = alertSounds[Random.Range(0, alertSounds.Length)];
                    while (source.clip == newClip) // ensure same sound isn't selected twice in a row
                        newClip = alertSounds[Random.Range(0, alertSounds.Length)];
                    source.clip = newClip;
                    source.Play();
                }

                ChasePlayer();
            }else if(aiMemorizesPlayer == true && playerIsInLOS == false){
                ChasePlayer();
                StartCoroutine(AiMemory());

                playerSeen = false;
            }
        }
    }

    void ChasePlayer(){
        float distance = Vector3.Distance(PlayerMove.transform.position, transform.position);

        if(distance <= chaseRadius && distance > distToPlayer){
            nav.SetDestination(PlayerMove.transform.position);
        }else if(nav.isActiveAndEnabled && distance <= distToPlayer){
            /* Kill Player */
            if (!playerCaught)
            {
                playerCaught = true;
                AudioSource.PlayClipAtPoint(caughtStinger, PlayerMove.transform.position);
            }

            /* Enemy Behavior */
            randomStrafeDir = Random.Range(0,2);
            randomStrafeStartTime = Random.Range(t_minStrafe, t_maxStrafe);

            if(waitStrafeTime <= 0){

                if(randomStrafeDir == 0){
                    nav.SetDestination(strafeLeft.position);
                }else if(randomStrafeDir == 1){
                    nav.SetDestination(strafeRight.position);
                }
                waitStrafeTime = randomStrafeStartTime;
            }else{
                waitStrafeTime -= Time.deltaTime;
            }
        }
    }

    void FacePlayer(){
        Vector3 direction = (PlayerMove.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*facePlayerFactor);
    }

    void Patrol(){
        nav.SetDestination(moveSpots[randomSpot].position);

        if(Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 2.0f){
            if(waitTime <= 0){
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }else{
                waitTime -= Time.deltaTime;
            }
        }
    }

    void NoiseCheck(){

        float distance = Vector3.Distance(PlayerMove.transform.position, transform.position);

        if (distance <= hearingRadius * hearMult){
            noisePosition = PlayerMove.transform.position;
            aiHeardPlayer = true;
        }
        else{
            aiHeardPlayer = false; 
            canSpin = false;
        }
    }

    void GoToNoisePosition(){
        nav.SetDestination(noisePosition);
        if (Vector3.Distance(transform. position, noisePosition) <= 5f && canSpin == true){
            isSpinningTime += Time.deltaTime;
            transform.Rotate (Vector3.up * spinSpeed, Space. World);
            if (isSpinningTime >= spinTime){
                canSpin = false; 
                aiHeardPlayer = false; 
                isSpinningTime = 0f;
            }

        }
        
    }



    IEnumerator AiMemory(){
        increasingMemoryTime = 0;
        while (increasingMemoryTime < memoryStartTime){
            increasingMemoryTime += Time.deltaTime; 
            aiMemorizesPlayer = true; 
            yield return null;

        }
        aiHeardPlayer = false; 
        aiMemorizesPlayer = false;
    }
    

    void CheckLOS(){
        Vector3 direction = PlayerMove.transform.position - transform.position;

        float angle = Vector3.Angle(direction,transform.forward);

        if(angle < fieldOfViewAngle * 0.5f){
            RaycastHit hit;

            if(Physics.Raycast(transform.position, direction.normalized, out hit, losRadius)){
                if(hit.collider.tag == "Player"){
                    playerIsInLOS = true;
                    aiMemorizesPlayer = true;
                }else{
                    playerIsInLOS = false;
                }
            }
        }
    }

}