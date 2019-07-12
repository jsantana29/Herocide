using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    private Text txtBox;
    private string txt;

    public float defaultBlinkTime = 0.5f;
    private float blinkTimer;

    void Start()
    {
        txtBox = GetComponent<Text>();
        txt = txtBox.text;
        blinkTimer = defaultBlinkTime;
    }

    void Update()
    {
        if (blinkTimer > 0.0f)
        {
            blinkTimer -= Time.deltaTime;
        }

        if (blinkTimer <= 0.0f)
        {
            SwitchText();
            blinkTimer = defaultBlinkTime;
        }
    }

    private void SwitchText()
    {
        if (txtBox.text == txt)
        {
            txtBox.text = "";
        }
        else
        {
            txtBox.text = txt;
        }
    }
}
