using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float hookSpeed;
    public float lifespan;
    private PlayerInput player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerInput>();

        
        GetComponent<Rigidbody2D>().velocity = transform.right * hookSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "ground"){
            player.GetComponent<DistanceJoint2D>().enabled = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);

            player.GetComponent<DistanceJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            player.GetComponent<DistanceJoint2D>().distance = 0f;
            player.setZip(true);
            StartCoroutine("hookLife");
        }
        
    }

    public IEnumerator hookLife()
    {
        
        //hangingOffWall = false;
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
        player.GetComponent<DistanceJoint2D>().enabled = false;
        player.setZip(false);
        
    }
}
