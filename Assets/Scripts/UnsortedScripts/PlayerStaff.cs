using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaff : MonoBehaviour
{
    [SerializeField] 
    private BoxCollider _staffTrigger;
    
    [SerializeField] 
    private bool _setAttackCooldown, _spellCharged;
    
    [SerializeField] 
    private Transform _spellSpawn, _magicFocusSpawn, _magicCircleSpawn, _spellChargingSpawn;

    private GameObject _currentMagicCircle, _currentSpellChargeEffect;
    private float _cooldownTimer;
    private ParticleSystem _magicFocus;

    public void AdjustMagicFocus()
    {
        if (_magicFocus == null)
        {
            GameObject newParticles = Instantiate(MasterManager.Instance.magicController.spellCharges[MagicController.Instance.magicIdx], _magicFocusSpawn);
            _magicFocus = newParticles.GetComponent<ParticleSystem>();
        }

        var maxParticles = _magicFocus.main;
        maxParticles.maxParticles = Mathf.RoundToInt(PlayerStats.Instance.GetMagicFocus());
    }

    public bool AttackCharge()
    {
        int currentSpell = MagicController.Instance.magicIdx;

        if (_setAttackCooldown)
        {
            if (_currentSpellChargeEffect == null) { _currentSpellChargeEffect = Instantiate(MasterManager.Instance.magicController.chargedVisual[currentSpell], _spellChargingSpawn); }
            
            _currentSpellChargeEffect.transform.SetParent(_spellChargingSpawn);
            _currentSpellChargeEffect.transform.localPosition = new Vector3(0, 0, 0);
            _currentSpellChargeEffect.transform.localEulerAngles = new Vector3(0, 0, 0);
            _currentSpellChargeEffect.transform.localScale = new Vector3(2, 2, 2);

            _cooldownTimer = PlayerStats.Instance.GetAttackCooldown();
            _setAttackCooldown = false;
        }

        if (_cooldownTimer > 0)
            _cooldownTimer -= Time.deltaTime;

        else if (_cooldownTimer <= 0)
        {
            if (_currentSpellChargeEffect != null)
                Destroy(_currentSpellChargeEffect);

            _currentMagicCircle = Instantiate(MasterManager.Instance.magicController.magicCircles[currentSpell], _magicCircleSpawn);
            _currentMagicCircle.transform.localScale = new Vector3(3, 3, 3);

            _cooldownTimer = 0;
            return true;
        }
        return false;
    }

    public void ShootProjectile()
    {
        if (_spellCharged)
        {
            if (_currentMagicCircle != null)
                Destroy(_currentMagicCircle);

            GameObject newProjectile;
            switch (MagicController.Instance.currentCastingType)
            {
                case MagicController.CastingType.charge:
                    newProjectile = Instantiate(MasterManager.Instance.magicController.wizardChargedSpells[MagicController.Instance.magicIdx], _spellSpawn);

                    BasicProjectile chargedAttack = newProjectile.GetComponent<BasicProjectile>();
                    chargedAttack.player = LocalGameManager.Instance.player;
                    chargedAttack.spawnParent = transform;

                    _spellCharged = false;
                    break;

                case MagicController.CastingType.beam:
                    newProjectile = Instantiate(MasterManager.Instance.magicController.wizardConstantSpells[MagicController.Instance.magicIdx], _spellSpawn);
                    break;

                case MagicController.CastingType.rapidFire:
                    newProjectile = Instantiate(MasterManager.Instance.magicController.wizardRapidFireSpells[MagicController.Instance.magicIdx], _spellSpawn);

                    BasicProjectile rapidFireAttack = newProjectile.GetComponent<BasicProjectile>();
                    rapidFireAttack.player = LocalGameManager.Instance.player;
                    rapidFireAttack.spawnParent = transform;
                    _spellCharged = false;
                    break;
            }
        }

        if (_currentMagicCircle != null)
            Destroy(_currentMagicCircle);

        if (_currentSpellChargeEffect != null)
            Destroy(_currentSpellChargeEffect);

        _setAttackCooldown = true;
    }

    public void GrabStaff(Transform spawnLocation)
    {
        _staffTrigger.enabled = false;
        transform.SetParent(spawnLocation);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
