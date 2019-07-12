using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private static PlayerMove playerMove;
    private PlayerInput player;
    private Inventory inventory;
    private GrapplingHook grappling;
    private LevelLoader loader;

    
    private static float speedUp = 2;
    public int pointsToAdd;
    public int healthToGive;

    private static float bootTimer = 10f;
    private static float invisTimer = 10f;

    private static float newSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        player = FindObjectOfType<PlayerInput>();
        inventory = FindObjectOfType<Inventory>();
        loader = FindObjectOfType<LevelLoader>();
        grappling = FindObjectOfType<GrapplingHook>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    


    void OnTriggerEnter2D(Collider2D other)
    {
        // Cancels the script if the trigger isn't met
        if(other.GetComponent<PlayerInput>() == null)
            return;

        // Confirms the script ran successfully
        Debug.Log("Script successfully loaded");

        //Adds mask to the inventory
        if (gameObject.tag == "InvisibilityMask")
        {

            player.addInventory("Invisibility");
  
        }

        if (gameObject.tag == "StaminaMask")
        {

            player.addInventory("Stamina");
  
        }

        if (gameObject.tag == "GodMask")
        {

            player.addInventory("God");
  
        }

        if (gameObject.tag == "CompanionMask")
        {

            player.addInventory("Companion");
  
        }

        if (gameObject.tag == "Herb")
        {
            player.addItem("Herb");
            Debug.Log("Supposed to add Herb into Inventory");
            // HealthManager.healthRestore(healthToGive);
        }
        
        if (gameObject.tag == "Boots")
        {

            player.addItem("Boots");
            Debug.Log("Supposed to add Boots into Inventory");
            //Sets the spedUp variable to true in the Player Move script
            //playerMove.isSpedUp(true);
        }

        if (gameObject.tag == "Hook")
        {
            player.addItem("Hook");
            Debug.Log("Supposed to add Hook into Inventory");
            grappling.setShots();
        }
        
        if (gameObject.tag == "Coins")
        {
            Debug.Log("Supposed to add points");
            ScoreManager.AddPoints(pointsToAdd);
        }

        if (gameObject.tag == "Rune")
        {
            loader.setRune();
        }

        // Destroys object
        Destroy (gameObject);
    }
}
