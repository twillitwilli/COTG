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
    private string _magicName;
    private int _magicIdx;

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
    public string GetMagicName() { return _magicName; }
    public int GetCurrentMagicIndex() { return _magicIdx; }
    public void SetCurrentMagicIndex(int newMagicIndex) { _magicIdx = newMagicIndex; }
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
        UpdateMagic();
    }

    public void AddMagic(MagicType magicToAdd)
    {
        _currentMagic |= magicToAdd;
        UpdateMagic();
    }

    public void RemoveMagic(MagicType magicToRemove)
    {
        _currentMagic &= ~magicToRemove;
        UpdateMagic();
    }

    private void UpdateMagic()
    {
        switch (_currentMagic)
        {
            case MagicType.cupcakes:
                _magicName = "Cupcakes";
                _magicIdx = 33;
                AddStatusEffect(StatusEffects.slowed);
                break;

            case MagicType.blood:
                _magicName = "Blood";
                _magicIdx = 32;
                AddStatusEffect(StatusEffects.lifeDraining);
                break;

            case MagicType.fire:
                switch(_currentMagic)
                {
                    case MagicType.water:
                        switch(_currentMagic)
                        {
                            case MagicType.earth:
                                switch (_currentMagic)
                                {
                                    case MagicType.dark:
                                        switch (_currentMagic)
                                        {
                                            case MagicType.light:
                                                _magicName = "DIVINE POWER";
                                                _magicIdx = 31;
                                                AddStatusEffect(StatusEffects.blinded | StatusEffects.burning | StatusEffects.electrocuted | StatusEffects.frozen | StatusEffects.lifeDraining | StatusEffects.poisoned | StatusEffects.rooted | StatusEffects.slowed);
                                                AddDashEffect(DashEffects.dashAOETrail | DashEffects.dashPillars | DashEffects.teleportBurst);
                                                AddSpecialEffect(SpecialEffects.AOEGround | SpecialEffects.burst | SpecialEffects.explosion | SpecialEffects.pillar | SpecialEffects.rain | SpecialEffects.summoning);
                                                break;

                                            default:
                                                _magicName = "Demonic Infused Metal";
                                                _magicIdx = 26;
                                                AddStatusEffect(StatusEffects.blinded | StatusEffects.slowed);
                                                SwitchCollisionType(CollisionEffects.peircing);
                                                break;
                                        }
                                        break;

                                    case MagicType.light:
                                        _magicName = "Angelic Infused Metal";
                                        _magicIdx = 27;
                                        AddStatusEffect(StatusEffects.slowed);
                                        SwitchCollisionType(CollisionEffects.peircing);
                                        AddSpecialEffect(SpecialEffects.summoning);
                                        break;

                                    default:
                                        _magicName = "Metal";
                                        _magicIdx = 16;
                                        AddStatusEffect(StatusEffects.slowed);
                                        SwitchCollisionType(CollisionEffects.peircing);
                                        break;
                                }

                                break;
                            case MagicType.dark:
                                switch (_currentMagic)
                                {
                                    case MagicType.light:
                                        _magicName = "Electrified Blue Fire";
                                        _magicIdx = 28;
                                        AddStatusEffect(StatusEffects.electrocuted | StatusEffects.burning);
                                        break;
                                        
                                    default:
                                        _magicName = "Red Ice";
                                        _magicIdx = 17;
                                        AddStatusEffect(StatusEffects.burning | StatusEffects.frozen);
                                        break;
                                }
                                break;
                            case MagicType.light:
                                _magicName = "Angelic Blue Fire";
                                _magicIdx = 18;
                                AddStatusEffect(StatusEffects.burning);
                                AddSpecialEffect(SpecialEffects.summoning);
                                break;

                            default:
                                _magicName = "Blue Fire";
                                _magicIdx = 6;
                                AddStatusEffect(StatusEffects.burning);
                                break;
                        }
                        break;

                    case MagicType.earth:
                        switch(_currentMagic)
                        {
                            case MagicType.dark:
                                switch (_currentMagic)
                                {
                                    case MagicType.light:
                                        _magicName = "Electrified Meteor";
                                        _magicIdx = 30;
                                        AddStatusEffect(StatusEffects.electrocuted | StatusEffects.rooted | StatusEffects.slowed);
                                        AddSpecialEffect(SpecialEffects.explosion);
                                        break;

                                    default:
                                        _magicIdx = 19;
                                        _magicName = "Dark Fire Meteor";
                                        AddStatusEffect(StatusEffects.burning | StatusEffects.blinded | StatusEffects.slowed);
                                        AddSpecialEffect(SpecialEffects.explosion);
                                        break;
                                }
                                break;

                            case MagicType.light:
                                _magicName = "Holy Fire Meteor";
                                _magicIdx = 21;
                                AddStatusEffect(StatusEffects.burning | StatusEffects.slowed);
                                AddSpecialEffect(SpecialEffects.explosion);
                                break;

                            default:
                                _magicName = "Meteor";
                                _magicIdx = 7;
                                AddStatusEffect(StatusEffects.burning | StatusEffects.slowed);
                                AddSpecialEffect(SpecialEffects.explosion);
                                break;
                        }
                        break;

                    case MagicType.dark:
                        switch (_currentMagic)
                        {
                            case MagicType.light:
                                _magicName = "Pure White Fire";
                                _magicIdx = 20;
                                AddStatusEffect(StatusEffects.burning | StatusEffects.electrocuted);
                                break;

                            default:
                                _magicName = "Dark Fire";
                                _magicIdx = 8;
                                AddStatusEffect(StatusEffects.burning | StatusEffects.blinded);
                                break;
                        }
                        break;

                    case MagicType.light:
                        _magicName = "Holy Fire";
                        _magicIdx = 9;
                        AddStatusEffect(StatusEffects.burning);
                        AddSpecialEffect(SpecialEffects.summoning);
                        break;

                    default:
                        _magicName = "Fire";
                        _magicIdx = 1;
                        AddStatusEffect(StatusEffects.burning);
                        break;
                }
                break;

            case MagicType.water:
                switch (_currentMagic)
                {
                    case MagicType.earth:
                        switch (_currentMagic)
                        {
                            case MagicType.dark:
                                switch (_currentMagic)
                                {
                                    case MagicType.light:
                                        _magicName = "Electrified Nature";
                                        _magicIdx = 29;
                                        AddStatusEffect(StatusEffects.rooted | StatusEffects.electrocuted);
                                        break;

                                    default:
                                        _magicIdx = 22;
                                        _magicName = "Withered Nature";
                                        AddStatusEffect(StatusEffects.blinded | StatusEffects.poisoned | StatusEffects.rooted);
                                        break;
                                }
                                break;

                            case MagicType.light:
                                _magicName = "Angelic Nature";
                                _magicIdx = 23;
                                AddStatusEffect(StatusEffects.rooted);
                                AddSpecialEffect(SpecialEffects.summoning);
                                break;

                            default:
                                _magicName = "Nature";
                                _magicIdx = 10;
                                AddStatusEffect(StatusEffects.rooted);
                                break;
                        }
                        break;
                    case MagicType.dark:
                        switch (_currentMagic)
                        {
                            case MagicType.light:
                                _magicName = "Etheral Ice";
                                _magicIdx = 24;
                                AddStatusEffect(StatusEffects.frozen);
                                AddSpecialEffect(SpecialEffects.AOEGround);
                                break;

                            default:
                                _magicName = "Ice";
                                _magicIdx = 11;
                                AddStatusEffect(StatusEffects.frozen);
                                break;
                        }
                        break;
                    case MagicType.light:
                        _magicName = "Spirit Water";
                        _magicIdx = 12;
                        AddSpecialEffect(SpecialEffects.summoning | SpecialEffects.pillar);
                        break;
                    default:
                        _magicName = "Water";
                        _magicIdx = 2;
                        AddSpecialEffect(SpecialEffects.AOEGround);
                        break;
                }
                break;

            case MagicType.earth:
                switch (_currentMagic)
                {
                    case MagicType.dark:
                        switch (_currentMagic)
                        {
                            case MagicType.light:
                                _magicName = "Electrified Earth";
                                _magicIdx = 25;
                                AddStatusEffect(StatusEffects.electrocuted | StatusEffects.slowed);
                                break;

                            default:
                                _magicName = "Demonic Infuse Earth";
                                _magicIdx = 13;
                                AddStatusEffect(StatusEffects.blinded | StatusEffects.slowed);
                                break;
                        }
                        break;

                    case MagicType.light:
                        _magicName = "Angelic Infused Earth";
                        _magicIdx = 14;
                        AddStatusEffect(StatusEffects.slowed);
                        AddSpecialEffect(SpecialEffects.summoning);
                        break;

                    default:
                        _magicName = "Earth";
                        _magicIdx = 3;
                        AddStatusEffect(StatusEffects.slowed);
                        break;
                }
                break;

            case MagicType.dark:
                switch (_currentMagic)
                {
                    case MagicType.light:
                        _magicName = "Lightning";
                        _magicIdx = 15;
                        AddStatusEffect(StatusEffects.electrocuted);
                        break;

                    default:
                        _magicName = "Shadow";
                        _magicIdx = 4;
                        AddStatusEffect(StatusEffects.blinded);
                        break;
                }
                break;

            case MagicType.light:
                _magicName = "Holy Light";
                _magicIdx = 5;
                AddSpecialEffect(SpecialEffects.pillar);
                break;

            default:
                _magicName = "Arcane";
                _magicIdx = 0;
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
