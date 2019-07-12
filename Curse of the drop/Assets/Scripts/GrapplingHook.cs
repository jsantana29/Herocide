using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public Transform firePoint;
    public GameObject hook;

    public int hookShots;
    public bool canFire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //The following if statements are rotating the fire point based on what side the player is facing and what direction is the player holding
        if(transform.localScale.x < 0){
            //firePoint.transform.rotation = Quaternion.Euler(0,0, 180);
            firePoint.transform.rotation = Quaternion.Euler(0,0, 0);
        }

        if(transform.localScale.x > 0){
            //firePoint.transform.rotation = Quaternion.Euler(0,0, 0);
            firePoint.transform.rotation = Quaternion.Euler(0,0, 180);
        }

        // if(Input.GetButtonDown("Hook") && hookShots > 0 && canFire){
            
            

        //     Debug.Log("shoot");
        //     //Applies fire point rotation to the instantiation of the object
        //     grapple();
        //     //Decreases amount of hooks you can shoot
        //     hookShots--;
        // }

            if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0){
                firePoint.transform.rotation = Quaternion.Euler(0,0, 45);
            }
            else if(Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0){
                firePoint.transform.rotation = Quaternion.Euler(0,0, 135);
            }
            else if(Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0){
                firePoint.transform.rotation = Quaternion.Euler(0,0, 225);
            }
            else if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0){
                firePoint.transform.rotation = Quaternion.Euler(0,0, 315);
            }
            else if(Input.GetAxisRaw("Vertical") > 0){
                firePoint.transform.rotation = Quaternion.Euler(0,0, 90);
            }
            else if(Input.GetAxisRaw("Vertical") < 0){
                firePoint.transform.rotation = Quaternion.Euler(0,0, -90);
            }

        
    }

    public void grapple(){
        //Creates the hook
        if(hookShots > 0){
            Instantiate(hook, firePoint.position, firePoint.rotation);
            hookShots--;
        }
        
    }

    public void setShots(){
        hookShots = 3;
    }
}
