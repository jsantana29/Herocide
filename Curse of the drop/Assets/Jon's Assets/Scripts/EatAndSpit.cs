using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAndSpit : MonoBehaviour
{
    public float nodeRange = 3.5f;
    public float defaultEatTimeInterval = 0.5f;
    public float spitForwardSpeed = 12.5f;
    public float spitUpwardSpeed = 12.5f;

    private float eatTimer;

    public Transform superJumpLocationNode;
    public Transform eatingHitbox;
    public Transform bodyHitbox;
    public LayerMask playerLayer;

    private bool superJumpInitiated = false;
    private bool spitting = false;

    private GameObject capturedPlayer = null;
    private TurnipMovement movement;

    void Start()
    {
        movement = GetComponent<TurnipMovement>();
        eatTimer = 0.0f;
    }

    void FixedUpdate()
    {
        if(eatTimer > 0.0f)
        {
            Collider2D player = Physics2D.OverlapCircle(eatingHitbox.position, eatingHitbox.GetComponent<CircleCollider2D>().radius, playerLayer);
            
            if(player != null)
            {
                capturedPlayer = player.gameObject;
                capturedPlayer.GetComponent<Rigidbody2D>().isKinematic = true;
                capturedPlayer.GetComponent<PlayerInput>().stopMoving();
                DisablePlayerColliders();
                capturedPlayer.transform.SetParent(bodyHitbox, false);
                capturedPlayer.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                capturedPlayer.GetComponent<SpriteRenderer>().enabled = false;
                movement.Retarget(superJumpLocationNode);
                movement.StartMoving();
                eatTimer = 0.0f;
            }
            else
            {
                eatTimer -= Time.deltaTime;

                if(eatTimer < 0.0f)
                {
                    eatTimer = 0.0f;

                    if(capturedPlayer == null)
                    {
                        movement.StartMoving();
                    }
                }
            }
        }

        if (!spitting && capturedPlayer != null)
        {
            capturedPlayer.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

            if (!superJumpInitiated)
            {
                if (movement.IsGrounded() && GetComponent<Rigidbody2D>().velocity.y == 0.0f && Vector2.Distance(transform.position, superJumpLocationNode.position) <= nodeRange)
                {
                    movement.UnTarget();
                    movement.SuperJump();
                    superJumpInitiated = true;
                }
            }
            else if (superJumpInitiated && !movement.IsSuperJumping() && GetComponent<Rigidbody2D>().velocity.y <= 0.0f)
            {
                Spit();
            }
        }
        else if(spitting && !GetComponent<Rigidbody2D>().IsTouching(capturedPlayer.GetComponent<Collider2D>()))
        {
            bodyHitbox.gameObject.GetComponent<Collider2D>().enabled = true;
            capturedPlayer = null;
            spitting = false;
            GetComponent<TerminationSequence>().InitiateTerminationSequence();
        }
    }

    public void Eat()
    {
        if (eatTimer == 0.0f && !spitting && !superJumpInitiated)
        {
            movement.StopMoving();
            eatTimer = defaultEatTimeInterval;
        }
    }

    private void Spit()
    {
        capturedPlayer.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        capturedPlayer.GetComponent<SpriteRenderer>().enabled = true;
        capturedPlayer.transform.parent = null;
        capturedPlayer.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        EnablePlayerColliders();
        bodyHitbox.gameObject.GetComponent<Collider2D>().enabled = false;
        Rigidbody2D playerRigidBody = capturedPlayer.GetComponent<Rigidbody2D>();
        playerRigidBody.isKinematic = false;

        if (transform.localScale.x > 0.0f)
        {
            playerRigidBody.velocity = new Vector2(spitForwardSpeed, spitUpwardSpeed);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(-spitForwardSpeed, spitUpwardSpeed);
        }
        
        capturedPlayer.GetComponent<PlayerInput>().Launch();
        capturedPlayer.GetComponent<PlayerInput>().startMoving();
        
        spitting = true;
        superJumpInitiated = false;
    }

    private void DisablePlayerColliders()
    {
        capturedPlayer.GetComponent<Collider2D>().enabled = false;

        Collider2D[] childColliders = capturedPlayer.GetComponentsInChildren<Collider2D>();

        foreach(var collider in childColliders)
        {
            collider.enabled = false;
        }
    }

    private void EnablePlayerColliders()
    {
        capturedPlayer.GetComponent<Collider2D>().enabled = true;

        Collider2D[] childColliders = capturedPlayer.GetComponentsInChildren<Collider2D>();

        foreach (var collider in childColliders)
        {
            collider.enabled = true;
        }
    }
}
