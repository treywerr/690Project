using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    public float defaultSpeed = 5f;
    public float gravity = 9.8f;

    private float speed;
    private float horizontalInput;
    private float verticalInput;
    private bool crouch = false;
    private bool sprint = false;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        /* Get the movement inputs */
        horizontalInput = InputWrapper.GetMovement("Horizontal");
        verticalInput = InputWrapper.GetMovement("Vertical");
        crouch = InputWrapper.GetAxis("Crouch", InputWrapper.InputStates.Gameplay) == 1f;
        sprint = InputWrapper.GetAxis("Shift", InputWrapper.InputStates.Gameplay) == 1f;

        /* Construct movement vector */

        if (!controller.isGrounded)
            velocity.y -= gravity * Time.deltaTime;
        else
        {
            velocity = Vector3.zero;
            velocity.y = -2f; // make the player stick to the ground better
        }

        if (crouch)
        {
            speed = defaultSpeed * 0.5f;
            transform.localScale = new Vector3(1, 0.5f, 1); // shorten the player
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // make player normal height when not crouching

            if (sprint)
                speed = defaultSpeed * 1.5f;
            else
                speed = defaultSpeed;
        }

        if (horizontalInput != 0 || verticalInput != 0)
            velocity = (transform.right * horizontalInput + transform.forward * verticalInput) * speed;

        /* Execute movement */
        controller.Move(velocity * Time.deltaTime);
    }
}
