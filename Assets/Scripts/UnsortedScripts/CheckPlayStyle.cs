using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayStyle : MonoBehaviour
{
    [SerializeField] private PlayerMenu menu;
    private VRPlayerController player;

    public GameObject standingPlayerObjs;
    public GameObject sittingPlayerObjs;

    private void Start()
    {
        player = LocalGameManager.instance.player;
    }

    private void OnEnable()
    {
        if (player.playerStanding)
        {
            standingPlayerObjs.SetActive(true);
            sittingPlayerObjs.SetActive(false);
        }
        else
        {
            sittingPlayerObjs.SetActive(true);
            standingPlayerObjs.SetActive(false);
        }
    }
}
