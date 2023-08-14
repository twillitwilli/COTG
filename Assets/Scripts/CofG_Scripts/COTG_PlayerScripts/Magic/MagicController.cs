using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    //CLASS SELECTION
    public enum ClassType { none = 0, Wizard = 1, Conjurer = 2, Sorcerer = 3, Mage = 4, Enchanter = 5, Warlock = 6, Witch = 7, Tarot = 8 }
    [SerializeField] private ClassType _currentClass;

    //MAGIC SELECTION
    [System.Flags] public enum MagicType { arcane = 1, fire = 2, water = 4, earth = 8, dark = 16, light = 32, blood = 64, cupcakes = 128 }
    [SerializeField] private MagicType _currentMagic;
    public string magicName { get; private set; }
    public int magicIdx { get; private set; }

    //STATUS
    [System.Flags] public enum StatusEffects { none = 0, burning = 1, blinded = 2, frozen = 4, electrocuted = 8, slowed = 16, rooted = 32, lifeDraining = 64, poisoned = 128 }
    [SerializeField] private StatusEffects _currentStatusEffect;
    [SerializeField] private bool _hasStatusEffect;
    
    //DASH EFFECTS
    [System.Flags] public enum DashEffects { none = 0, dashAOETrail = 1, teleportBurst = 2, dashPillars = 4 }
    [SerializeField] private DashEffects _currentDashEffects;

    //COLLISION EFFECTS
    public enum CollisionEffects { none = 0, peircing = 1, bouncing = 2, split = 3 }
    [SerializeField] private CollisionEffects _currentCollisionEffects;

    //SPECIAL EFFECTS
    [System.Flags] public enum SpecialEffects { none = 0, explosion = 1, rain = 2, summoning = 4, burst = 8, pillar = 16, AOEGround = 32 }
    [SerializeField] private SpecialEffects _currentSpecialEffect;

    //CASTING TYPE
    public enum CastingType { charge, rapidFire, beam}
    [SerializeField] private CastingType _currentCastingType;
    [SerializeField] private bool _controllabeAttack;

    //PLAYER SUMMONED MINION
    private GameObject _currentMinion;


    // ------------------------------------------------------------------------------------


    //GET REFERENCES
    public ClassType GetClassType() { return _currentClass; }
    public MagicType GetMagicType() { return _currentMagic; }

    public StatusEffects GetCurrentStatusEffect() { return _currentStatusEffect; }
    public DashEffects GetDashEffect() { return _currentDashEffects; }
    public CollisionEffects GetCollisionEffect() { return _currentCollisionEffects; }
    public SpecialEffects GetSpecialEffects() { return _currentSpecialEffect; }
    public CastingType GetCurrentCastingType() { return _currentCastingType; }
    public bool HasControllabeAttack() { return _controllabeAttack; }
    public GameObject GetCurrentMinion() { return _currentMinion; }


    // -----------------------------------------------------------------------------------------


    //CLASS FUNCTIONS
    public void LoadClass(int classValue)
    {
        switch (classValue)
        {
            case 0:
                _currentClass = ClassType.none;
                break;

            case 1:
                _currentClass = ClassType.Wizard;
                break;

            case 2:
                _currentClass = ClassType.Conjurer;
                break;

            case 3:
                _currentClass = ClassType.Sorcerer;
                break;

            case 4:
                _currentClass = ClassType.Mage;
                break;

            case 5:
                _currentClass = ClassType.Enchanter;
                break;

            case 6:
                _currentClass = ClassType.Warlock;
                break;

            case 7:
                _currentClass = ClassType.Witch;
                break;

            case 8:
                _currentClass = ClassType.Tarot;
                break;
        }
    }

    public void ChangeClass(ClassType newClass)
    {
        _currentClass = newClass;

        switch (_currentClass)
        {
            case ClassType.Wizard:
                break;

            case ClassType.Conjurer:
                break;

            case ClassType.Sorcerer:
                break;

            case ClassType.Mage:
                break;

            case ClassType.Enchanter:
                break;

            case ClassType.Warlock:
                break;

            case ClassType.Witch:
                break;

            case ClassType.Tarot:
                break;

            case ClassType.none:
                break;
        }
    }

    public bool CheckPlayerClass(ClassType classCheck)
    {
        if (_currentClass == classCheck) { return true; }
        else return false;
    }


    // ---------------------------------------------------------------------------------------------


    //MAGIC FUNCTIONS
    public void SetToSpecificMagic(MagicType newMagic)
    {
        _currentMagic = newMagic;
        UpdateMagic(false, 0);
    }

    public void AddMagic(MagicType magicToAdd)
    {
        switch (magicToAdd)
        {
            case MagicType.arcane | MagicType.blood | MagicType.cupcakes:
                _currentMagic = magicToAdd;
                break;

            default:
                _currentMagic |= magicToAdd;
                break;
        }

        UpdateMagic(false, 0);
    }

    public void RemoveMagic(MagicType magicToRemove)
    {
        _currentMagic &= ~magicToRemove;
        UpdateMagic(false, 0);
    }

    private void SetMagicProperties(string name, int index)
    {
        magicName = name;
        magicIdx = index;
    }

    public void UpdateMagic(bool loadMagic, int loadMagicIndex)
    {
        int magicInt = loadMagic ? loadMagicIndex : (int)_currentMagic;

        switch (magicInt)
        {
            // Cupcakes
            case 128:
                SetMagicProperties("Cupcakes", 32);

                AddStatusEffect(StatusEffects.slowed);
                break;

            // Blood
            case 64:
                SetMagicProperties("Blood", 31);

                AddStatusEffect(StatusEffects.lifeDraining);
                break;

            // Fire, Water, Earth, Dark, Light
            case 62:
                SetMagicProperties("DIVINE POWER", 30);

                AddStatusEffect(StatusEffects.blinded | StatusEffects.burning | StatusEffects.electrocuted | StatusEffects.frozen | StatusEffects.lifeDraining | StatusEffects.poisoned | StatusEffects.rooted | StatusEffects.slowed);
                AddDashEffect(DashEffects.dashAOETrail | DashEffects.dashPillars | DashEffects.teleportBurst);
                AddSpecialEffect(SpecialEffects.AOEGround | SpecialEffects.burst | SpecialEffects.explosion | SpecialEffects.pillar | SpecialEffects.rain | SpecialEffects.summoning);
                break;

            // Fire, Earth, Dark, Light
            case 58:
                SetMagicProperties("Electrified Meteor", 29);

                AddStatusEffect(StatusEffects.electrocuted | StatusEffects.rooted | StatusEffects.slowed);
                AddSpecialEffect(SpecialEffects.explosion);
                break;

            // Water, Earth, Dark, Light
            case 60:
                SetMagicProperties("Electrified Nature", 28);

                AddStatusEffect(StatusEffects.rooted | StatusEffects.electrocuted);
                break;

            // Fire, Water, Light, Dark
            case 54:
                SetMagicProperties("Electrified Blue Fire", 27);

                AddStatusEffect(StatusEffects.electrocuted | StatusEffects.burning);
                break;

            // Fire, Water, Earth Light
            case 46:
                SetMagicProperties("Angelic Infused Metal", 26);

                AddStatusEffect(StatusEffects.slowed);
                SwitchCollisionType(CollisionEffects.peircing);
                AddSpecialEffect(SpecialEffects.summoning);
                break;

            // Fire, Water, Earth, Dark
            case 30:
                SetMagicProperties("Demonic Infused Metal", 25);

                AddStatusEffect(StatusEffects.blinded | StatusEffects.slowed);
                SwitchCollisionType(CollisionEffects.peircing);
                break;

            // Earth, Dark, Light
            case 56:
                SetMagicProperties("Electrified Earth", 24);

                AddStatusEffect(StatusEffects.electrocuted | StatusEffects.slowed);
                break;

            // Water, Dark, Light
            case 52:
                SetMagicProperties("Etheral Ice", 23);

                AddStatusEffect(StatusEffects.frozen);
                AddSpecialEffect(SpecialEffects.AOEGround);
                break;

            // Water, Earth, Light
            case 44:
                SetMagicProperties("Angelic Nature", 22);

                AddStatusEffect(StatusEffects.rooted);
                AddSpecialEffect(SpecialEffects.summoning);
                break;

            // Water, Earth, Dark
            case 28:
                SetMagicProperties("Withered Nature", 21);

                AddStatusEffect(StatusEffects.blinded | StatusEffects.poisoned | StatusEffects.rooted);
                break;

            // Fire, Dark, Light
            case 50:
                SetMagicProperties("Pure Fire", 20);

                AddStatusEffect(StatusEffects.burning | StatusEffects.electrocuted);
                break;

            // Fire, Earth, Dark
            case 26:
                SetMagicProperties("Dark Fire Meteors", 19);

                AddStatusEffect(StatusEffects.burning | StatusEffects.blinded | StatusEffects.slowed);
                AddSpecialEffect(SpecialEffects.explosion);
                break;

            // Fire, Water, Light
            case 38:
                SetMagicProperties("Angelic Blue Fire", 18);

                AddStatusEffect(StatusEffects.burning);
                AddSpecialEffect(SpecialEffects.summoning);
                break;

            // Fire, Water, Dark
            case 22:
                SetMagicProperties("Red Ice", 17);

                AddStatusEffect(StatusEffects.burning | StatusEffects.frozen);
                break;

            // Fire, Water, Earth
            case 14:
                SetMagicProperties("Metal", 16);

                AddStatusEffect(StatusEffects.slowed);
                SwitchCollisionType(CollisionEffects.peircing);
                break;

            // Dark, Light
            case 48:
                SetMagicProperties("Lightning", 15);

                AddStatusEffect(StatusEffects.electrocuted);
                break;

            // Earth, Light
            case 40:
                SetMagicProperties("Angelic Infused Earth", 14);

                AddStatusEffect(StatusEffects.slowed);
                AddSpecialEffect(SpecialEffects.summoning);
                break;

            // Earth, Dark
            case 24:
                SetMagicProperties("Demoic Infused Earth", 13);

                AddStatusEffect(StatusEffects.blinded | StatusEffects.slowed);
                break;

            // Water, Light
            case 36:
                SetMagicProperties("Spirit Water", 12);

                AddSpecialEffect(SpecialEffects.summoning | SpecialEffects.pillar);
                break;

            // Water, Dark
            case 20:
                SetMagicProperties("Ice", 11);

                AddStatusEffect(StatusEffects.frozen);
                break;

            // Water, Earth
            case 12:
                SetMagicProperties("Nature", 10);

                AddStatusEffect(StatusEffects.rooted);
                break;

            // Fire, Light
            case 34:
                SetMagicProperties("Holy Fire", 9);

                AddStatusEffect(StatusEffects.burning);
                AddSpecialEffect(SpecialEffects.summoning);
                break;

            // Fire, Dark
            case 18:
                SetMagicProperties("Dark Fire", 8);

                AddStatusEffect(StatusEffects.burning | StatusEffects.blinded);
                break;

            // Fire, Earth
            case 10:
                SetMagicProperties("Meteor", 7);

                AddStatusEffect(StatusEffects.burning | StatusEffects.slowed);
                AddSpecialEffect(SpecialEffects.explosion);
                break;

            // Fire, Water
            case 6:
                SetMagicProperties("Blue Fire", 6);

                AddStatusEffect(StatusEffects.burning);
                break;

            // Light
            case 32:
                SetMagicProperties("Holy Light", 5);

                AddSpecialEffect(SpecialEffects.pillar);
                break;

            // Dark
            case 16:
                SetMagicProperties("Shadow", 4);

                AddStatusEffect(StatusEffects.blinded);
                break;

            // Earth
            case 8:
                SetMagicProperties("Earth", 3);

                AddStatusEffect(StatusEffects.slowed);
                break;

            // Water
            case 4:
                SetMagicProperties("Water", 2);

                AddSpecialEffect(SpecialEffects.AOEGround);
                break;

            //Fire
            case 2:
                SetMagicProperties("Fire", 1);

                AddStatusEffect(StatusEffects.burning);
                break;

            // Arcane
            default:
                SetMagicProperties("Arcane", 0);
                break;
        }
    }

    // -----------------------------------------------------------------------------------------------


    //STATUS EFFECT FUNCTIONS
    public void ChangeStatusEffectAbility(StatusEffects newStatusEffect)
    {
        _currentStatusEffect = newStatusEffect;
    }

    public void AddStatusEffect(StatusEffects addStatusEffect)
    {
        _currentStatusEffect |= addStatusEffect;
    }

    public void RemoveStatusEffect(StatusEffects removeStatusEffect)
    {
        _currentStatusEffect &= ~removeStatusEffect;
    }


    // -----------------------------------------------------------------------------------------------


    //DASH EFFECT FUNCTIONS
    public void SetToSpecificDashType(DashEffects newDashEffect)
    {
        _currentDashEffects = newDashEffect;
    }

    public void AddDashEffect(DashEffects addDashEffect)
    {
        _currentDashEffects |= addDashEffect;
    }

    public void RemoveDashEffect(DashEffects removeDashEffect)
    {
        _currentDashEffects &= ~removeDashEffect;
    }


    // ------------------------------------------------------------------------------------------------


    //COLLISION FUNCTIONS
    public void SwitchCollisionType(CollisionEffects newCollisionEffect)
    {
        _currentCollisionEffects = newCollisionEffect;
    }



    // ------------------------------------------------------------------------------------------------


    //SPECIAL EFFECT FUNCIONS
    public void SetToSpecificSpecialEffect(SpecialEffects newSpecialEffects)
    {
        _currentSpecialEffect = newSpecialEffects;
    }

    public void AddSpecialEffect(SpecialEffects addSpecialEffect)
    {
        _currentSpecialEffect |= addSpecialEffect;
    }

    public void RemoveSpecialEffect(SpecialEffects removeSpecialEffect)
    {
        _currentSpecialEffect &= ~removeSpecialEffect;
    }


    // -------------------------------------------------------------------------------------------------



    //CASTING TYPE FUNCTIONS

    public void LoadCastingType(int castingTypeValue)
    {
        switch (castingTypeValue)
        {
            case 0:
                _currentCastingType = CastingType.charge;
                break;

            case 1:
                _currentCastingType = CastingType.rapidFire;
                break;

            case 2:
                _currentCastingType = CastingType.beam;
                break;
        }
    }

    public void ChangeCastingType(CastingType newCastingType)
    {
        _currentCastingType = newCastingType;
    }



    // --------------------------------------------------------------------------------------------------


    //CONTROLLABLE ATTACK FUNCTIONS
    public void ToggleControllabeAttack(bool on) { _controllabeAttack = on; }



    // --------------------------------------------------------------------------------------------------

    public bool CanSummonMinion()
    {
        if (_currentMinion == null) { return true; }
        else return false;
    }

    // --------------------------------------------------------------------------------------------------

    public void ResetAll()
    {

    }
}
