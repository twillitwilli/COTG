using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaff : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private PlayerStats _playerStats;
    private MagicController _magicController;
    private PlayerMagicController _playerMagicController;
    private GearController _gearController;
    private StaffMagicController _staffController;
    private VRPlayerController _player;

    [SerializeField] private BoxCollider _staffTrigger;
    [SerializeField] private bool _setAttackCooldown, _spellCharged;
    [SerializeField] private Transform _spellSpawn, _magicFocusSpawn, _magicCircleSpawn, _spellChargingSpawn;

    private GameObject _currentMagicCircle, _currentSpellChargeEffect;
    private float _cooldownTimer;
    private ParticleSystem _magicFocus;

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
        _playerStats = _gameManager.GetPlayerStats();
        _magicController = _gameManager.GetMagicController();
        _playerMagicController = MasterManager.playerMagicController;
        _gearController = _gameManager.GetGearController();
        _staffController = _gearController.GetStaffController();

        _player = _gameManager.player;
    }

    public void AdjustMagicFocus()
    {
        if (_magicFocus == null)
        {
            GameObject newParticles = Instantiate(_playerMagicController.spellCharges[_magicController.magicIdx], _magicFocusSpawn);
            _magicFocus = newParticles.GetComponent<ParticleSystem>();
        }

        var maxParticles = _magicFocus.main;
        maxParticles.maxParticles = Mathf.RoundToInt(_playerStats.GetMagicFocus());
    }

    public bool AttackCharge()
    {
        int currentSpell = _magicController.magicIdx;

        if (_setAttackCooldown)
        {
            if (_currentSpellChargeEffect == null) { _currentSpellChargeEffect = Instantiate(_playerMagicController.chargedVisual[currentSpell], _spellChargingSpawn); }
            
            _currentSpellChargeEffect.transform.SetParent(_spellChargingSpawn);
            _currentSpellChargeEffect.transform.localPosition = new Vector3(0, 0, 0);
            _currentSpellChargeEffect.transform.localEulerAngles = new Vector3(0, 0, 0);
            _currentSpellChargeEffect.transform.localScale = new Vector3(2, 2, 2);

            _cooldownTimer = _playerStats.GetAttackCooldown();
            _setAttackCooldown = false;
        }

        if (_cooldownTimer > 0) { _cooldownTimer -= Time.deltaTime; }
        else if (_cooldownTimer <= 0)
        {
            if (_currentSpellChargeEffect != null) { Destroy(_currentSpellChargeEffect); }

            _currentMagicCircle = Instantiate(_playerMagicController.magicCircles[currentSpell], _magicCircleSpawn);
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
            if (_currentMagicCircle != null) { Destroy(_currentMagicCircle); }
            GameObject newProjectile;
            switch (_magicController.GetCurrentCastingType())
            {
                case MagicController.CastingType.charge:
                    newProjectile = Instantiate(_playerMagicController.wizardChargedSpells[_magicController.magicIdx], _spellSpawn);

                    BasicProjectile chargedAttack = newProjectile.GetComponent<BasicProjectile>();
                    chargedAttack.player = _player;
                    chargedAttack.spawnParent = transform;

                    _spellCharged = false;
                    break;

                case MagicController.CastingType.beam:
                    newProjectile = Instantiate(_playerMagicController.wizardConstantSpells[_magicController.magicIdx], _spellSpawn);
                    break;

                case MagicController.CastingType.rapidFire:
                    newProjectile = Instantiate(_playerMagicController.wizardRapidFireSpells[_magicController.magicIdx], _spellSpawn);

                    BasicProjectile rapidFireAttack = newProjectile.GetComponent<BasicProjectile>();
                    rapidFireAttack.player = _player;
                    rapidFireAttack.spawnParent = transform;
                    _spellCharged = false;
                    break;
            }
        }

        if (_currentMagicCircle != null) { Destroy(_currentMagicCircle); }
        if (_currentSpellChargeEffect != null) { Destroy(_currentSpellChargeEffect); }

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
