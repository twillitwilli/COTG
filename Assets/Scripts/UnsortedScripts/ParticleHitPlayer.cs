using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHitPlayer : MonoBehaviour
{
    public string objectName;
    public float healthAdjustment;
    public bool randomAmount;
    public float minAmount, maxAmount;

    private void OnParticleCollision(GameObject other)
    {
        VRPlayerController player;
        if (other.gameObject.TryGetComponent<VRPlayerController>(out player))
        {
            if (!randomAmount)
                PlayerStats.Instance.AdjustHealth(healthAdjustment, objectName);

            else
            {
                float randomValue = Random.Range(minAmount, maxAmount);
                PlayerStats.Instance.AdjustHealth(randomValue, objectName);
            }
        }
    }
}
