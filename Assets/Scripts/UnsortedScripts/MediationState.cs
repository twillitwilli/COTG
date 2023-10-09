using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediationState : MonoBehaviour
{
    VRPlayer _player;
    PlayerComponents _playerComponents;

    public bool playerMeditating { get; set; }

    public GameObject[] magicCircles;
    private Vector3 previousHeadPosition;
    private Animator animator;

    public float distanceOfHeadPos;

    //timer
    TimeSpan timePlaying;
    bool timerGoing, timerStarted, mindFocused;
    float elapsedTime;

    public double currentTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        _player = LocalGameManager.Instance.player;
        _playerComponents = _player.GetPlayerComponents();
        previousHeadPosition = _player.head.transform.position;
        timerGoing = false;
    }

    void LateUpdate()
    {
        if (playerMeditating)
        {
            float distance = Vector3.Distance(_player.head.transform.position, previousHeadPosition) * 100000;
            distanceOfHeadPos = distance;
            if (_player.isCrouched && !timerStarted && distance < 10) { MeditationController(); }
            else if (distance > 10 || !_player.isCrouched) { Debug.Log("Broke Focus Distance = " + distance); BrokeFocus(); }
            previousHeadPosition = _player.head.transform.position;
        }
    }

    public void MeditationController()
    {
        BeginTimer();
        timerStarted = true;
    }

    public void FocusingTheMind()
    {
        _playerComponents.onScreenText.PrintText("Focus The Mind", true);
        animator.Play("MeditationFloatingLevel1");
        magicCircles[0].SetActive(true);
        magicCircles[1].SetActive(false);
        magicCircles[2].SetActive(false);
    }

    public void FlowLikeWater()
    {
        _playerComponents.onScreenText.PrintText("Flow Like Water", true);
        animator.Play("MeditationFloatingLevel2");
        magicCircles[0].SetActive(false);
        magicCircles[1].SetActive(true);
        magicCircles[2].SetActive(false);
    }

    public void Transcendence()
    {
        _playerComponents.onScreenText.PrintText("Transcendence", true);
        animator.Play("MeditationFloatingTop");
        magicCircles[0].SetActive(false);
        magicCircles[1].SetActive(false);
        magicCircles[2].SetActive(true);
    }

    public void BrokeFocus()
    {
        _playerComponents.onScreenText.PrintText("Focus Broken", true);
        animator.Play("MeditationFloatingCancel");
        mindFocused = false;
        StopTimer();
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
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
            if (!mindFocused && seconds > 30 && seconds < 32)
            {
                FocusingTheMind();
                mindFocused = true;
            }
            yield return null;
        }
    }

    public void StopTimer()
    {
        timerStarted = false;
    }
}
