using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BasicBeamAttack : MonoBehaviour
{
    [HideInInspector] 
    public VRPlayerController player;
    
    private ParticleHitEnemy particleDamager;
    private ParticleSystem particle;

    public void Awake()
    {
        particleDamager = GetComponent<ParticleHitEnemy>();
        particle = GetComponent<ParticleSystem>();
    }

    public async void Start()
    {
        await Task.Delay(100);

        StartDelay();
    }

    private void StartDelay()
    {
        particleDamager.healthAdjustment = (PlayerStats.Instance.AttackDamage() / -15);
        var particleMain = particle.main;
        particleMain.startSpeed = (10f + (0.5f * PlayerStats.Instance.data.rangeUpgrades));
    }
}
