using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public float maxClimbSpeed;
    public float hopUpY;
    public float hopUpX;
    public float upClimb;
    public float climbStamina;
    public float buffStamina;
    private float tiredStamina;
    public float climbTimer;
    public float normalStamina;

    public float stabilizer;

    public bool canClimb;
    public bool isBuffed;

    public GameObject tiredImage;

    private Animator anim;
    private Animator climbAnim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        climbAnim = GetComponent<Animator>();
        tiredImage.SetActive(false);

        tiredStamina = climbStamina - 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void climbPlayer(bool hangingOffWall, float climbingVelocity, bool onCorner, bool isClimbing, bool onCeiling, bool grounded){
        
        //Checks for climbing input
        

            if((hangingOffWall || onCeiling) && canClimb){
                //Timer that increments as the player is climbing
                climbTimer += Time.deltaTime;

                //If the timer reaches a certain threshold before running out of stamina, a warning is displayed
                if(climbTimer > tiredStamina){
                        
                    //anim.SetBool("isTired", true);
                    tiredImage.SetActive(true);
                }

                //If the timer surpasses the stamina threshold(in seconds), then the player can no longer climb
                if (climbTimer > climbStamina) {    
                    canClimb = false;
                    //Timer set back to 0
                    climbTimer = 0;
                }

                //If the player is actually climbing with the grip button
                if(isClimbing && !onCeiling){
                    //Stops the player when they grab on the wall
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, stabilizer);
                    climbAnim.SetBool("climbing", true);
                }
               
                //Climb up or down based on vertical input
                if(onCeiling && isClimbing){
                    climbAnim.SetBool("ceilingClimb", true);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, upClimb);

                }

                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y + climbingVelocity);
            }
        

        


        //ClimbTimer and the ability to climb are reset upon touching the ground and the warning is deactivated
        if(grounded){
            canClimb = true;
            climbTimer = 0;
            //anim.SetBool("isTired", false);
            tiredImage.SetActive(false);
        }
        
       
        //If player reaches the upper corner of a wall, makes the player hop up over the corner
        if(hangingOffWall && !onCorner && isClimbing){
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + hopUpX,hopUpY);
        }

        //Sets a max climbing speed
        if(GetComponent<Rigidbody2D>().velocity.y > maxClimbSpeed){
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y - climbingVelocity);
        }
    }

    public void climbStaminaBuff(){
        climbStamina = buffStamina;
        tiredStamina = climbStamina - 2;
    }

    public void regulateStamina(){
        climbStamina = normalStamina;
        tiredStamina = climbStamina - 2;
    }
}
