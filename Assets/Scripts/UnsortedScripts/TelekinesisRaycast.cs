using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisRaycast : MonoBehaviour
{
    [SerializeField] 
    private VRPlayerController player;
    
    public VRPlayerHand hand;
    public Transform telekineticHold;
    
    [SerializeField] 
    private GameObject rayEffect;
    
    public GameObject telekineticGrabEffect;
    public float range;
    public LayerMask ignoreLayers;
    
    [HideInInspector] 
    public PlayerItemGrabbable selectedGrabbable;

    public float cooldownTimer;
    
    [HideInInspector] 
    public float maxTimer;
    
    [HideInInspector] 
    public bool setCooldown;
    
    [HideInInspector] 
    public PlayerItemGrabbable heldObject;

    private void Awake()
    {
        maxTimer = cooldownTimer;
    }


    public void FixedUpdate()
    {
        if (CooldownDone())
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.up, out hit, range, -ignoreLayers))
            {
                if (!rayEffect.activeSelf)
                    rayEffect.SetActive(true);

                if (hit.collider.CompareTag("Grabbable") && hit.collider.GetComponent<PlayerItemGrabbable>() && hit.collider.GetComponent<PlayerItemGrabbable>().canTelekineticGrab)
                {
                    if (selectedGrabbable && selectedGrabbable != hit.collider.GetComponent<PlayerItemGrabbable>())
                    {
                        selectedGrabbable.activeTelekinesis.Remove(this);
                        SelectNewGrabbable(hit.collider.GetComponent<PlayerItemGrabbable>());
                        return;
                    }
                    else { SelectNewGrabbable(hit.collider.GetComponent<PlayerItemGrabbable>()); }
                    return;
                }
                return;
            }
            else
            {
                if (rayEffect.activeSelf) { rayEffect.SetActive(false); }
                if (selectedGrabbable != null)
                {
                    selectedGrabbable.activeTelekinesis.Remove(this);
                    selectedGrabbable = null;
                }
            }
        }
    }

    public bool CooldownDone()
    {
        if (setCooldown)
        {
            cooldownTimer = maxTimer;
            setCooldown = false;
        }
        if (cooldownTimer > 0) { cooldownTimer -= Time.deltaTime; }
        else if (cooldownTimer <= 0)
        {
            cooldownTimer = 0;
            return true;
        }
        return false;
    }

    private void SelectNewGrabbable(PlayerItemGrabbable newGrabbable)
    {
        selectedGrabbable = newGrabbable;
        if (!selectedGrabbable.activeTelekinesis.Contains(this)) 
        {
            selectedGrabbable.activeTelekinesis.Clear();
            selectedGrabbable.activeTelekinesis.Add(this); 
        }
        //hand.grabOption = VRPlayerHand.GrabType.grabObject;
    }
}
