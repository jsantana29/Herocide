using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManMovement : MonoBehaviour
{

    public float speed = 6.0f;
    public float movementTimer = 0.0f;

    public float waitTimer = 0.0f;
    public float defaultWaitInterval = 1.5f;
    public float defaultMovementInterval = 1.2f;

    public int highForwardChance = 70;
    public int lowForwardChance = 30;
    private int forwardChance = 50;

    private bool moving;
    private bool waiting;
    private bool selectingDirection;
    private bool reversing;

    public Transform wallCheck;
    public LayerMask ground;

    void Start()
    {
        moving = true;
        waiting = true;
        selectingDirection = false;
        reversing = false;
    }

    void FixedUpdate()
    {
        if (moving)
        {
            if (waiting)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);

                waitTimer -= Time.deltaTime;

                if (waitTimer <= 0.0f)
                {
                    waiting = false;
                    selectingDirection = true;
                    waitTimer = 0.0f;
                }
            }

            if (selectingDirection)
            {
                SelectRandomDirection();
                selectingDirection = false;
                movementTimer = defaultMovementInterval;
            }

            if (movementTimer > 0.0f)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0.0f);

                movementTimer -= Time.deltaTime;

                if (movementTimer <= 0.0f)
                {
                    movementTimer = 0.0f;
                    waiting = true;
                    waitTimer = defaultWaitInterval;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBarrier" || collision.gameObject.tag == "UntargetBarrier")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            movementTimer = 0.0f;
            waiting = true;
            selectingDirection = false;
            waitTimer = defaultWaitInterval;
            reversing = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBarrier" || collision.gameObject.tag == "UntargetBarrier")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            movementTimer = 0.0f;
            waiting = true;
            selectingDirection = false;
            waitTimer = defaultWaitInterval;
            reversing = true;
        }
    }

    private void SelectRandomDirection()
    {
        if(selectingDirection)
        {
            if (Physics2D.OverlapBox(wallCheck.position, wallCheck.lossyScale, 0.0f, ground) || reversing)
            {
                reversing = false;

                if(transform.lossyScale.x > 0.0f)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    speed = -Mathf.Abs(speed);
                    forwardChance = lowForwardChance;
                }
                else
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    speed = Mathf.Abs(speed);
                    forwardChance = highForwardChance;
                }
            }
            else
            {
                int randomNum = Random.Range(1, 101);

                if(randomNum <= forwardChance)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    speed = Mathf.Abs(speed);
                    forwardChance = highForwardChance;
                }
                else
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    speed = -Mathf.Abs(speed);
                    forwardChance = lowForwardChance;
                }
            }
        }
    }

    public void StopMoving()
    {
        moving = false;
        waiting = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        waitTimer = 0.0f;
        movementTimer = 0.0f;
    }

    public void StartMoving()
    {
        moving = true;
    }

    private void Reverse()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed *= -1.0f;
    }
}
