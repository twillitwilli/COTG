using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHitPlayer : MonoBehaviour
{
    private PlayerStats _playerStats;

    public string objectName;
    public float healthAdjustment;
    public bool randomAmount;
    public float minAmount, maxAmount;

    private void Start()
    {
        _playerStats = LocalGameManager.Instance.GetPlayerStats();
    }

    private void OnParticleCollision(GameObject other)
    {
        VRPlayerController player;
        if (other.gameObject.TryGetComponent<VRPlayerController>(out player))
        {
            if (!randomAmount) { _playerStats.AdjustHealth(healthAdjustment, objectName); }
            else
            {
                float randomValue = Random.Range(minAmount, maxAmount);
                _playerStats.AdjustHealth(randomValue, objectName);
            }
        }
    }
}
