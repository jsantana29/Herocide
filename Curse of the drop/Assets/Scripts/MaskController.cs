using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskController : MonoBehaviour
{
    private Image image;
    public string selectedMask;

    public Sprite stamina;
    public Sprite invisibility;
    public Sprite god;
    public Sprite herb;
    public Sprite companion;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        selectedMask = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(selectedMask.Equals("Invisibility")){
            image.sprite = invisibility;
        }
        else if(selectedMask.Equals("Stamina")){
            image.sprite = stamina;
        }
        else if(selectedMask.Equals("God")){
            image.sprite = god;
        }
        else if(selectedMask.EndsWith("Companion")){
            image.sprite = companion;
        }
        else if(selectedMask.Equals("Herb"))
        {
            image.sprite = herb;
        }
    }

    public void setMask(string mask){
        selectedMask = mask;
    }
}
