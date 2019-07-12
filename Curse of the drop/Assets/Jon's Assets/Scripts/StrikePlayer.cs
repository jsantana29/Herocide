using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikePlayer : MonoBehaviour
{
    public float maxJumpHeight = 6.0f;
    public float strikeDownwardSpeed = 20.0f;
    public float strikeForwardSpeed = 20.0f;
    public float jumpSpeed = 12.0f;
    public float defaultJumpRestartTime = 0.2f;
    public float deaccelerationSpeed = 2.0f;
    private float jumpRestartTimer;

    public Transform groundCheck;
    public Transform ceilingCheck;
    public LayerMask ground;

    private Transform currentTarget;

    private bool attacking;
    private bool jumpInitiated;
    private bool jumping;
    private bool striking;
    private bool strikeInitiated;

    private Vector2 jumpApexPoint;

    void Start()
    {
        currentTarget = null;
        attacking = false;
        striking = false;
        jumpInitiated = false;
        jumping = false;
        strikeInitiated = false;
        jumpRestartTimer = 0.0f;
    }

    void FixedUpdate()
    {
        if (attacking)
        {
            if (jumpInitiated)
            {
                GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                jumpApexPoint = new Vector2(transform.position.x, transform.position.y + maxJumpHeight);
                jumpInitiated = false;
                jumping = true;
            }

            if (jumping)
            {
                Jump();
            }
            else if (striking)
            {
                if(Physics2D.OverlapBox(groundCheck.position, groundCheck.lossyScale, 0.0f, ground))
                {
                    if(currentTarget.GetComponent<Masks>().getInvis())
                    {
                        StopAttacking();
                    }
                    else if(jumpRestartTimer > 0.0f)
                    {
                        jumpRestartTimer -= Time.deltaTime;
                        Deaccelerate();

                        if(jumpRestartTimer <= 0.0f)
                        {
                            jumpRestartTimer = 0.0f;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                            striking = false;
                            jumpInitiated = true;
                            strikeInitiated = false;
                        }
                    }
                }
                else
                {
                    if (!strikeInitiated)
                    {
                        if(!currentTarget.GetComponent<Masks>().getInvis())
                        {
                            FaceTarget();
                        }
                
                        strikeInitiated = true;
                    }

                    Strike();
                }
            }
        }
    }

    public void Attack(Transform target)
    {
        currentTarget = target;
        attacking = true;
        jumpInitiated = true;
    }

    public void StopAttacking()
    {
        currentTarget = null;
        attacking = false;
        striking = false;
        jumpInitiated = false;
        jumping = false;
        jumpRestartTimer = 0.0f;
    }

    private void Jump()
    {
        if(transform.position.y < jumpApexPoint.y - 0.2f && !Physics2D.OverlapBox(ceilingCheck.position, ceilingCheck.lossyScale, 0.0f, ground))
        {
            transform.position = Vector2.MoveTowards(transform.position, jumpApexPoint, jumpSpeed * Time.deltaTime);
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            jumping = false;
            striking = true;
            jumpRestartTimer = defaultJumpRestartTime;
        }
    }

    private void FaceTarget()
    {
        if (currentTarget != null)
        {
            float xDist = currentTarget.position.x - GetComponent<Transform>().position.x;

            if (xDist < 0.0f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (xDist > 0.0f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private void Strike()
    {
        if(transform.localScale.x > 0.0f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(strikeForwardSpeed, -strikeDownwardSpeed);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-strikeForwardSpeed, -strikeDownwardSpeed);
        }
    }

    private void Deaccelerate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        if(transform.localScale.x > 0.0f)
        {
            float newXVel = rigidbody.velocity.x - (deaccelerationSpeed * Time.deltaTime);

            if(newXVel < 0.0f)
            {
                newXVel = 0.0f;
            }

            rigidbody.velocity = new Vector2(newXVel, rigidbody.velocity.y);
        }
        else
        {
            float newXVel = rigidbody.velocity.x + (deaccelerationSpeed * Time.deltaTime);

            if(newXVel > 0.0f)
            {
                newXVel = 0.0f;
            }

            rigidbody.velocity = new Vector2(newXVel, rigidbody.velocity.y);
        }
    }
}