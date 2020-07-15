using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D myBody;
    private bool isJumping;
    public float jumpForce;

    public Transform DownCollsion;
    public LayerMask groundLayer;

    public float jumpTime;
    private float jumpTimeCounter;


    private Animator anim;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isJumping = false;

        jumpTimeCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!isJumping)
        {
            myBody.velocity = Vector2.up * jumpForce;
            isJumping = true;

            anim.Play("DinoIdle");

            jumpTimeCounter = jumpTime;

        }

        if (Input.GetKey(KeyCode.Space)&&jumpTimeCounter>0&&!isJumping)
        {
            myBody.velocity = Vector2.up * jumpForce;
        }

        if (Physics2D.Raycast(DownCollsion.position, Vector2.down, 0.05f, groundLayer))
        {
            isJumping = false;
            anim.Play("DinoRun");
        }

        jumpTimeCounter -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
    }
}
