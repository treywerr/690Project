using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Transform headPos;
    private Transform crouchPos;
    private Transform camPos;

    public float defaultSpeed = 5f;
    public float gravity = 9.8f;

    private float speed;
    private float horizontalInput;
    private float verticalInput;
    private bool crouch = false;
    private bool sprint = false;

    private Vector3 velocity; // Store velocity vector for gravity

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        headPos = this.gameObject.transform.GetChild(2);
        crouchPos = this.gameObject.transform.GetChild(1);
        camPos = this.gameObject.transform.GetChild(0);
        speed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        /* Get the movement inputs */
        horizontalInput = InputWrapper.GetMovement("Horizontal");
        verticalInput = InputWrapper.GetMovement("Vertical");
        crouch = InputWrapper.GetAxis("Crouch", InputWrapper.InputStates.Gameplay) == 1f;
        sprint = InputWrapper.GetAxis("Sprint", InputWrapper.InputStates.Gameplay) == 1f;

        /* Construct movement vector */

        velocity.x = 0;
        velocity.z = 0;

        if (crouch)
        {
            speed = defaultSpeed * 0.5f;
            //transform.localScale = new Vector3(1, 0.5f, 1); // shorten the player
            camPos.localPosition = crouchPos.localPosition;
        }
        else
        {
            //transform.localScale = new Vector3(1, 1, 1); // make player normal height when not crouching
            camPos.localPosition = headPos.localPosition;

            if (sprint)
                speed = defaultSpeed * 1.5f;
            else
                speed = defaultSpeed;
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
    }
}
