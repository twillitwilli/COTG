using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemGrabbable : MonoBehaviour
{
    public enum PlayerItem { nothingToGrab, map, wallet, bomb, key, potion, staff, bow, bowString, rune, climbable, jar, classCard }
    public PlayerItem whichItem;
    public bool canBePlacedInPocket, throwableObject;
    [HideInInspector] public VRPlayerHand currentHand;
    [HideInInspector] public bool attachedToPocket;

    [Header("Has Impact sound")]
    public bool hasImpactSound;
    public AudioSource impactSound;

    [Header("Materials To Change For Grab")]
    public Material normalMat;
    public Material canGrabMat;
    private MeshRenderer meshRenderer;

    [Header("Telekinetic Settings")]
    public bool canTelekineticGrab;
    public GameObject telekineticGrabEffect;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (hasImpactSound && !impactSound.isPlaying && col.relativeVelocity.magnitude >= 0.3f)
        {
            impactSound.volume = col.relativeVelocity.magnitude / 5;
            impactSound.pitch = Random.Range(0.9f, 1.1f);
            impactSound.Play();
        }
    }

    private void OnDestroy()
    {
        if (currentHand != null) { currentHand.GetGrabController().GrabbableReset(); }
    }
}
