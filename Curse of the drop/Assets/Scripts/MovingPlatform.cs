using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public int direction;
    public float platformSpeed;

    // public GameObject startNode;
    // public GameObject endNode;

    public GameObject platform;

    public Transform currentPoint;

    public Transform[] points;

    public int pointSelection;


    public bool isGoingRight;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //currentNode = endNode;
        currentPoint = points[pointSelection];
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(platformSpeed * direction, rb.velocity.y);
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * platformSpeed);
        
        if(platform.transform.position == currentPoint.position){
            pointSelection++;

            if(pointSelection == points.Length){
                pointSelection = 0;
            }

            currentPoint = points[pointSelection];
        }
    }

    // public void setDirection(){
    //     direction *= -1;
        
    // }
}
