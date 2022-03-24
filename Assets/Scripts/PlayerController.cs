using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float defaultSpeed = 0.1f;
    [SerializeField] private float fallSpeed = 1;
    private float speed;
    private float horizontalInput;
    private float verticalInput;
    private float jumpInput;
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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        jumpInput = Input.GetAxisRaw("Jump");
    }

    void FixedUpdate() {
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
            move += new Vector3(horizontalInput, 0, verticalInput) * speed;

        controller.Move(move);
    }
}
