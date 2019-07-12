using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnipIdleSearch : MonoBehaviour
{
    public Transform searchHitBox;
    public LayerMask playerLayer;
    private TurnipMovement movement;
    private bool idleing = false;

    void Start()
    {
        movement = GetComponent<TurnipMovement>();
    }

    void FixedUpdate()
    {
        if (idleing)
        {
            Collider2D player = Physics2D.OverlapCircle(searchHitBox.position, searchHitBox.GetComponent<CircleCollider2D>().radius, playerLayer);

            if (player && true)
            {
                movement.Retarget(player.gameObject.transform);
                idleing = false;
                GetComponent<Animator>().SetBool("wakeUpYaLazyBum", true);
            }
        }
    }

    public void StartIdleing()
    {
        movement.StopMoving();
        idleing = true;
    }
}
