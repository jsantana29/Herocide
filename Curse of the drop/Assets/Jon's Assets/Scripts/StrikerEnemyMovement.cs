using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerEnemyMovement : MonoBehaviour
{
    public float raycastDist = 15.0f;
    public float speed = 6.0f;

    private bool moving;
    private bool attacking;

    public LayerMask playerLayer;
    public LayerMask ground;
    public Transform wallCheck;

    private StrikePlayer strikePlayer;


    void Start()
    {
        moving = true;
        attacking = false;
        strikePlayer = GetComponent<StrikePlayer>();
    }

    void FixedUpdate()
    {
        if (!attacking)
        {
            CheckRaycast();
        }

        if (!attacking && moving)
        {
            if (Physics2D.OverlapBox(wallCheck.position, wallCheck.lossyScale, 0.0f, ground))
            {
                Reverse();
            }

            Move();
        }
    }

    public void Untarget()
    {
        attacking = false;
        strikePlayer.StopAttacking();
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

        if (player.collider && !player.collider.gameObject.GetComponent<Masks>().getInvis())
        {
            strikePlayer.Attack(player.collider.gameObject.transform);
            attacking = true;
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
