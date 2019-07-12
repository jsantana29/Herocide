using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    // int values
    public int maxPlayerHealth;
    public static int playerHealth;
    public static int healthToGive;

    // boolean values
    public bool usedHerb;
    public bool isDead;

    // text values
    Text healthText;

    // LevelManager value
    private LevelManager levelManager;


    
    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
        playerHealth = maxPlayerHealth;
        levelManager = FindObjectOfType<LevelManager>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0 && !isDead)
        {
            isDead = true;
            levelManager.respawnPlayer();
        }
        if ( playerHealth > maxPlayerHealth )
        {
            playerHealth = maxPlayerHealth;
        }

        // Tells the player their current health
        healthText.text = "" + playerHealth;
    }

    // method to deal damage to player
    public static void HurtPlayer(int damageToGive)
    {
        Debug.Log("Supposed to subtract player health");
        playerHealth -= damageToGive;

        if(playerHealth < 0){
            
            playerHealth = 0;
        }
    }

    public void FullHealth()
    {
        playerHealth = maxPlayerHealth;
    }

    public static void healthRestore(int healthToGive)
    {
        Debug.Log("Supposed to restore player health");
        Debug.Log("Health: " + healthToGive);
        playerHealth += healthToGive;
    }

    public void herbActivation(bool usedHerb)
    {
        if ( playerHealth > maxPlayerHealth )
        {
            usedHerb = false;
        }
        else
        {
            usedHerb = true;
            Debug.Log("Supposed To Add Health");
            healthRestore(healthToGive);
        }
    }
}
