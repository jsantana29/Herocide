using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    Vector2 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = startPosition;
        transform.position = Vector2.MoveTowards(transform.position, startPosition, Time.deltaTime *5f);
    }
}
