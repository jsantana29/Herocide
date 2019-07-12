using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private PlayerInput player;
    private HealthManager health;
    private static PlayerMove playerMove;

    private GrapplingHook grapple;


    public int healthToGive;

    public bool usedHerb;

    public int herbs;



    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerInput>();
        health = FindObjectOfType<HealthManager>();
        grapple = FindObjectOfType<GrapplingHook>();
        playerMove = FindObjectOfType<PlayerMove>();
        herbs = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method that activates the item
    public void itemActivation(string item)
    {
        if (item.Equals("Herb") && herbs > 0)
        {
            HealthManager.healthRestore(healthToGive);
            Debug.Log("Health:" + healthToGive );
            herbs--;
        }

        if(item.Equals("Hook")){
            grapple.grapple();
        }

        if(item.Equals("Boots")){
            playerMove.isSpedUp(true);
        }
    }

    public void addHerb(){
        herbs++;
    }
}
