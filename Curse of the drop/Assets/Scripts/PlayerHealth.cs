using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Int Values
    public int maxPlayerHealth;
    public static int playerHealth;

    // Text Values
    Text text;

    // Object Values
    private LevelManager levelManager;
    
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        text = GetComponent<Text>();
        playerHealth = maxPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if the player health is above zero
        if(playerHealth <= 0)
        {
            levelManager.respawnPlayer();
        }

        text.text = "" + 3;
        
    }

    // Method that decreases player health on contact
    public void OnC(int damageToGive)
    {
        playerHealth -= damageToGive;
    }

    //public void FullHealth()
    //{
    //    playerHealth = maxPlayerHealth;
    //}
}
