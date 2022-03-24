using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float defaultSpeed = 5f;
    [SerializeField] private float fallSpeed = 1f;
    private float speed;
    private float horizontalInput;
    private float verticalInput;
    private bool crouch = false;
    private bool sprint = false;

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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        /* Construct movement vector */
        Vector3 move;

        if (!controller.isGrounded)
            move = Vector3.down * fallSpeed;
        else
            move = Vector3.zero;

        if (sprint)
            speed *= 1.5f;
        else if (crouch)
            speed *= 0.5f;
        else
            speed = defaultSpeed;

        if (horizontalInput != 0 || verticalInput != 0)
            move += (transform.right * horizontalInput + transform.forward * verticalInput) * speed * Time.deltaTime;

        /* Execute movement */
        controller.Move(move);
    }
}
