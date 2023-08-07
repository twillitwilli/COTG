using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapZoom : MonoBehaviour
{
    public float zoomAmount;
    private Camera _mapCamera;

    private void Awake()
    {
        _mapCamera = LocalGameManager.instance.GetMapCamera();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finger")) { AdjustZoom(zoomAmount); }
    }

    public void AdjustZoom(float zoomAdjustment)
    {
        _mapCamera.orthographicSize += zoomAdjustment;
        if (_mapCamera.orthographicSize > 1000) { _mapCamera.orthographicSize = 1000; }
        else if (_mapCamera.orthographicSize < 100) { _mapCamera.orthographicSize = 100; }
    }
}
