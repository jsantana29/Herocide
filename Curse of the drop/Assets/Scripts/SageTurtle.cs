using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SageTurtle : MonoBehaviour
{
    public GameObject restPoint;
    public GameObject player;
    public float turtleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, restPoint.transform.position, Time.deltaTime * turtleSpeed);
        transform.localScale = player.transform.localScale;
    }
}
