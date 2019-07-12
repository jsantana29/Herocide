using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpHeight;
    public float kickBack;
    public float faceLeft;
    public float faceRight;

    public bool wallJumped;

    public int mattyFactor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        
    }
    
    //Method that makes the player jump
    public void jumpPlayer(bool grounded, bool hangingOffWall){
        //Checks if player is on the ground or hanging on a wall
        if(grounded || hangingOffWall){
            //wallJumped = false;
            //Checks if player is not on the ground yet on the wall
            if(hangingOffWall && !grounded){
                //wallJumped = true;
                
                if (transform.localScale.x == -1f){
                    Debug.Log("Kickback");
                    GetComponent<Rigidbody2D>().velocity = new Vector2(kickBack * mattyFactor, GetComponent<Rigidbody2D>().velocity.y);
                    transform.localScale = new Vector3(faceRight, 1f, 1f);
                    //wallJumped = true;
                }
                else if (transform.localScale.x == 1f)
                {
                    Debug.Log("Kickback");
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-kickBack * mattyFactor, GetComponent<Rigidbody2D>().velocity.y);
                    transform.localScale = new Vector3(faceLeft, 1f, 1f);
                    //wallJumped = true;
                    
                }
                
                
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            
        }

        
        
    }
}
