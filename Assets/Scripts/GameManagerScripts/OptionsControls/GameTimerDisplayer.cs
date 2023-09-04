using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerDisplayer : MonoBehaviour
{
    [SerializeField] 
    private Text textBox;

    [HideInInspector]
    public bool runningTimer;

    public void Start()
    {
        if (!runningTimer)
            DisplayCurrentGameTime();
    }

    private void Update()
    {
        if (runningTimer)
            DisplayCurrentGameTime();
    }

    public void DisplayCurrentGameTime()
    {
        textBox.text = GameTimer.Instance.currentTimeDisplay;
    }
}
