using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleRainAttack : MonoBehaviour
{
    public ParticleSystem[] particlesEffectedByAttack;

    [SerializeField] private PlayerStats _playerStats;

    public void Start()
    {
        _playerStats = LocalGameManager.instance.GetPlayerStats();
        Invoke("StartDelay", 0.1f);
    }

    private void StartDelay()
    {
        ParticleAttackChanges();
        ParticleRangeChanges();
    }

    public virtual void ParticleAttackChanges()
    {
        for(int i = 0; i < particlesEffectedByAttack.Length; i++)
        {
            if(particlesEffectedByAttack[i].gameObject.GetComponent<ParticleHitEnemy>()) 
            {
                particlesEffectedByAttack[i].gameObject.GetComponent<ParticleHitEnemy>().healthAdjustment = (_playerStats.GetAttackDamage() / -5);
            }
        }
        float scaleWithAttack = (0.75f + (0.1f * _playerStats.GetDamageUpgrades()));
        particlesEffectedByAttack[0].transform.localScale = new Vector3(scaleWithAttack, scaleWithAttack, scaleWithAttack);
    }

    public virtual void ParticleRangeChanges()
    {
        float scaleWithRange = (0.75f + (0.1f * _playerStats.GetRangeUpgrades()));
        transform.localScale = new Vector3(scaleWithRange, scaleWithRange, scaleWithRange);
    }
}
