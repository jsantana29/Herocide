using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string levelToLoad;
    public bool hasRune;

    public TextMesh text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" && hasRune){
            SceneManager.LoadScene(levelToLoad);
        }
        else if(!hasRune){
            text.gameObject.SetActive(true);
        }
    }

    public void setRune(){
        hasRune = true;
    }
}
