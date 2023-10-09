using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHitPlayer : MonoBehaviour
{
    public string objectName;
    public float healthAdjustment;

    public bool randomAmount;

    public float 
        minAmount, 
        maxAmount;

    void OnParticleCollision(GameObject other)
    {
        VRPlayer player;
        if (other.gameObject.TryGetComponent<VRPlayer>(out player))
        {
            if (!randomAmount)
                PlayerStats.Instance.Damage(healthAdjustment);

            else
            {
                float randomValue = Random.Range(minAmount, maxAmount);
                PlayerStats.Instance.Damage(randomValue);
            }
        }
    }
}
