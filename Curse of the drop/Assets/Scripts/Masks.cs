using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masks : MonoBehaviour
{
    private PlayerInput player; 
    private PlayerClimb climb;
    private HealthManager health;

    public int healthToGive;

    public float invisibilityActiveTime;
    public float invisibilityCooldown;
    public float invisibilityTimer;

    public float staminaActiveTime;
    public float staminaCooldown;
    public float staminaTimer;

    public bool invisibleOnCooldown;
    public bool usedInvis;
    public bool staminaOnCooldown;
    public bool usedStamina;
    public bool usedHerb;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerInput>();
        climb = GetComponent<PlayerClimb>();
        health = GetComponent<HealthManager>();

        usedInvis = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(usedInvis){
            invisibilityTimeCheck();
        }

        if(invisibleOnCooldown){
            invisibilityCooldownCheck();
        }

        // if(usedStamina){
        //     staminaTimeCheck();
        // }

        // if(staminaOnCooldown){
        //     staminaCooldownCheck();
        // }

 
    }

    //This is where the mask power is activated
    public void maskPower(string mask){
        if(mask.Equals("Invisibility")){
            if(!invisibleOnCooldown && !usedInvis){
                usedInvis = true;
                
                makeInvisible();
                Debug.Log("invisible");
            }
            else if(!invisibleOnCooldown && usedInvis){
                usedInvis = false;
                
                makeVisible();
                Debug.Log("not invisible");
                invisibilityTimer = 0;
                invisibleOnCooldown = true;
            }
            
        }
        else if(mask.Equals("Frenemy")){
            

            
        }
        else if(mask.Equals("God")){

        }
    }

    public void makeInvisible(){
        // store a reference to the SpriteRenderer on the current GameObject
        SpriteRenderer spRend = player.GetComponent<SpriteRenderer>();
        // copy the SpriteRenderer's color property
        // change the SpriteRenderer's color property to match the copy with the altered alpha value
                
        Color col = spRend.color;;
        col.a = 0.3f; // 0.5f = half transparent
        spRend.color = col;
        player.GetComponent<Animator>().SetBool("stealth", true);
        // change col's alpha value (0 = invisible, 1 = fully opaque)
    }

     public void makeVisible(){
        // store a reference to the SpriteRenderer on the current GameObject
        SpriteRenderer spRend = player.GetComponent<SpriteRenderer>();
        // copy the SpriteRenderer's color property
        // change the SpriteRenderer's color property to match the copy with the altered alpha value
                
        Color col = spRend.color;;
        col.a = 1.0f; // 0.5f = half transparent
        spRend.color = col;
        player.GetComponent<Animator>().SetBool("stealth", false);
        // change col's alpha value (0 = invisible, 1 = fully opaque)
    }

    public void invisibilityTimeCheck(){
        invisibilityTimer += Time.deltaTime;

        if(invisibilityTimer > invisibilityActiveTime){
            usedInvis = false;
            invisibleOnCooldown = true;
            invisibilityTimer = 0;
            makeVisible();
        }
    }

    public void invisibilityCooldownCheck(){
        invisibilityTimer += Time.deltaTime;

        if(invisibilityTimer > invisibilityCooldown){
            invisibleOnCooldown = false;
            invisibilityTimer = 0;
        }
    }

    public void staminaTimeCheck(){
        staminaTimer += Time.deltaTime;

        if(staminaTimer > staminaActiveTime){
            usedStamina = false;
            staminaOnCooldown = true;
            staminaTimer = 0;
            climb.regulateStamina();
        }
    }

    public void staminaCooldownCheck(){
        staminaTimer += Time.deltaTime;

        if(staminaTimer > staminaCooldown){
            staminaOnCooldown = false;
            staminaTimer = 0;
        }
    }

    public bool getInvis(){
        return usedInvis;
    }
}
