using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStuff/CreateNewMagicController")]
public class PlayerMagicController : ScriptableObject
{
    public enum critEffect { explosion, rain, summon, burst, pillar, aoeGround }

    [Header("Arcane Orbs")]
    public GameObject arcaneBombInHand;
    public GameObject arcaneBombExplosion;

    [Header("--All Classes--")]
    public string[] magicIndexTypes;
    public GameObject[] magicCircles;
    public GameObject[] spellCharges;
    public GameObject[] chargedVisual;

    [Header("--Crit Effects--")]
    public GameObject[] explosion;
    public GameObject[] rain;
    public GameObject[] burst;
    public GameObject[] pillar;
    public GameObject[] aoeGround;
    [Header("--Minions--")]
    public SummonableMinions[] minions;
    [Header("--Summoned Pet Attack--")]
    public GameObject[] petAttack;


    [Header("--Sorcerer--")]
    public GameObject[] sorcererChargedSpells;
    public GameObject[] sorcererRapidFireSpells;
    public GameObject[] sorcererConstantSpells;
    public GameObject[] accelerationEffects;
    public GameObject[] effectBurstWhenCast;

    [Header("--Wizard--")]
    public GameObject[] staffs;
    public GameObject[] wizardChargedSpells;
    public GameObject[] wizardRapidFireSpells;
    public GameObject[] wizardConstantSpells;

    [Header("--Conjurer--")]
    public GameObject[] bows;
    public GameObject[] conjurerChargedSpells;
    public GameObject[] conjurerRapidFireSpells;
    public GameObject[] conjurerConstantSpells;

    [Header("--Mage--")]
    public GameObject[] wands;
    public GameObject[] mageChargedSpells;
    public GameObject[] mageRapidFireSpells;
    public GameObject[] mageConstantSpells;

    [Header("--Other Stuff--")]
    public GameObject arcaneShield;
    public GameObject timeSlowBubble;

    [Header("Status Effect Stuff")]
    public GameObject damageOverTimeNode;
}
