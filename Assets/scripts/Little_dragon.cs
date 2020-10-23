using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;

public class Little_dragon : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;

    private float horizontalInput;
    private float verticalInput;
    private bool jumpInput;
    private bool attackInput;

    private readonly float RUN_SPEED = 5;
    private readonly float JUMP_STRENGTH = 15;
    private bool isGrounded = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

   
    void Update()
    {
        GetInput();
        GroundCheck();
        Run();
        Jump();
        Animations();
        Flip();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetKey(KeyCode.K);
        attackInput = Input.GetKey(KeyCode.J);
    }
    private void Run()
    {
        rb2d.velocity = new Vector2(horizontalInput * RUN_SPEED, rb2d.velocity.y);
    }
    private void Jump()
    {
        if (!isGrounded)
            return;
        if (jumpInput)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, JUMP_STRENGTH);
        }
    }
    private void GroundCheck()
    {
        LayerMask layermask = 1 << LayerMask.NameToLayer("solids");

        RaycastHit2D[] rays = new RaycastHit2D[3];
        rays[0] = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, layermask);
        rays[1] = Physics2D.Raycast(transform.position + new Vector3(-0.5f,0), Vector2.down, 1.1f, layermask);
        rays[2] = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0), Vector2.down, 1.1f, layermask);


        for (int i=0; i < rays.Length; i++)
        {
            if (rays[i].collider)
            {
                if (rays[i].normal.y > 0.9f)
                {
                    isGrounded = true;
                    //if (!jumpInput)
                       // canJump = true;
                    return;
                }
            }
        }
        isGrounded = false;
    }
    private void Animations()
    {
        animator.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));
    }

    private void Flip()
    {
        if (rb2d.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb2d.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
