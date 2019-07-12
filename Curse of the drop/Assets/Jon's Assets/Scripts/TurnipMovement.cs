using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnipMovement : MonoBehaviour
{
    public float defaultJumpInterval = 1.5f;
    public float maxSpeed = 6f;
    public float jumpHeight = 3.5f;
    public float highJumpHeight = 8.0f;
    public float superJumpInterval = .75f;
    public float fowardAccel = 10.0f;

    private float jumpTimer ;
    private float superJumpTimer;

    private bool targeting = false;
    private bool jumping = false;
    private bool superJumping = false;
    private bool grounded = false;
    private bool moving = true;

    private Transform currentTarget = null;

    public Transform groundCheckBox;
    public Transform wallCheckBox;
    public Transform highWallCheckBox;
    public LayerMask ground;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        jumpTimer = 0.0f;
        superJumpTimer = 0.0f;
        StopMoving();
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapBox(groundCheckBox.position, groundCheckBox.lossyScale, 0.0f, ground);
        anim.SetBool("isGrounded", grounded);

        if (grounded && GetComponent<Rigidbody2D>().velocity.y == 0.0f)
        {
            if (jumping)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, GetComponent<Rigidbody2D>().velocity.y);
                jumping = false;
            }

            if (jumpTimer > 0.0f)
            {
                jumpTimer -= Time.deltaTime;

                if (jumpTimer < 0.0f)
                {
                    jumpTimer = 0.0f;
                }
            }
        }

        if (jumping && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) <= maxSpeed)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(fowardAccel, 0.0f));

            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }

        if (moving)
        {
            if (targeting)
            {
                FollowCurrentTarget();
            }

            if (superJumping && superJumpTimer == 0.0f)
            {
                SuperJump();
            }
            else if (superJumping)
            {
                superJumpTimer -= Time.deltaTime;

                if (superJumpTimer < 0.0f)
                {
                    superJumpTimer = 0.0f;
                }
            }
            else
            {
                Jump();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBarrier")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, GetComponent<Rigidbody2D>().velocity.y);
            jumping = false;
            Reverse();
        }
        else if(collision.gameObject.tag == "UntargetBarrier")
        {
            UnTarget();
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, GetComponent<Rigidbody2D>().velocity.y);
            jumping = false;
            Reverse();
        }
    }

    public void Reverse()
    {
        fowardAccel *= -1;
    }

    private void Jump()
    {
        if(grounded && jumpTimer == 0.0f)
        {
            if(fowardAccel > 0.0f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1.0f, transform.localScale.y, transform.localScale.z);
            }

            if (Physics2D.OverlapBox(wallCheckBox.position, wallCheckBox.lossyScale, 0.0f, ground))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, highJumpHeight);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(fowardAccel, 0.0f));
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, jumpHeight);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(fowardAccel, 0.0f));
            }

            jumpTimer = defaultJumpInterval;
            jumping = true;
            anim.SetTrigger("startJump");
        }
    }

    public void SuperJump()
    {
        if (superJumping)
        {
            if(!grounded && Physics2D.OverlapBox(highWallCheckBox.position, highWallCheckBox.lossyScale, 0.0f, ground))
            {
                Reverse();
            }

            if (fowardAccel > 0.0f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1.0f, transform.localScale.y, transform.localScale.z);
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, highJumpHeight);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(fowardAccel, 0.0f));

            superJumping = false;
            jumping = true;
            jumpTimer = defaultJumpInterval;
            anim.SetTrigger("startJump");
        }
        else if (!superJumping && grounded)
        {
            if (fowardAccel > 0.0f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1.0f, transform.localScale.y, transform.localScale.z);
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, highJumpHeight);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(fowardAccel, 0.0f));

            superJumping = true;
            jumping = true;
            superJumpTimer = superJumpInterval;
            anim.SetTrigger("startJump");
        }
    }

    public void StopMoving()
    {
        moving = false;
    }

    public void StartMoving()
    {
        moving = true;
    }

    public void Retarget(Transform target)
    {
        targeting = true;
        currentTarget = target;
        FollowCurrentTarget();
    }

    private void FollowCurrentTarget()
    {
        if (currentTarget != null)
        {
            float xDist = currentTarget.position.x - GetComponent<Transform>().position.x;

            if (xDist < 0.0f)
            {
                fowardAccel = Mathf.Abs(fowardAccel) * -1;
            }
            else if (xDist > 0.0f)
            {
                fowardAccel = Mathf.Abs(fowardAccel);
            }
        }
    }

    public void UnTarget()
    {
        targeting = false;
        currentTarget = null;
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    public bool IsSuperJumping()
    {
        return superJumping;
    }
}
