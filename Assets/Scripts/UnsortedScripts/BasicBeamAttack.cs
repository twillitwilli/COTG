using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BasicBeamAttack : MonoBehaviour
{
    public VRPlayer player { get; set; }
    
    ParticleHitEnemy _particleDamager;
    ParticleSystem _particle;

    public void Awake()
    {
        _particleDamager = GetComponent<ParticleHitEnemy>();
        _particle = GetComponent<ParticleSystem>();
    }

    public async void Start()
    {
        await Task.Delay(100);

        StartDelay();
    }

    void StartDelay()
    {
        _particleDamager.healthAdjustment = (PlayerStats.Instance.AttackDamage() / -15);
        var particleMain = _particle.main;
        particleMain.startSpeed = (10f + (0.5f * PlayerStats.Instance.data.rangeUpgrades));
    }
}
