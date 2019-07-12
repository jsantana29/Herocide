using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerDive : MonoBehaviour
{
    private bool diving;
    private bool descending;
    private bool ascending;

    public float descentForwardSpeed;
    public float descentDownwardSpeed;
    public float ascentForwardSpeed;
    public float ascentUpwardSpeed;
    public float maxDiveDistance;

    private float lowestDiveY;
    private float originalY;

    public Transform groundCheck;
    public LayerMask ground;

    void Start()
    {
        diving = false;
        descending = false;
        ascending = false;
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (diving)
        {
            if (!descending && !ascending)
            {
                originalY = transform.position.y;
                lowestDiveY = originalY - maxDiveDistance;
                descending = true;
            }

            if (descending)
            {
                if (Physics2D.OverlapBox(groundCheck.position, groundCheck.lossyScale, 0.0f, ground))
                {
                    descending = false;
                    ascending = true;
                }
                else if (transform.position.y <= lowestDiveY)
                {
                    transform.position = new Vector2(transform.position.x, lowestDiveY);
                    descending = false;
                    ascending = true;
                }
                else
                {
                    Descend();
                }
            }
            else if (ascending)
            {
                if (transform.position.y >= originalY)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                    transform.position = new Vector2(transform.position.x, originalY);
                    ascending = false;
                    diving = false;
                }
                else
                {
                    Ascend();
                }
            }
        }
    }

    private void Descend()
    {
        if (transform.localScale.x > 0.0f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(descentForwardSpeed, -descentDownwardSpeed);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-descentForwardSpeed, -descentDownwardSpeed);
        }
    }

    public void Ascend()
    {
        if (transform.localScale.x > 0.0f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(ascentForwardSpeed, ascentUpwardSpeed);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-ascentForwardSpeed, ascentUpwardSpeed);
        }
    }

    public bool isDiving()
    {
        return diving;
    }

    public void Dive()
    {
        diving = true;
    }
}
