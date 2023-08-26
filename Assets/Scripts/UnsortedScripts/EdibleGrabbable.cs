using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleGrabbable : MonoBehaviour
{
    private PlayerStats _playerStats;

    public enum EdibleEffect { health }
    public EdibleEffect effect;
    public float value;

    private void Start()
    {
        _playerStats = LocalGameManager.Instance.GetPlayerStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Head"))
        {
            switch (effect)
            {
                case EdibleEffect.health:
                    _playerStats.AdjustHealth(value, "Food Poisoning");
                    break;
            }
        }
    }
}
