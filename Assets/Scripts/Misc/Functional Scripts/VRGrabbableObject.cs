using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonRigidbodyView))]
public class VRGrabbableObject : MonoBehaviour
{
    public enum typeOfObject { grabbablePickup, bigGrabbablePickup, heavyPickup, pushable, climbable };
    [Tooltip("Select weather the object is Grabbable or Climbable")]
    public typeOfObject objectType;

    public bool canEquip;

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

    [HideInInspector] public VRPlayerHand activeHand = null;
    [HideInInspector] public VRPlayerHand activeHandRight = null;
    [HideInInspector] public VRPlayerHand activeHandLeft = null;
    [HideInInspector] public List<TelekinesisRaycast> activeTelekinesis = new List<TelekinesisRaycast>();
    [HideInInspector] public bool insideObject, insidePocket, attachedToPocket, setThrownCD, disableThrowCD, highlightedGrabbable;
    [HideInInspector] public int insideObjectInt;
    [HideInInspector] public VRSockets pocket;
    [HideInInspector] public Rigidbody rb;
    private float thrownCDTimer;
    [HideInInspector] public PhotonView photonView;
    private PhotonRigidbodyView photonRBView;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    //Switch between Grabbable or Climbable
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        ChangeSettings();
        gameObject.layer = LayerMask.NameToLayer("Grabbable");
    }

    private void LateUpdate()
    {
        switch (objectType)
        {
            case typeOfObject.grabbablePickup:
                GrabController();
                break;
        }
    }

    private void GrabController()
    {
        if (activeTelekinesis.Count > 0 && !highlightedGrabbable) { highlightedGrabbable = true; }
        else if (activeTelekinesis.Count <= 0 && highlightedGrabbable) { highlightedGrabbable = false; }
        if (attachedToPocket)
        {
            if (CoopManager.instance != null) { photonView.OwnershipTransfer = OwnershipOption.Fixed; }
            transform.position = pocket.transform.position;
            transform.rotation = pocket.transform.rotation;
        }
        else if (!disableThrowCD && !attachedToPocket && !JustThrown())
        {
            Collider collider = GetComponent<Collider>();
            collider.isTrigger = false;
            disableThrowCD = true;
        }
        if (!highlightedGrabbable && meshRenderer.material != normalMat) { DefaultMaterial(); }
        else if (highlightedGrabbable && meshRenderer.material != canGrabMat) { meshRenderer.material = canGrabMat; }
    }

    public bool JustThrown()
    {
        if (setThrownCD)
        {
            thrownCDTimer = 0.15f;
            setThrownCD = false;
        }
        if (thrownCDTimer > 0) { thrownCDTimer -= Time.deltaTime; }
        else if (thrownCDTimer <= 0)
        {
            return false;
        }
        return true;
    }

    public void ObjectGrabbed()
    {
        photonView.RequestOwnership();
    }

    private void OnTriggerEnter(Collider other)
    {
        //inside wall check
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground"))
        {
            insideObjectInt++;
            insideObject = true;
        }
        if (canEquip && other.gameObject.CompareTag("Pocket")) { insidePocket = true; pocket = other.gameObject.GetComponent<VRSockets>(); }
    }

    private void OnTriggerExit(Collider other)
    {
        //inside wall check
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground"))
        {
            insideObjectInt--;
            if (insideObjectInt <= 0)
            {
                insideObjectInt = 0;
                insideObject = false;
            }
        }
        if (canEquip && other.gameObject.CompareTag("Pocket")) { insidePocket = false; pocket = null; }
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

    public void ChangeSettings()
    {
        switch (objectType)
        {
            case typeOfObject.grabbablePickup:
                gameObject.tag = "Grabbable";
                if (CoopManager.instance != null)
                {
                    photonView = GetComponent<PhotonView>();
                    photonView.OwnershipTransfer = OwnershipOption.Takeover;
                    photonRBView = GetComponent<PhotonRigidbodyView>();
                    photonRBView.m_TeleportEnabled = true;
                    photonRBView.m_SynchronizeVelocity = true;
                    photonRBView.m_SynchronizeAngularVelocity = true;
                }
                break;
            case typeOfObject.bigGrabbablePickup:
                gameObject.tag = "Big Grabbable";
                break;
            case typeOfObject.heavyPickup:
                gameObject.tag = "Heavy Grabbable";
                break;
            case typeOfObject.pushable:
                gameObject.tag = "Pushable";
                break;
            case typeOfObject.climbable:
                rb.useGravity = false;
                rb.isKinematic = true;
                gameObject.tag = "Climb Point";
                break;
        }
    }

    public void DefaultMaterial()
    {
        meshRenderer.material = normalMat;
    }

    private void OnDestroy()
    {
        //if (activeHand != null) { activeHand.GrabbableReset(); }
    }
}
