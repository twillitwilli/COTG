using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayStyle : MonoBehaviour
{
    [SerializeField] 
    PlayerMenu menu;
    
    VRPlayer player;

    public GameObject 
        standingPlayerObjs, 
        sittingPlayerObjs;

    void Start()
    {
        player = LocalGameManager.Instance.player;
    }

    void OnEnable()
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
