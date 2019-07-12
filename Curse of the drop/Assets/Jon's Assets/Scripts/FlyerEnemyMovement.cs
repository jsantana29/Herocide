using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerEnemyMovement : MonoBehaviour
{
    public float raycastDist = 15.0f;
    public float speed = 6.0f;

    private bool moving;

    public LayerMask playerLayer;
    public LayerMask ground;
    public Transform wallCheck;

    private FlyerDive dive;
    public Masks masks;


    void Start()
    {
        moving = true;
        dive = GetComponent<FlyerDive>();
    }

    void FixedUpdate()
    {
        if (!dive.isDiving())
        {
            CheckRaycast();
        }

        if (!dive.isDiving() && moving)
        {
            if (Physics2D.OverlapBox(wallCheck.position, wallCheck.lossyScale, 0.0f, ground))
            {
                Reverse();
            }

            Move();
        }
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
            player = Physics2D.Raycast(transform.position, new Vector2(1.0f, -1.0f), raycastDist, playerLayer);
            Debug.DrawRay(transform.position, new Vector2(1.0f, -1.0f), Color.red);
        }
        else
        {
            player = Physics2D.Raycast(transform.position, new Vector2(-1.0f, -1.0f), raycastDist, playerLayer);
            Debug.DrawRay(transform.position, new Vector2(-1.0f, -1.0f), Color.red);
        }

        if (player.collider && !player.collider.gameObject.GetComponent<Masks>().getInvis())
        {
            dive.Dive();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBarrier" || collision.gameObject.tag == "UntargetBarrier")
        {
            Reverse();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBarrier" || collision.gameObject.tag == "UntargetBarrier")
        {
            Reverse();
        }
    }
}
