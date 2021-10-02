using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    protected CharacterController2D controller;

    public float speed = 10f;
    public float gravityScale = -9.81f;

    public GameObject groundCheck;
    public float distToGround = 0.2f;
    public LayerMask groundMask;
    public float jumpHeight = 2f;

    Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, distToGround, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float x = Input.GetAxisRaw("Horizontal");

        Vector2 move = transform.right * x;

        controller.Move(move * speed * Time.deltaTime);

        if (!isGrounded)
        {
            velocity.y += gravityScale * Time.deltaTime;
        }


        controller.Move(velocity * Time.deltaTime);

        jump();
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) & isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityScale);
        }
    }
}
