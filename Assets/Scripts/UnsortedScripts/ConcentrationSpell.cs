using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcentrationSpell : MonoBehaviour
{
    public float attackDamage, projectileSpeed, projectileRange, aimAssist;
    public LayerMask ignoreLayers;

    public GameObject[] scaleObjects;
    public GameObject[] collisionEffect; // 0 normal collision, 1 explosion, 2 burst, 3 pillar, 4 aoe ground

    protected VRPlayerController player;
    protected Rigidbody rb;
    protected bool rayHit;
    protected int statusEffectChance;
}
