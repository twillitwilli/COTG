using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthModifier : MonoBehaviour
{
    public string objectName;
    public float healthValue;

    private PlayerStats _playerStats;

    private void Awake()
    {
        if (objectName == null) { objectName = "Object Not Named: " + gameObject.name; }
    }

    private void Start()
    {
        _playerStats = LocalGameManager.Instance.GetPlayerStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<VRPlayerController>())
        {
            _playerStats.AdjustHealth(healthValue, objectName);
        }
    }
}
