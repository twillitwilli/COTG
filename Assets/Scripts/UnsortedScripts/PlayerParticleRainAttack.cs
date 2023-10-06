using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerParticleRainAttack : MonoBehaviour
{
    public ParticleSystem[] particlesEffectedByAttack;

    public async void Start()
    {
        await Task.Delay(100);

        StartDelay();    
    }

    private void StartDelay()
    {
        ParticleAttackChanges();
        ParticleRangeChanges();
    }

    public virtual void ParticleAttackChanges()
    {
        for (int i = 0; i < particlesEffectedByAttack.Length; i++)
        {
            if (particlesEffectedByAttack[i].gameObject.GetComponent<ParticleHitEnemy>())
                particlesEffectedByAttack[i].gameObject.GetComponent<ParticleHitEnemy>().healthAdjustment = (PlayerStats.Instance.AttackDamage() / -5);
        }

        float scaleWithAttack = (0.75f + (0.1f * PlayerStats.Instance.data.damageUpgrades));
        particlesEffectedByAttack[0].transform.localScale = new Vector3(scaleWithAttack, scaleWithAttack, scaleWithAttack);
    }

    public virtual void ParticleRangeChanges()
    {
        float scaleWithRange = (0.75f + (0.1f * PlayerStats.Instance.data.rangeUpgrades));
        transform.localScale = new Vector3(scaleWithRange, scaleWithRange, scaleWithRange);
    }
}
