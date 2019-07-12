using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private Image image;
    public string selectedItem;

    public Sprite herb;
    public Sprite boots;
    public Sprite hook;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        selectedItem = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(selectedItem.Equals("Herb"))
        {
            image.sprite = herb;
        }
        if(selectedItem.Equals("Boots"))
        {
            image.sprite = boots;
        }
        if(selectedItem.Equals("Hook"))
        {
            image.sprite = hook;
        }
    }

    public void setItem(string item)
    {
        selectedItem = item;
    }
}
