using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodHandCatch : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            Debug.Log("Trigger box works");
            anim.SetTrigger("catch");
        }
    }
}
