using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStatDisplay : MonoBehaviour
{
    private Text textBox;

    private void Awake()
    {
        textBox = GetComponent<Text>();
    }

    public void UpdateStateDisplay(string whatToDisplay)
    {
        textBox.text = whatToDisplay;
    }
}
