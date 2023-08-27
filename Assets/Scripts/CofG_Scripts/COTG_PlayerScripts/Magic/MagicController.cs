using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MagicController : MonoSingleton<MagicController>
{
    //CLASS SELECTION
    public enum ClassType 
    { 
        none = 0, 
        Wizard = 1, 
        Conjurer = 2, 
        Sorcerer = 3, 
        Mage = 4, 
        Enchanter = 5, 
        Warlock = 6, 
        Witch = 7, 
        Tarot = 8 
    }
    public ClassType currentClass { get; private set; }

    //MAGIC SELECTION
    [System.Flags] 
    public enum MagicType 
    { 
        arcane = 1, 
        fire = 2, 
        water = 4, 
        earth = 8, 
        dark = 16, 
        light = 32, 
        blood = 64, 
        cupcakes = 128 
    }
    public MagicType currentMagic { get; private set; }
    public string magicName { get; private set; }
    public int magicIdx { get; private set; }

    //STATUS
    [System.Flags] 
    public enum StatusEffects 
    { 
        none = 0, 
        burning = 1, 
        blinded = 2, 
        frozen = 4, 
        electrocuted = 8, 
        slowed = 16, 
        rooted = 32, 
        lifeDraining = 64, 
        poisoned = 128 
    }
    public StatusEffects currentStatusEffect { get; private set; }
    public bool hasStatusEffect { get; private set; }

    //DASH EFFECTS
    [System.Flags] 
    public enum DashEffects 
    { 
        none = 0, 
        dashAOETrail = 1, 
        teleportBurst = 2, 
        dashPillars = 4 
    }
    public DashEffects currentDashEffects { get; private set; }

    //COLLISION EFFECTS
    public enum CollisionEffects 
    { 
        none = 0, 
        peircing = 1, 
        bouncing = 2, 
        split = 3 
    }
    public CollisionEffects currentCollisionEffects { get; private set; }

    //SPECIAL EFFECTS
    [System.Flags] public enum SpecialEffects 
    { 
        none = 0, 
        explosion = 1, 
        rain = 2, 
        summoning = 4, 
        burst = 8, 
        pillar = 16, 
        AOEGround = 32 
    }
    public SpecialEffects currentSpecialEffect { get; private set; }

    //CASTING TYPE
    public enum CastingType 
    { 
        none = 0, 
        charge = 1, 
        rapidFire = 2, 
        beam = 3
    }
    public CastingType currentCastingType { get; private set; }
    public bool controllabeAttack { get; private set; }

    //PLAYER SUMMONED MINION
    public GameObject currentMinion { get; private set; }


    // -----------------------------------------------------------------------------------------


    //CLASS FUNCTIONS
    public void LoadClass(int classValue)
    {
        switch (classValue)
        {
            case 0:
                currentClass = ClassType.none;
                break;

            case 1:
                currentClass = ClassType.Wizard;
                break;

            case 2:
                currentClass = ClassType.Conjurer;
                break;

            case 3:
                currentClass = ClassType.Sorcerer;
                break;

            case 4:
                currentClass = ClassType.Mage;
                break;

            case 5:
                currentClass = ClassType.Enchanter;
                break;

            case 6:
                currentClass = ClassType.Warlock;
                break;

            case 7:
                currentClass = ClassType.Witch;
                break;

            case 8:
                currentClass = ClassType.Tarot;
                break;
        }
    }

    public void ChangeClass(ClassType newClass)
    {
        currentClass = newClass;

        switch (currentClass)
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
        bool check = currentClass == classCheck ? true : false;
        return check;
    }


    // ---------------------------------------------------------------------------------------------


    //MAGIC FUNCTIONS
    public void SetToSpecificMagic(MagicType newMagic)
    {
        currentMagic = newMagic;
        UpdateMagic();
    }

    public void AddMagic(MagicType magicToAdd)
    {
        currentMagic |= magicToAdd;
        UpdateMagic();
    }

    public void RemoveMagic(MagicType magicToRemove)
    {
        currentMagic &= ~magicToRemove;
        UpdateMagic();
    }

    private void SetMagicProperties(string name, int index)
    {
        magicName = name;
        magicIdx = index;
    }

    public void UpdateMagic(bool loadMagic = false, int loadMagicIndex = 0)
    {
        // Will remove Arcane if any other magic type is selected
        if (currentMagic != MagicType.arcane && (currentMagic & MagicType.arcane) != 0)
        {
            currentMagic &= ~MagicType.arcane;
        }

        // Will remove all other magic types if Blood or Cupcakes is selected
        if ((currentMagic & (MagicType.blood | MagicType.cupcakes)) != 0)
        {
            currentMagic = currentMagic & (MagicType.blood | MagicType.cupcakes);
        }

        int magicInt = loadMagic ? loadMagicIndex : (int)currentMagic;

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
            case 1 | 0:
                SetMagicProperties("Arcane", 0);
                break;
        }
    }

    // -----------------------------------------------------------------------------------------------


    //STATUS EFFECT FUNCTIONS
    public void ChangeStatusEffectAbility(StatusEffects newStatusEffect)
    {
        currentStatusEffect = newStatusEffect;
    }

    public void AddStatusEffect(StatusEffects addStatusEffect)
    {
        currentStatusEffect |= addStatusEffect;
    }

    public void RemoveStatusEffect(StatusEffects removeStatusEffect)
    {
        currentStatusEffect &= ~removeStatusEffect;
    }


    // -----------------------------------------------------------------------------------------------


    //DASH EFFECT FUNCTIONS
    public void SetToSpecificDashType(DashEffects newDashEffect)
    {
        currentDashEffects = newDashEffect;
    }

    public void AddDashEffect(DashEffects addDashEffect)
    {
        currentDashEffects |= addDashEffect;
    }

    public void RemoveDashEffect(DashEffects removeDashEffect)
    {
        currentDashEffects &= ~removeDashEffect;
    }


    // ------------------------------------------------------------------------------------------------


    //COLLISION FUNCTIONS
    public void SwitchCollisionType(CollisionEffects newCollisionEffect)
    {
        currentCollisionEffects = newCollisionEffect;
    }



    // ------------------------------------------------------------------------------------------------


    //SPECIAL EFFECT FUNCIONS
    public void SetToSpecificSpecialEffect(SpecialEffects newSpecialEffects)
    {
        currentSpecialEffect = newSpecialEffects;
    }

    public void AddSpecialEffect(SpecialEffects addSpecialEffect)
    {
        currentSpecialEffect |= addSpecialEffect;
    }

    public void RemoveSpecialEffect(SpecialEffects removeSpecialEffect)
    {
        currentSpecialEffect &= ~removeSpecialEffect;
    }


    // -------------------------------------------------------------------------------------------------



    //CASTING TYPE FUNCTIONS

    public void LoadCastingType(int castingTypeValue)
    {
        switch (castingTypeValue)
        {
            case 0:
                currentCastingType = CastingType.charge;
                break;

            case 1:
                currentCastingType = CastingType.rapidFire;
                break;

            case 2:
                currentCastingType = CastingType.beam;
                break;
        }
    }

    public void ChangeCastingType(CastingType newCastingType)
    {
        currentCastingType = newCastingType;
    }



    // --------------------------------------------------------------------------------------------------


    //CONTROLLABLE ATTACK FUNCTIONS
    public void ToggleControllabeAttack(bool on) { controllabeAttack = on; }



    // --------------------------------------------------------------------------------------------------

    public bool CanSummonMinion()
    {
        bool canSummon = currentMinion == null ? true : false;
        return canSummon;
    }

    public void SetNewMinion(GameObject newMinion)
    {
        currentMinion = newMinion;
    }

    // --------------------------------------------------------------------------------------------------


    public async Task LoadSavedDungeonMagicStats(PlayerDungeonData loadedData)
    {
        switch (loadedData.playerClass)
        {
            case 0:
                currentClass = ClassType.none;
                break;

            case 1:
                currentClass = ClassType.Wizard;
                break;

            case 2:
                currentClass = ClassType.Conjurer;
                break;

            case 3:
                currentClass = ClassType.Sorcerer;
                break;

            case 4:
                currentClass = ClassType.Mage;
                break;

            case 5:
                currentClass = ClassType.Enchanter;
                break;

            case 6:
                currentClass = ClassType.Warlock;
                break;

            case 7:
                currentClass = ClassType.Witch;
                break;

            case 8:
                currentClass = ClassType.Tarot;
                break;
        }

        switch (loadedData.magicType)
        {
            // Cupcakes
            case 128:
                currentMagic |= MagicType.cupcakes;
                break;

            // Blood
            case 64:
                currentMagic |= MagicType.blood;
                break;

            // Fire, Water, Earth, Dark, Light
            case 62:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.earth;
                currentMagic |= MagicType.dark;
                currentMagic |= MagicType.light;
                break;

            // Fire, Earth, Dark, Light
            case 58:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.earth;
                currentMagic |= MagicType.dark;
                currentMagic |= MagicType.light;
                break;

            // Water, Earth, Dark, Light
            case 60:
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.earth;
                currentMagic |= MagicType.dark;
                currentMagic |= MagicType.light;
                break;

            // Fire, Water, Light, Dark
            case 54:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.light;
                currentMagic |= MagicType.dark;
                break;

            // Fire, Water, Earth Light
            case 46:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.earth;
                currentMagic |= MagicType.light;
                break;

            // Fire, Water, Earth, Dark
            case 30:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.earth;
                currentMagic |= MagicType.dark;
                break;

            // Earth, Dark, Light
            case 56:
                currentMagic |= MagicType.earth;
                currentMagic |= MagicType.dark;
                currentMagic |= MagicType.light;
                break;

            // Water, Dark, Light
            case 52:
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.dark;
                currentMagic |= MagicType.light;
                break;

            // Water, Earth, Light
            case 44:
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.earth;
                currentMagic |= MagicType.light;
                break;

            // Water, Earth, Dark
            case 28:
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.earth;
                currentMagic |= MagicType.dark;
                break;

            // Fire, Dark, Light
            case 50:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.dark;
                currentMagic |= MagicType.light;
                break;

            // Fire, Earth, Dark
            case 26:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.earth;
                currentMagic |= MagicType.dark;
                break;

            // Fire, Water, Light
            case 38:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.light;
                break;

            // Fire, Water, Dark
            case 22:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.dark;
                break;

            // Fire, Water, Earth
            case 14:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.earth;
                break;

            // Dark, Light
            case 48:
                currentMagic |= MagicType.dark;
                currentMagic |= MagicType.light;
                break;

            // Earth, Light
            case 40:
                currentMagic |= MagicType.earth;
                currentMagic |= MagicType.light;
                break;

            // Earth, Dark
            case 24:
                currentMagic |= MagicType.earth;
                currentMagic |= MagicType.dark;
                break;

            // Water, Light
            case 36:
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.light;
                break;

            // Water, Dark
            case 20:
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.dark;
                break;

            // Water, Earth
            case 12:
                currentMagic |= MagicType.water;
                currentMagic |= MagicType.earth;
                break;

            // Fire, Light
            case 34:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.light;
                break;

            // Fire, Dark
            case 18:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.dark;
                break;

            // Fire, Earth
            case 10:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.earth;
                break;

            // Fire, Water
            case 6:
                currentMagic |= MagicType.fire;
                currentMagic |= MagicType.water;
                break;

            // Light
            case 32:
                currentMagic |= MagicType.light;
                break;

            // Dark
            case 16:
                currentMagic |= MagicType.dark;
                break;

            // Earth
            case 8:
                currentMagic |= MagicType.earth;
                break;

            // Water
            case 4:
                currentMagic |= MagicType.water;
                break;

            //Fire
            case 2:
                currentMagic |= MagicType.fire;
                break;

            // Arcane
            case 1 | 0:
                currentMagic |= MagicType.arcane;
                break;
        }

        UpdateMagic();
    }
}
