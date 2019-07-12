using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Creates a score
    public static int score; 
    
    // Creates text to add the score to
    Text text; 

    // What comes up upon booting the game
    void Start() 
    {
        // Initializes the text object as a score of 0
        text = GetComponent<Text>();
        score = 0;
    }

    // Changes made per frame
    void Update()
    {
        // Makes sure score isn't negative
        if (score < 0)
            score = 0;

        // Sets the text box to the current score

        //Debug.Log("Score is supposed to update");
        text.text = "" + score; 
    }

    // Sets the points pending to add to the actual score
    public static void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        Debug.Log("Score is supposed to update");
    }

    // Resets the score
    public static void Reset()
    {
        score = 0;
    }
}