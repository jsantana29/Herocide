using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        //Finds the object that contains a LevelManager script
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If the collider is a player, then run the respawn method to set them back to current checkpoint
        if(other.tag == "Player"){
            Debug.Log("Player is supposed to die");
            levelManager.respawnPlayer();
        }
    }
}
