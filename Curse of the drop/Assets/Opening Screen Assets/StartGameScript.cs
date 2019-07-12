using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
}
