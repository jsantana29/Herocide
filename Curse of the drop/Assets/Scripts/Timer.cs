using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Variables for the starting and updating time.
    public Text timerText;
    private float startTime;

    void Start()
    {
        // Initializes the time at 0:0.00
        startTime = Time.deltaTime;
    }

    void Update ()
    {
        // Gives you the the time passed since the timer started.
        float t = Time.time - startTime;
        
        // Finds the current minute, second, and converts it to a string.
        string minutes = Mathf.Floor((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        
        // Sets the time in 0:0.00 format by concatenating the minutes and seconds.
        timerText.text = minutes + ":" + seconds;
    }
}