using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QTArts.AbstractClasses;

public class GameTimer : MonoSingleton<GameTimer>
{
    TimeSpan timePlaying;
    bool timerGoing;
    float elapsedTime;

    public double currentTime { get; set; }
    public string currentTimeDisplay { get; set; }

    void Start()
    {
        timerGoing = false;
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            var t = timePlaying;
            var seconds = t.TotalSeconds;
            currentTime = seconds;
            if (seconds > 60)
            {
                if (seconds > 3600) currentTimeDisplay = timePlaying.ToString("hh':'mm':'ss");
                else currentTimeDisplay = timePlaying.ToString("mm':'ss");
            }
            else currentTimeDisplay = timePlaying.ToString("ss");
            yield return null;
        }
    }
}
