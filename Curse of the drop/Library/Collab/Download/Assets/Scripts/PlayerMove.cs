using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform firePoint;
    public float maxSpeed;
    public float timer;
    public float speedDuration;
    private float currentMaxSpeed;
    public float enhancedMultiplier;
    private float speedMultiplier;

    public bool idle;
    public bool spedUp;
    public bool isSpeeding;

    public void isSpedUp(bool bootPickup){
        spedUp = bootPickup;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        spedUp = false;
        
        currentMaxSpeed = maxSpeed;

        speedMultiplier = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void movePlayer(float playerVelocity, bool grounded, bool hasWallJumped){
        //Checks if the player is sped up from the running boots
        //If so, the timer is being incremented by the delta time and a speed multiplier is applied to the player velocity
            if(spedUp){
                timer += Time.deltaTime;
                speedMultiplier = enhancedMultiplier;
                playerVelocity = playerVelocity * speedMultiplier;
                
                
            //Checks if the timer exceeds the set duration of the running boots effect
                if (timer > speedDuration) {    
                    isSpedUp(false);                   
                    speedMultiplier = 1f;
                    timer = 0;
                }
            }

            
        
            //Checks if the player is moving left. Player velocity is either negative(left) or positive(right)
            if (playerVelocity < 0)
            {
                //Player velocity is added to the current horizontal movement of the player, giving it gradual speed
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + playerVelocity, GetComponent<Rigidbody2D>().velocity.y);
                //Flips the character sprite so that it faces left when it moves left
                transform.localScale = new Vector3(-1f, 1f, 1f);
                idle = false;
                //firePoint.transform.rotation = Quaternion.Euler(0,0, -180);
            }

            //Checks if the player is moving right
            if (playerVelocity > 0)
            {
                //Same concept as above
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + playerVelocity, GetComponent<Rigidbody2D>().velocity.y);
                transform.localScale = new Vector3(1f, 1f, 1f);
                idle = false;
                //firePoint.transform.rotation = Quaternion.Euler(0,0, 180);
            }

            if(playerVelocity == 0 && (grounded || (!grounded && !hasWallJumped))){
                GetComponent<Rigidbody2D>().velocity = new Vector2(playerVelocity, GetComponent<Rigidbody2D>().velocity.y);
                idle = true;
            }
            
            //Checks if the player is moving to the left or right at max speed
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > currentMaxSpeed * speedMultiplier){
                //Subtracts player velocity when over max speed, keeping the net gain at 0
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x - playerVelocity, GetComponent<Rigidbody2D>().velocity.y);
                
            }
            
            
    }
}
