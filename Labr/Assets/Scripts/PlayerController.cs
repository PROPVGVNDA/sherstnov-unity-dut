using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float moveSpeed = 5.0f;
    private const float jumpForce = 12.0f;
    private const int playerLayer = 7;
    private Rigidbody2D rb;
    private new CircleCollider2D collider;
    private bool isGrounded;
    private Animator animator;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Fall: " + animator.GetBool("IsFalling"));
        isGrounded = IsGrounded();
        //Debug.Log("IsGrounded: " + isGrounded);
        float horizontalInput = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        Vector2 moveDirection = new(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = moveDirection;

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
        }

        if (rb.velocity.y < 0)
        {
            animator.SetBool("IsFalling", true); // Set IsFalling to true when player is falling
            animator.SetBool("IsJumping", false); // Ensure IsJumping is false when falling
        }
        else if (isGrounded)
        {
            animator.SetBool("IsFalling", false); // Set IsFalling to false when player is grounded
        }

        if (horizontalInput > 0 && !facingRight) { Flip(); }
        else if (horizontalInput < 0 && facingRight) { Flip(); }
    }

    private bool IsGrounded()
    {
        float extraHeightText = .03f;
        int layerMask = ~(1 << playerLayer);
        RaycastHit2D ray = Physics2D.Raycast(collider.bounds.center, Vector2.down, collider.bounds.extents.y + extraHeightText, layerMask);
        Color rayColor = ray.collider != null ? Color.green : Color.red;
        //Debug.DrawRay(collider.bounds.center, Vector2.down * (collider.bounds.extents.y + extraHeightText), rayColor);
        return ray.collider != null;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}