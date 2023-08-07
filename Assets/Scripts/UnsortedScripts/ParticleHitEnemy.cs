using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHitEnemy : MonoBehaviour
{
    public float healthAdjustment;
    public bool fromPlayer;
    [HideInInspector] public VRPlayerController player;

    private void OnParticleCollision(GameObject other)
    {
        
        if (other.GetComponent<EnemyController>())
        {
            if (fromPlayer) { other.GetComponent<EnemyController>().enemyHealth.AdjustHealth(healthAdjustment, false); }
            else { other.GetComponent<EnemyController>().enemyHealth.AdjustHealth(healthAdjustment, false); }
        }
        if (other.GetComponent<BreakableObject>()) { other.GetComponent<BreakableObject>().BreakObjectWithAttack(); }
    }
}
