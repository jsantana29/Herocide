using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodHandScript : MonoBehaviour
{
    //GodHand variables
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    private Transform placeHolderChild;
    private bool randomSpawn = false;
    private bool isMad = false;

    public float speed;
    public Transform spawnSpot;
    public Transform startSpot;
    public Transform finishSpot;
    
    //Player stuff
    private PlayerInput pScript;
    private PlayerMove pIsIdle;
    private Rigidbody2D playerRb;
    private bool isIdle = false;
    private bool caught = false;
    
    public GameObject player;
    
    // Level Ratio stuff
    private float levelRatio;
    private float playerLvlRatio;
    private float minRatio;
    
    //Player last position
    private GameObject Player;
    private bool PlayerCaught = false;

    // Idle Timer
    public const float restTimer = 6.0f;
    private float idleTimer = restTimer;

    // Initate Timer
    public const float levelMinSpawnTimer = 20.0f;
    private float initTimer = 0.0f;


    // follower timer
    public const float resetStalker = 5.0f;
    private float stalkerTimer = resetStalker; // if time add condition

    //Animator for the god hand
    private Animator anim;

    private LevelManager LevelManager;
    

    void Start()
    {
        //this.gameObject.SetActive(false);// = false;
        // Initilize the Rigidbodys
        rb = GetComponent<Rigidbody2D>();               // Godhand
        playerRb = player.GetComponent<Rigidbody2D>();  // player
        placeHolderChild = this.transform.GetChild(0);  // child of GodHand for player to move to

        //GodHand starts at spawn spot so that player doesn't see hand until needed
        transform.position = spawnSpot.position;
        playerDirection = player.transform.position - this.transform.position;

        pScript = player.GetComponent<PlayerInput>();  //FindObjectOfType<PlayerScript>();
        pIsIdle = player.GetComponent<PlayerMove>();

        anim = GetComponentInChildren<Animator>();

        LevelManager = FindObjectOfType<LevelManager>();

        // Level Ratio
        levelRatio = (finishSpot.position - startSpot.position).sqrMagnitude;
        playerLvlRatio = levelRatio;
        minRatio = levelRatio;
    }

    private void FixedUpdate()
    {      
        // if player is idle start timer, otherwise reset
        idleTimerCounter();

        // if player is idle create player last position object (Player)
        playerLastPositionObj();

        // for testing purposes only 
        if (Input.GetKeyDown(KeyCode.G))
        {
            transform.position = randomPos(Camera.main.orthographicSize+1, Camera.main.transform);
        }
        // God Hand movement for catches of player and place holder, and idle players
        godHandMovement();

        // Player ratio
        playerLvlRatio = (finishSpot.position - player.transform.position).sqrMagnitude;

        if (playerLvlRatio < minRatio)
            minRatio = playerLvlRatio;

        initTimer += Time.fixedDeltaTime;

        if (initTimer >= (levelMinSpawnTimer * (1 + (minRatio/levelRatio))))
        {
            // activate god hand to stalk player
            isMad = true;
        }

        if (isMad)
        {
            stalkerTimer -= Time.fixedDeltaTime;
            if (stalkerTimer < 0)
            {
                // go back to spawn spot to get out of the frame
                stalkerTimer = resetStalker;
                initTimer = 0.0f;
                PlayerCaught = true;
            }
        }
        
    }
    // if the god hand gets out of frame for ANY camera, it resets the booleans to catch the player again later
   /* void OnBecameInvisible()
    {
        if ()
        resetAfter();
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!caught)
        {
            idleTimer = restTimer;
            // if player is caught instead of place holder (Player) change players components accordingly
            if (collision.gameObject == player)
            {
                anim.SetTrigger("catch");
                anim.SetBool("caught", true);
                caught = true;
                // changed player.GetComponent to playerRb.GetComponent
                playerRb.GetComponent<BoxCollider2D>().enabled = false;
                playerRb.GetComponent<PlayerInput>().enabled = false;
                playerRb.GetComponent<Rigidbody2D>().isKinematic = true;
                //Camera.main.GetComponent<CameraScript>().enabled = false;
                //player.transform.GetChild(0).transform.parent = null;
                delChildren(player);
                // Destroy(GameObject.FindGameObjectWithTag("Player"));
                LevelManager.respawnPlayer();
                player.GetComponent<SpriteRenderer>().enabled = false;
            }
            // if place holder is caught instead of player destory the Player object
            else if (collision.gameObject.tag == "Player") 
            {
                //Destroy(collision.gameObject);
                // LevelManager.respawnPlayer();
                // player.GetComponent<SpriteRenderer>().enabled = false;
                PlayerCaught = true;
            }
        }
    }

    #region Helper methods
    private Vector2 randomPos(float factor,Transform pos)
    {
        float ranPosx = Random.Range(-100f, 100f);
        float ranPosy = (100-Mathf.Abs(ranPosx))*(Random.value < .5 ? 1 : -1);
        return (Vector2)pos.transform.position+(new Vector2(ranPosx, ranPosy).normalized) * factor * Mathf.Sqrt(5);
        //The square root of 5 is the rectangular ratio of the camera view
    }
    
    private void idleTimerCounter()
    {
        if (pIsIdle.idle && !caught)
        {
            idleTimer -= Time.fixedDeltaTime;
        }
        else
        {
            idleTimer = restTimer;

        }
    }

    private void playerLastPositionObj()
    {
        if (idleTimer < 0)
        {
            this.gameObject.SetActive(true);
            if (Player == null)
            {
                // initalize/add object components
                Player = new GameObject("Player");
                Player.gameObject.tag = "Player";
                Player.AddComponent<CircleCollider2D>();
                Player.GetComponent<CircleCollider2D>().isTrigger = true;

                // get direction for hand to move towards
                playerDirection = playerDirection.normalized;
                Player.transform.position = player.transform.position;

            }
            isIdle = true;            
        }
    }

    private void godHandMovement()
    {
        if (caught) // player is caught
        {
            // After catching player, the god hand moves to spawn point outside of camera view
            transform.position = Vector3.MoveTowards(transform.position, spawnSpot.position, Time.fixedDeltaTime * speed);

            // make player go to center of hand (might not be needed when we get animation)
            player.transform.position = Vector3.MoveTowards(player.transform.position, placeHolderChild.transform.position, Time.fixedDeltaTime * speed);
            //IsIdle and caught bollean will reset after the grab animation.
        }
        else if (PlayerCaught) // player place holder is caught instead of player
        {
            // After catching player place holder, the god hand moves to spawn point outside of camera view
            transform.position = Vector3.MoveTowards(transform.position, spawnSpot.position, Time.fixedDeltaTime * speed);
            if (!this.gameObject.GetComponent<Renderer>().isVisible)
            {
                resetAfter();
            }
            
            
        }
        else if (isIdle) // if player is idle 
        {
            // move towards players last position
            this.transform.position = Vector3.MoveTowards(this.transform.position, Player.transform.position, Time.fixedDeltaTime * speed);
            // Teleport (spawn) the GodHand around the player camera once the player is idle
            if (!randomSpawn)
            {
                transform.position = randomPos(Camera.main.orthographicSize + 1, Camera.main.transform);
                randomSpawn = true;
            }
        }
        else if(isMad)
        {
            if (!randomSpawn)
            {
                transform.position = randomPos(Camera.main.orthographicSize + 1, Camera.main.transform);
                randomSpawn = true;
            }
            playerDirection = playerDirection.normalized;
            rb.MovePosition(rb.position + playerDirection * Time.fixedDeltaTime * speed);   // move position since add force doesn't work on kinematics
            playerDirection = player.transform.position - this.transform.position;          //^ - Get the vector difference to get the player direction.
       
        }
    }

    private void delChildren(GameObject obj)
    {
        for (var i = obj.transform.childCount - 1; i >= 1; i--)
        {
            obj.transform.GetChild(i).transform.parent = null;
        }
    }
   
    private void resetAfter()
    {
        transform.position = spawnSpot.position;
        isIdle = false;
        randomSpawn = false;
        PlayerCaught = false;
        isMad = false;
    }
    #endregion

}
