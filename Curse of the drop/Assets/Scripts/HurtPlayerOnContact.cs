using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnContact : MonoBehaviour
{
    // int values
    public int damageToGive;

    private AudioSource audio;
    
    
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If the collider is a player, then run the respawn method to set them back to current checkpoint
        if (other.tag == "Player")
        {
            audio.Play();
            HealthManager.HurtPlayer(damageToGive);

            var player = other.GetComponent<PlayerInput>();
            player.ActivateKBToReset = true;

            // Checks player's position in relation to the enemy
            if (other.transform.position.x < transform.position.x)
            {
                player.knockFromRight = true;
            }
            else
            {
                player.knockFromRight = false;
            }
        }
    }
}
