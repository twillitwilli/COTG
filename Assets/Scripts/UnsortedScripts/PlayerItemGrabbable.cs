using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemGrabbable : MonoBehaviour
{
    public ItemPoolManager.GrabbableItem grabbableItem;

    public bool 
        canBePlacedInPocket, 
        throwableObject;

    public VRHand currentHand { get; set; }

    public bool attachedToPocket { get; set; }

    [Header("Has Impact sound")]
    public bool hasImpactSound;
    public AudioSource impactSound;

    [Header("Materials To Change For Grab")]
    public Material normalMat;
    public Material canGrabMat;

    MeshRenderer meshRenderer;

    [Header("Telekinetic Settings")]
    public bool canTelekineticGrab;
    public GameObject telekineticGrabEffect;
    public List<TelekinesisRaycast> activeTelekinesis = new List<TelekinesisRaycast>();

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (hasImpactSound && !impactSound.isPlaying && col.relativeVelocity.magnitude >= 0.3f)
        {
            impactSound.volume = col.relativeVelocity.magnitude / 5;
            impactSound.pitch = Random.Range(0.9f, 1.1f);
            impactSound.Play();
        }
    }

    void OnDestroy()
    {
        if (currentHand != null)
            currentHand.GetGrabController().ClearAllGrabbableInfo();
    }
}
