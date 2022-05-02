using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public Transform headPos;
    public Transform crouchPos;
    public Transform camPos;

    public float defaultSpeed = 5f;
    public float gravity = 9.8f;

    private float speed;
    private float horizontalInput;
    private float verticalInput;
    private bool crouch = false;
    private bool sprint = false;

    public AudioSource source;
    public AudioClip walkSound;
    public AudioClip sprintSound;
    public AudioClip crouchSound;
    private bool moving = false;

    private float previousSpeed;
    private Coroutine soundRoutine;

    private Vector3 velocity; // Store velocity vector for gravity

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = defaultSpeed;
        previousSpeed = defaultSpeed;
        if (Respawner.getLocalSpawn() == null)
        {
            Respawner.setLocalSpawn(GameObject.Find("SpawnPoint").transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* Get the movement inputs */
        horizontalInput = InputWrapper.GetAxis("Horizontal", InputWrapper.InputStates.Gameplay);
        verticalInput = InputWrapper.GetAxis("Vertical", InputWrapper.InputStates.Gameplay);
        crouch = InputWrapper.GetAxis("Crouch", InputWrapper.InputStates.Gameplay) == 1f;
        sprint = InputWrapper.GetAxis("Sprint", InputWrapper.InputStates.Gameplay) == 1f;

        /* Construct movement vector */

        velocity.x = 0;
        velocity.z = 0;

        if (crouch)
        {
            speed = defaultSpeed * 0.5f;
            //It4Enemy.hearMult = .5f;
            StealthController.Request(.5f, 0);
            // shorten the player
            if (camPos.localPosition != crouchPos.localPosition)
                camPos.localPosition = crouchPos.localPosition;
        }
        else
        {
            // make player normal height when not crouching
            if (camPos.localPosition != headPos.localPosition)
                camPos.localPosition = headPos.localPosition;

            if (sprint)
            {
                speed = defaultSpeed * 1.5f;
            }
            else
            {
                speed = defaultSpeed;
                //It4Enemy.hearMult = 1f;
                StealthController.Request(1f, 0);
            }
        }

        if (horizontalInput != 0 || verticalInput != 0)
        {
            float y = velocity.y;
            velocity = (transform.right * horizontalInput + transform.forward * verticalInput) * speed;
            velocity.y = y;
        }

        if (!controller.isGrounded)
            velocity.y -= gravity * Time.deltaTime;
        else
        {
            velocity.y = -2f; // make the player stick to the ground better
        }

        /* Execute movement */
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            if (velocity.x != 0 || velocity.z != 0)
            {
                moving = true;

                if (crouch)
                    source.clip = crouchSound;
                else if (sprint)
                {
                    source.clip = sprintSound;
                    //It4Enemy.hearMult = 1.5f; // Set multiplier here b/c it should only increase when player is sprinting
                    StealthController.Request(2f, 0);
                }
                else
                    source.clip = walkSound;

                if (previousSpeed != speed)
                {
                    StopAllCoroutines();
                    soundRoutine = StartCoroutine(PlayWalkNoise());
                }

            }
            else if (moving)
            {
                source.Stop();
                StopAllCoroutines();
                moving = false;
            }
        }
        previousSpeed = speed;
    }

    IEnumerator PlayWalkNoise()
    {
        yield return new WaitForSeconds(0.5f);
        source.Play();
    }
}
