using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    
    public float fallHeight;

    public int fallDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "ground"){
            if(Mathf.Abs (other.relativeVelocity.y) > fallHeight * 2){
                Debug.Log("Fall damage x 3");
                HealthManager.HurtPlayer(fallDamage * 3);
                GetComponent<AudioSource>().Play();
            }
            else if(Mathf.Abs (other.relativeVelocity.y) > fallHeight * 1.5){
                Debug.Log("Fall damage x 2");
                HealthManager.HurtPlayer(fallDamage * 2);
                GetComponent<AudioSource>().Play();
            }
            else if(Mathf.Abs (other.relativeVelocity.y) > fallHeight){
                Debug.Log("Fall damage x 1");
                HealthManager.HurtPlayer(fallDamage);
                GetComponent<AudioSource>().Play();
            }

        }

        // if (Mathf.Abs (other.relativeVelocity.y) > fallHeight && other.gameObject.tag == "ground") {

		// 	Debug.Log("Fall damage");
        //     HealthManager.HurtPlayer(fallDamage);
		// }
    }

}

    