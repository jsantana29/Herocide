using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Script objects
    private PlayerMove move;
    private PlayerJump jump;
    private PlayerClimb climb;
    private Masks playerMask;
    private SpeechScript speech;

    public GameObject companion;
    public GameObject restPoint;
    public GameObject speechBubble;

    private MaskController maskController;

    private int maskCounter;
    private const int INVENTORY_SIZE = 3;


    //Float values
    private float playerVelocity;
    private float climbingVelocity;
    public float playerSpeed;
    public float climbingSpeed;
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;

    public float speechTimer;
    public float speechChange;

    public float groundCheckRadius;
    public float wallCheckRadius;
    public float cornerCheckRadius;
    public float ceilingCheckRadius;

    //Boolean values
    private bool hasJumped;
    public bool hasWallJumped;
    private bool isClimbing;
    public bool isZipping;
    public bool grounded;
    public bool hangingOffWall;
    public bool onCorner;
    public bool onCeiling;
    public bool isTired;
    public bool canClimb;
    public bool knockFromRight;
    public bool turtleSpawned;

    public bool jumpAnim;

    public bool ActivateKBToReset;

    //Transforms
    public Transform groundcheck;
    public Transform wallCheck;
    public Transform cornerCheck;
    public Transform ceilingCheck;

    private List<string> maskInventory;
    //public GameObject movingPlatform;


    //Layermasks
    public LayerMask whatIsGround;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<PlayerMove>();
        jump = GetComponent<PlayerJump>();
        climb = GetComponent<PlayerClimb>();
        playerMask = GetComponent<Masks>();

        maskController = FindObjectOfType<MaskController>();
        speech = FindObjectOfType<SpeechScript>();
        anim = GetComponent<Animator>();
        

        hasJumped = false;
        hasWallJumped = false;
        isClimbing = false;
        isZipping = false;

        GetComponent<DistanceJoint2D>().enabled = false;
        maskInventory = new List<string>();

        maskCounter = 0;

        knockbackCount = knockbackLength;
    }

    // Update is called once per frame
    //Used for receiving input
    void Update()
    {
        //Input.GetAxisRaw either returns a 0(not moving), 1(right), -1(left)
        //It is multiplied by playerspeed to get a velocity that either moves left or right

        if(!isZipping){
            playerVelocity = Input.GetAxisRaw("Horizontal") * playerSpeed;
            climbingVelocity = Input.GetAxisRaw("Vertical") * climbingSpeed;
        }
        


        if(Input.GetButtonDown("Jump")){
            hasJumped = true;
            GetComponent<DistanceJoint2D>().enabled = false;
            isZipping = false;

            if(grounded){
                anim.SetTrigger("jumping");
            }
            
            //Debug.Log("Jump start");
            //checkJump();
        }

        

        if(Input.GetButton("Climb")){
            isClimbing = true;
            GetComponent<DistanceJoint2D>().enabled = false;
            isZipping = false;
            //checkClimb();
        }
        else{
            isClimbing = false;
            
        }

        if(!hangingOffWall || !onCorner){
            anim.SetBool("climbing",false);
        }

        if(!onCeiling){
            anim.SetBool("ceilingClimb", false);
        }

        if(Input.GetButtonDown("Mask")){
            Debug.Log("mask power");

            if(maskInventory.Count > 0){
                playerMask.maskPower(maskInventory[maskCounter]);
            }
            
        }

        if(Input.GetButtonDown("LeftToggle")){
            if(maskInventory.Count >= 2){
                maskCounter--;

                if(maskCounter < 0){
                    maskCounter = maskInventory.Count - 1;
                }

                
                Debug.Log("Mask " +maskCounter);
                Debug.Log("Mask count " +maskInventory.Count);
            }
            
            
        }

        if(Input.GetButtonDown("RightToggle")){
            if(maskInventory.Count >= 2){
                 maskCounter++;

                if(maskCounter >= maskInventory.Count){
                    maskCounter = 0;
                }

                
                Debug.Log("Mask " +maskCounter);
                Debug.Log("Mask count " +maskInventory.Count);
            }
            
            
        }

        if(maskInventory.Count > 0){
             maskController.setMask(maskInventory[maskCounter]);

            if(maskInventory[maskCounter].Equals("Stamina")){
                climb.climbStaminaBuff();
            }
            else{
                climb.regulateStamina();
            }

            if(maskInventory[maskCounter].Equals("Companion")){
                companion.SetActive(true);
                
                if(!turtleSpawned){
                    companion.transform.position = restPoint.transform.position;
                    speech.setSpawn(true);
                    speechBubble.SetActive(true);
                    speech.setSpeech();
                }

                turtleSpawned = true;

                speechTimer += Time.deltaTime;

                if(speechTimer > speechChange){
                    speech.setSpeech();
                    speechTimer = 0;
                }
                
            }
            else{
                companion.SetActive(false);
                speechBubble.SetActive(false);
                speech.setSpawn(false);
                turtleSpawned = false;
            }


        }

        resetKBState(ActivateKBToReset);

    }

    //FixedUpdate is used for physics calculations. Similar to update 
    void FixedUpdate(){

        checkMove();

        grounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, whatIsGround);
        hangingOffWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsGround);
        onCorner = Physics2D.OverlapCircle(cornerCheck.position, cornerCheckRadius, whatIsGround);
        onCeiling = Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, whatIsGround);

        if(hangingOffWall && hasJumped){
            hasWallJumped = true;
        }

        if(grounded){
            hasWallJumped = false;
           
            //Debug.Log("Jump end");
            checkClimb();
        }
        anim.SetBool("grounded", grounded);

        if(hasJumped){
            checkJump();
            hasJumped = false;
        }

        if(isClimbing){
            checkClimb();
            
        }

        
        
    }

    //Method that goes into PlayerJump script and applies the jump
    void checkJump(){
        jump.jumpPlayer(grounded, hangingOffWall);
    }

    //Method that goes into PlayerMove script and applies the movement
    void checkMove(){
        move.movePlayer(playerVelocity, grounded, hasWallJumped);
    }

    //Method that goes into PlayerClimb script and applies the climbing
    void checkClimb(){
        climb.climbPlayer(hangingOffWall, climbingVelocity, onCorner, isClimbing, onCeiling, grounded);
    }

    public void sans(){
        //GetComponent<AudioSource>().Play();
    }

    public void setZip(bool isZip){
        isZipping = isZip;
    }

    public float getPlayerVelocity(){
        return playerVelocity;
    }

    public void addInventory(string mask){
        maskInventory.Add(mask);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.transform.tag == "Platform"){
            //Debug.Log("Should be child");
            transform.parent = other.transform;
        }
    }

    // void OnTriggerEnter2D(Collider2D other){
    //     if(other.transform.tag == "Platform"){
    //         //Debug.Log("Should be child");
    //         transform.parent = other.transform;
    //     }
    // }

    void OnCollisionExit2D(Collision2D other){
        if(other.transform.tag == "Platform"){
            transform.parent = null;
        }
    }

    // void OnTriggerExit2D(Collider2D other){
    //     if(other.transform.tag == "Platform"){
    //         transform.parent = null;
    //     }
    // }

    public void resetKBState(bool resetKB)
    {
        if (resetKB)
        {
            knockbackCount -= Time.deltaTime;

            if (knockbackCount <= 0)
            {
                knockbackCount -= Time.deltaTime;
                Input.GetAxisRaw("Horizontal");
                ActivateKBToReset = false;
                knockbackCount = knockbackLength;
            }
            else
            {
                if (knockFromRight)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-knockback, 0);
                }
                if (!knockFromRight)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, 0);
                }

            }
        }
    }



}
