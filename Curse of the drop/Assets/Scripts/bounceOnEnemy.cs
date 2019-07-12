using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceOnEnemy : MonoBehaviour
{
    // Float values
    public float bounce;
    private float enemyBounce;

    //Private variables
    private Rigidbody2D myrigidbody2D;
    private AudioSource music;

    private PlayerInput player;
    public Animator anim;

    private GameObject lastCollider;

    private int collisions;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myrigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerInput>();
        collisions = 0;
        enemyBounce = 20;
        
        //music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Method to trigger the bounce
    void OnTriggerEnter2D(Collider2D other)
    {
       

        //Debug.Log("Player is supposed to bounce off enemy.");
        if (other.tag == "Enemy"){
            
            //Applies the bounce to the player
            myrigidbody2D.velocity = new Vector2(myrigidbody2D.velocity.x, enemyBounce);
            anim.SetTrigger("Bounce");
            
            
            
        }

        if(other.tag == "Sans"){
            //If there were no previous collisions, then the last collider is the current one
             if(collisions == 0){
                lastCollider = other.gameObject;
            }
            
            collisions++;

            //Applies the bounce to the player
            myrigidbody2D.velocity = new Vector2(myrigidbody2D.velocity.x, bounce);
            anim.SetTrigger("Bounce");
            
            //Stops the music of the previous game object
            //lastCollider.GetComponent<AudioSource>().Stop();
            lastCollider = other.gameObject;

            //Plays the audio of the new game object
            //other.GetComponent<AudioSource>().Play();
        }
            
    }
}
