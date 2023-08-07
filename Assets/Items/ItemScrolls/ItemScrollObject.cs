using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemPools/ItemScroll")]
public class ItemScrollObject : ScriptableObject
{
    public string scrollName, scrollDescription;
    public Material itemPicture;

    [Header("--Add Magic--")]
    public bool addMagicType;
    public MagicController.MagicType addMagic;

    [Header("--Add Status Effect")]
    public bool addStatusEffect;
    public MagicController.StatusEffects addStatus;

    [Header("--Add Dash Effect--")]
    public bool addDashEffet;
    public MagicController.DashEffects addDash;

    [Header("--Change Collision Type--")]
    public bool changeCollision;
    public MagicController.CollisionEffects collisionEffect;

    [Header("--Add Special Effect--")]
    public bool addSpecialEffect;
    public MagicController.SpecialEffects addSpecial;

    [Header("--Change Casting Type--")]
    public bool changeCastingType;
    public MagicController.CastingType castingType;

    [Header("--Dungeon Abilities--")]
    public bool giveDungeonAbility;
    public bool tempMapReveal, mapReveal, tempCompassReveal, compassReveal;

    [Header("--Change Player Stats--")]
    public bool changeBaseStat;
    public bool movementSpeed;
    public float movementValue;
    public bool currentHealth;
    public float currentHealthValue;
    public bool maxHealth;
    public float maxHealthValue;
    public bool attackDamage;
    public float attackValue;
    public bool magicFocus;
    public float magicFocusValue;
    public bool attackRange;
    public float rangeValue;
    public bool attackCooldown;
    public float attackCDValue;
    public bool critChance;
    public float critChanceValue;
    public bool aimAssist;
    public float aimAssistValue;
    public bool luck;
    public float luckValue;
    public bool arcaneOrb;
    public float arcaneOrbValue;
    public bool gold;
    public float goldValue;
}
