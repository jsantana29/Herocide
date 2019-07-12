using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    public float raycastDist = 15.0f;
    public float speed = 6.0f;

    private bool moving;
    private bool targeting;

    public LayerMask playerLayer;
    public LayerMask ground;
    public Transform wallCheck;

    private Transform currentTarget;

    void Start()
    {
        moving = true;
        targeting = false;
        currentTarget = null;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckRaycast();

        if (targeting && moving)
        {
            if(currentTarget.GetComponent<Masks>().getInvis())
            {
                Untarget();
            }
            else
            {
                FollowCurrentTarget();
            }
        }
        else if(moving)
        {
            if (Physics2D.OverlapBox(wallCheck.position, wallCheck.lossyScale, 0.0f, ground))
            {
                Reverse();
            }

            Move();
        }
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
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-Mathf.Abs(speed), 0.0f);
            }
            else if (xDist > 0.0f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Abs(speed), 0.0f);
            }
        }
    }

    public void Untarget()
    {
        targeting = false;
        currentTarget = null;
    }

    public void Reverse()
    {
        if (transform.localScale.x > 0.0f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void Move()
    {
        if (transform.localScale.x > 0.0f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0.0f);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0.0f);
        }
    }

    public void CheckRaycast()
    {
        RaycastHit2D player;

        if (transform.localScale.x > 0.0f)
        {
            player = Physics2D.Raycast(transform.position, new Vector2(1.0f, 0.0f), raycastDist, playerLayer);
        }
        else
        {
            player = Physics2D.Raycast(transform.position, new Vector2(-1.0f, 0.0f), raycastDist, playerLayer);
        }

        if(player.collider && !player.collider.gameObject.GetComponent<Masks>().getInvis())
        {
            Retarget(player.collider.gameObject.transform);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBarrier")
        {
            Reverse();
        }
        else if (collision.gameObject.tag == "UntargetBarrier")
        {
            Untarget();
            Reverse();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBarrier")
        {
            Reverse();
        }
        else if (collision.gameObject.tag == "UntargetBarrier")
        {
            Untarget();
            Reverse();
        }
    }
}
