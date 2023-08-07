using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RealTimeDisplay : MonoBehaviour
{
    [SerializeField] private PlayerMenu playerMenu;
    [SerializeField] private Text textBox;
    [SerializeField] private GameObject currentTimeObject;

    private VRPlayerController _player;
    private CurrentTime currentTime;

    private void Awake()
    {
        if (!CurrentTime.instance) { Instantiate(currentTimeObject); }
    }

    private void Start()
    {
        _player = LocalGameManager.instance.player;
        currentTime = CurrentTime.instance;
    }

    private void LateUpdate()
    {
        if (!_player.militaryTime) { TweleveHourClock(); }
        else { TwentyFourHourClock(); }
    }

    private void TweleveHourClock()
    {
        if (currentTime.hour <= 11 && currentTime.hour > 0)
        {
            if (currentTime.minutes <= 9) { textBox.text = "" + currentTime.hour + ":0" + currentTime.minutes + "AM"; }
            else if (currentTime.minutes >= 10) { textBox.text = "" + currentTime.hour + ":" + currentTime.minutes + "AM"; }
        }
        else if (currentTime.hour >= 13)
        {
            int alteredHour = currentTime.hour - 12;
            if (currentTime.minutes <= 9) { textBox.text = "" + alteredHour + ":0" + currentTime.minutes + "PM"; }
            else if (currentTime.minutes >= 10) { textBox.text = "" + alteredHour + ":" + currentTime.minutes + "PM"; }
        }
        else if (currentTime.hour == 0)
        {
            int alteredHour = 12;
            if (currentTime.minutes <= 9) { textBox.text = "" + alteredHour + ":0" + currentTime.minutes + "AM"; }
            else if (currentTime.minutes >= 10) { textBox.text = "" + alteredHour + ":" + currentTime.minutes + "AM"; }
        }
        else if (currentTime.hour == 12)
        {
            if (currentTime.minutes <= 9) { textBox.text = "" + currentTime.hour + ":0" + currentTime.minutes + "PM"; }
            else if (currentTime.minutes >= 10) { textBox.text = "" + currentTime.hour + ":" + currentTime.minutes + "PM"; }
        }
    }

    private void TwentyFourHourClock()
    {
        if (currentTime.minutes <= 9) { textBox.text = "" + currentTime.hour + ":0" + currentTime.minutes; }
        else if (currentTime.minutes >= 10) { textBox.text = "" + currentTime.hour + ":" + currentTime.minutes; }
    }
}
