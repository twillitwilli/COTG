using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour
{
    public GameObject gameTimer;
    public Transform timerSpawn;
    [HideInInspector] public VRPlayerController player;
    [HideInInspector] public GameObject timer;
    private Camera _mapCamera;

    private void Awake()
    {
        _mapCamera = LocalGameManager.instance.GetMapCamera();
    }

    private void Start()
    {
        _mapCamera.enabled = true;
        _mapCamera.orthographicSize = 250;
        timer = Instantiate(gameTimer, timerSpawn);
    }

    private void OnDestroy()
    {
        _mapCamera.enabled = false;
    }
}
