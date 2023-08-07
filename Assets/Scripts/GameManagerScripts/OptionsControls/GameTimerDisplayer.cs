using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerDisplayer : MonoBehaviour
{
    private GameTimer _gameTimer;

    [SerializeField] private Text textBox;
    public bool runningTimer;

    public void Start()
    {
        _gameTimer = LocalGameManager.instance.GetGameTimer();
        if (!runningTimer) { DisplayCurrentGameTime(); }
    }

    private void Update()
    {
        if (runningTimer) { DisplayCurrentGameTime(); }
    }

    public void DisplayCurrentGameTime()
    {
        textBox.text = _gameTimer.currentTimeDisplay;
    }
}
