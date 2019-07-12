using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodArrow : MonoBehaviour
{
    public RectTransform arrow;
    public GameObject player;
    public GameObject quest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = quest.transform.position - player.transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, dir);
        arrow.rotation = Quaternion.Euler(0, 0, angle);
    }
}
