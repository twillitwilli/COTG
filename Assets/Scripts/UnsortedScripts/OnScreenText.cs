using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenText : MonoBehaviour
{
    public Animator screenTextAnimator;
    public Text textBox;

    public void Start()
    {
        DisplayOff();
    }

    public void PrintText(string text, bool displayOffDelay)
    {
        textBox.text = text;
        DisplayOn();
        if (displayOffDelay) { Invoke("DisplayOff", 6); }
    }

    public void DisplayOn()
    {
        screenTextAnimator.Play("TurnOnOnScreenText");
    }

    public void DisplayOff()
    {
        screenTextAnimator.Play("TurnOffOnScreenText");
    }
}
