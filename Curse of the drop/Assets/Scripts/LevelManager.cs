using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private PlayerInput player;
    private HealthManager health;
    public GameObject currentCheckpoint;

    public float respawnDelay;

    public string levelToLoad;
   
    // Start is called before the first frame update
    void Start()
    {
        //Finds object with the PlayerInput, HealthManager scripts
        player = FindObjectOfType<PlayerInput>();
        health = FindObjectOfType<HealthManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void respawnPlayer(){
        Debug.Log("Player is supposed to respawn");
        StartCoroutine("respawnTimer");
        
        //Sets player position to the checkpoint position
        
    }

    public IEnumerator respawnTimer(){

        darkSouls();
        player.GetComponent<Animator>().SetTrigger("dead");
        player.GetComponent<Animator>().SetBool("isDead",true);
        player.enabled = false;
        //health.FullHealth();
        yield return new WaitForSeconds(respawnDelay);
        // player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        // player.transform.position = currentCheckpoint.transform.position;
        SceneManager.LoadScene(levelToLoad);
       
        player.GetComponent<Animator>().SetTrigger("respawn");
        player.GetComponent<Animator>().SetBool("isDead", false);
        player.enabled = true;
    }

    public void darkSouls(){
        //Plays Dark Souls death theme on respawn
        GetComponent<AudioSource>().Play();
    }


}
