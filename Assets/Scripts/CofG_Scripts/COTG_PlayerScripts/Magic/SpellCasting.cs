using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    // --- FOR SORCERER ONLY ---
    private VRPlayerController _player;
    private PlayerComponents _playerComponents;
    private PlayerStats _playerStats;
    private MagicController _magicController;

    public bool canUseSpellCasting;
    public float accelerationReq = 1f, stoppingForce = 0.25f, distanceReq;
    public float distanceBetweenHandAndChest;
    [SerializeField] private Transform[] handCenter; 
    [SerializeField] private Transform centerOfHands;

    private PlayerMagicController magic;
    private float castTime;
    private float distanceForSpellCharge = 0.2f;
    private GameObject chargingSpell;
    private List<GameObject> magicFocusCharges = new List<GameObject>();

    private void Awake()
    {
        _player = LocalGameManager.Instance.player;
        _playerComponents = _player.GetPlayerComponents();
        _playerStats = LocalGameManager.Instance.GetPlayerStats();
        _magicController = LocalGameManager.Instance.GetMagicController();

        magic = MasterManager.playerMagicController;
        ResetSpellCharge();
    }

    private void OnEnable()
    {
        canUseSpellCasting = true;
    }

    private void LateUpdate()
    {
        if (canUseSpellCasting)
        {
            Vector3 centerPoint = (handCenter[0].position + handCenter[1].position) / 2;
            centerOfHands.position = centerPoint;
            if (_playerStats.GetCurrentMagicFocus() < _playerStats.GetMagicFocus())
            {
                float distance = Vector3.Distance(handCenter[0].position, handCenter[1].position);

                if (distance > distanceForSpellCharge) { ResetSpellCharge(); }
                else if (distance <= distanceForSpellCharge) { ChargeSpell(_magicController.magicIdx); }
            }
        }
    }

    private void ChargeSpell(int currentMagic)
    {
        if (!chargingSpell) 
        {
            GameObject newSpell = Instantiate(magic.chargedVisual[currentMagic]);
            newSpell.transform.SetParent(centerOfHands);
            ResetTransform(newSpell);
            chargingSpell = newSpell;
        }
        if (castTime > 0) { castTime -= Time.deltaTime * 6; }
        else if (castTime <= 0) { SpellReady(currentMagic); }
    }

    private void SpellReady(int currentMagic)
    {
        for (int i = 0; i < 2; i++)
        {
            if (!_playerComponents.GetHand(i).GetSpellCastingForHands().spellReadyVisual) { SpawnSpellCharges(i, currentMagic); }
            else SetSpellFocus(_playerComponents.GetHand(i).GetSpellCastingForHands().spellReadyVisual.GetComponent<ParticleSystem>(), Mathf.RoundToInt(_playerStats.GetMagicFocus()));

            _playerComponents.GetHand(i).GetSpellCastingForHands().magicActive = true;
        }

        ResetSpellCharge();
    }

    private void SpawnSpellCharges(int hand, int currentMagic)
    {
        GameObject spellReady = Instantiate(magic.spellCharges[currentMagic]);
        magicFocusCharges.Add(spellReady);
        spellReady.transform.SetParent(_playerComponents.GetHand(hand).GetSpellCastingForHands().magicChargesSpawn);
        ResetTransform(spellReady);
        _playerComponents.GetHand(hand).GetSpellCastingForHands().spellReadyVisual = spellReady;
        SetSpellFocus(spellReady.GetComponent<ParticleSystem>(), Mathf.RoundToInt(_playerStats.GetMagicFocus()));
    }

    private void ResetTransform(GameObject obj)
    {
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localEulerAngles = new Vector3(0, 0, 0);
        obj.transform.localScale = new Vector3(1, 1, 1);
    }

    private void SetSpellFocus(ParticleSystem magicVisual, int magicFocus)
    {
        var maxParticles = magicVisual.main;
        maxParticles.maxParticles = magicFocus;
    }

    private void ResetSpellCharge()
    {
        if (chargingSpell) { Destroy(chargingSpell); }
        castTime = _playerStats.GetAttackCooldown();
    }

    public void CalibrateSettings()
    {
        distanceBetweenHandAndChest = Vector3.Distance(_playerComponents.GetOriginPoint(0).transform.position, _playerComponents.GetOriginPoint(2).transform.position) * 100;
        distanceBetweenHandAndChest -= (distanceBetweenHandAndChest * 0.5f);
    }

    private void OnDisable()
    {
        canUseSpellCasting = false;
        foreach (GameObject obj in magicFocusCharges)
        {
            if (obj != null) { Destroy(obj); }
        }
        magicFocusCharges.Clear();
    }
}
