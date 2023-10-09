using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    // --- FOR SORCERER ONLY ---
    VRPlayer _player;
    PlayerComponents _playerComponents;

    public bool canUseSpellCasting;
    public float 
        accelerationReq = 1f, 
        stoppingForce = 0.25f, 
        distanceReq,
        distanceBetweenHandAndChest;

    [SerializeField] 
    Transform[] handCenter; 

    [SerializeField] 
    Transform centerOfHands;

    PlayerMagicController magic;
    float castTime;
    float distanceForSpellCharge = 0.2f;
    GameObject chargingSpell;

    List<GameObject> magicFocusCharges = new List<GameObject>();

    void Awake()
    {
        _player = LocalGameManager.Instance.player;
        _playerComponents = _player.GetPlayerComponents();

        magic = MasterManager.playerMagicController;
        ResetSpellCharge();
    }

    void OnEnable()
    {
        canUseSpellCasting = true;
    }

    void LateUpdate()
    {
        if (canUseSpellCasting)
        {
            Vector3 centerPoint = (handCenter[0].position + handCenter[1].position) / 2;
            centerOfHands.position = centerPoint;

            if (PlayerStats.Instance.currentMagicFocus < PlayerStats.Instance.data.magicFocus)
            {
                float distance = Vector3.Distance(handCenter[0].position, handCenter[1].position);

                if (distance > distanceForSpellCharge)
                    ResetSpellCharge();

                else if (distance <= distanceForSpellCharge)
                    ChargeSpell(MagicController.Instance.magicIdx);
            }
        }
    }

    void ChargeSpell(int currentMagic)
    {
        if (!chargingSpell) 
        {
            GameObject newSpell = Instantiate(magic.chargedVisual[currentMagic]);
            newSpell.transform.SetParent(centerOfHands);
            ResetTransform(newSpell);
            chargingSpell = newSpell;
        }

        if (castTime > 0)
            castTime -= Time.deltaTime * 6;

        else if (castTime <= 0)
            SpellReady(currentMagic);
    }

    void SpellReady(int currentMagic)
    {
        for (int i = 0; i < 2; i++)
        {
            if (!_playerComponents.GetHand(i).GetSpellCastingForHands().spellReadyVisual)
                SpawnSpellCharges(i, currentMagic);

            else
                SetSpellFocus(_playerComponents.GetHand(i).GetSpellCastingForHands().spellReadyVisual.GetComponent<ParticleSystem>(), (int)(PlayerStats.Instance.data.magicFocus));

            _playerComponents.GetHand(i).GetSpellCastingForHands().magicActive = true;
        }

        ResetSpellCharge();
    }

    void SpawnSpellCharges(int hand, int currentMagic)
    {
        GameObject spellReady = Instantiate(magic.spellCharges[currentMagic]);
        magicFocusCharges.Add(spellReady);
        spellReady.transform.SetParent(_playerComponents.GetHand(hand).GetSpellCastingForHands().magicChargesSpawn);
        ResetTransform(spellReady);
        _playerComponents.GetHand(hand).GetSpellCastingForHands().spellReadyVisual = spellReady;
        SetSpellFocus(spellReady.GetComponent<ParticleSystem>(), Mathf.RoundToInt(PlayerStats.Instance.data.magicFocus));
    }

    void ResetTransform(GameObject obj)
    {
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localEulerAngles = new Vector3(0, 0, 0);
        obj.transform.localScale = new Vector3(1, 1, 1);
    }

    void SetSpellFocus(ParticleSystem magicVisual, int magicFocus)
    {
        var maxParticles = magicVisual.main;
        maxParticles.maxParticles = magicFocus;
    }

    void ResetSpellCharge()
    {
        if (chargingSpell)
            Destroy(chargingSpell);

        castTime = PlayerStats.Instance.data.attackCooldown;
    }

    public void CalibrateSettings()
    {
        distanceBetweenHandAndChest = Vector3.Distance(_playerComponents.GetOriginPoint(0).transform.position, _playerComponents.GetOriginPoint(2).transform.position) * 100;
        distanceBetweenHandAndChest -= (distanceBetweenHandAndChest * 0.5f);
    }

    void OnDisable()
    {
        canUseSpellCasting = false;

        foreach (GameObject obj in magicFocusCharges)
        {
            if (obj != null)
                Destroy(obj);
        }

        magicFocusCharges.Clear();
    }
}
