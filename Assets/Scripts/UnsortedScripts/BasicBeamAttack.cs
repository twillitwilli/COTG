using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBeamAttack : MonoBehaviour
{
    [HideInInspector] public VRPlayerController player;
    private ParticleHitEnemy particleDamager;
    private ParticleSystem particle;

    private PlayerStats _playerStats;

    public void Awake()
    {
        particleDamager = GetComponent<ParticleHitEnemy>();
        particle = GetComponent<ParticleSystem>();
    }

    public void Start()
    {
        _playerStats = LocalGameManager.instance.GetPlayerStats();

        Invoke("StartDelay", 0.1f);
    }

    private void StartDelay()
    {
        particleDamager.healthAdjustment = (_playerStats.GetAttackDamage() / -15);
        var particleMain = particle.main;
        particleMain.startSpeed = (10f + (0.5f * _playerStats.GetRangeUpgrades()));
    }
}
