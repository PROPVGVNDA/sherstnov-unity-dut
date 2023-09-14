using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float moveSpeed = 10.0f;
    private const float jumpForce = 15.0f;
    private const int playerLayer = 7;
    private Rigidbody2D rb;
    private PolygonCollider2D collider;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = IsGrounded();
        Debug.Log("IsGrounded: " + isGrounded);
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = moveDirection;

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        float extraHeightText = .03f;
        int layerMask = ~(1 << playerLayer);
        RaycastHit2D ray = Physics2D.Raycast(collider.bounds.center, Vector2.down, collider.bounds.extents.y + extraHeightText, layerMask);
        Color rayColor = ray.collider != null ? Color.green : Color.red;
        Debug.DrawRay(collider.bounds.center, Vector2.down * (collider.bounds.extents.y + extraHeightText), rayColor);
        return ray.collider != null;
    }
}
