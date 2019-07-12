using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement: MonoBehaviour
{
    // float values
    public float moveSpeed;
    public float wallCheckRadius;
    public float groundCheckRadius;

    // boolean values
    public bool moveRight;
    public bool hittingWall;
    public bool notAtEdge;

    // Transform values
    public Transform wallCheck;
    public Transform groundCheck;

    // Layer values
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;


    private void Start()
    {
        
    }

    private void Update()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        notAtEdge = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        

        // Checks for wall or no ground
        if ( !notAtEdge || hittingWall )
        {
            moveRight = !moveRight;
        }

        if (moveRight)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }
}
