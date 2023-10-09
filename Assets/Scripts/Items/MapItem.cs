using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour
{
    public GameObject gameTimer;
    public Transform timerSpawn;

    public VRPlayer player { get; set; }
    public GameObject timer { get; set; }

    Camera _mapCamera;

    void Awake()
    {
        _mapCamera = LocalGameManager.Instance.GetMapCamera();
    }

    void Start()
    {
        _mapCamera.enabled = true;
        _mapCamera.orthographicSize = 250;
        timer = Instantiate(gameTimer, timerSpawn);
    }

    void OnDestroy()
    {
        _mapCamera.enabled = false;
    }
}
