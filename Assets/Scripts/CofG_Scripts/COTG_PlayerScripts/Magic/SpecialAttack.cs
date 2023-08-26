using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    private PlayerStats _playerStats;

    [SerializeField] private MagicController.SpecialEffects _specialEffect;
    [SerializeField] private ParticleHitEnemy _particleHitEnemy;

    public EnemyHealthModifier enemyHealthModifier;

    private void Start()
    {
        _playerStats = LocalGameManager.Instance.GetPlayerStats();
    }

    public virtual void Explosion()
    {
        float explosionDamage = _playerStats.GetAttackDamage();
        enemyHealthModifier.healthValue = (-1 * (explosionDamage + (explosionDamage * 0.5f)));
        Destroy(gameObject, 6);
    }

    public virtual void Rain()
    {
        transform.localEulerAngles = new Vector3(-90, 0, 0);
        _particleHitEnemy.healthAdjustment = (-1 * (_playerStats.GetAttackDamage() * 0.65f));
        Destroy(gameObject, _playerStats.GetMagicFocus() * 2);
    }

    public virtual void Summoning()
    {

    }

    public virtual void Burst()
    {
        Destroy(gameObject, 6);
    }

    public virtual void Pillar()
    {
        Destroy(gameObject, 6);
    }

    public virtual void AOEGround()
    {
        Destroy(gameObject, _playerStats.GetMagicFocus() * 2);
    }
}
