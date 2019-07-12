using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileBodyCollision : MonoBehaviour
{
    private const float HORIZONTAL_KNOCKBACK = 8.0f;
    private const float VERTICAL_KNOCKBACK = 6.0f;

    private void OnCollisionStay2D(Collision2D player)
    {
        Debug.Log("WE GOT A HIT!");
        float xDist = player.gameObject.transform.position.x - transform.position.x;

        if (xDist < 0.0f)
        {
            player.gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(-HORIZONTAL_KNOCKBACK, VERTICAL_KNOCKBACK));
        }
        else if (xDist > 0.0f)
        {
            player.gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(HORIZONTAL_KNOCKBACK, VERTICAL_KNOCKBACK));
        }
        else
        {
            player.gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(0.0f, VERTICAL_KNOCKBACK));
        }
    }
}
