using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float climbSpeed;
    public Rigidbody2D rb;

    private bool isJumping;
    private bool isGrounded;
    [HideInInspector]
    public bool isClimbing;

    public Transform GroudCheck;
    public float GroundCheckRadius;
    public LayerMask collisionMask;
    public CapsuleCollider2D capsuleCollider;

    public float jumpForce;
    public Animator animator;

    private Vector3 velocity = Vector3.zero;


    public SpriteRenderer spriteRenderer;
    public float horizontalMovement;
    public float verticalMovement;

    public static PlayerMovement instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une Instance de PlayerMovement");
            return;
        }

        instance = this;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing) 
        {
            isJumping = true;
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);


    }

    void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.deltaTime;

        isGrounded = Physics2D.OverlapCircle(GroudCheck.position, GroundCheckRadius, collisionMask);

        MovePlayer(horizontalMovement, verticalMovement);
    }

    private void Flip(float x)
    {
        if(x < -0.0001) 
        {
            spriteRenderer.flipX = true;
        }

        else if (x > 0.0001)
        {
            spriteRenderer.flipX = false;
        }
    }

    void MovePlayer(float horizontalMovement, float verticalMovement)
    {
        if(!isClimbing)
        {
            Vector3 targetVelocity = new Vector2(horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.005f);

            if (isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }

        else
        {
            Vector3 targetVelocity = new Vector2(0, verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.005f);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroudCheck.position, GroundCheckRadius); 

    }

    internal void freezeMovement()
    {
        Vector3 targetVelocity = new Vector3(0, 0, 0);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0f);
    }
}
