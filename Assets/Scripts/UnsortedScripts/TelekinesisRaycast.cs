using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisRaycast : MonoBehaviour
{
    [SerializeField] 
    VRPlayer player;
    
    public VRHand hand;
    public Transform telekineticHold;
    
    [SerializeField] 
    GameObject rayEffect;
    
    public GameObject telekineticGrabEffect;
    public float range;
    public LayerMask ignoreLayers;
    
    public PlayerItemGrabbable selectedGrabbable { get; set; }

    public float cooldownTimer;
    
    public float maxTimer { get; set; }
    public bool setCooldown { get; set; }
    
    public PlayerItemGrabbable heldObject { get; set; }

    void Awake()
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

    void SelectNewGrabbable(PlayerItemGrabbable newGrabbable)
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
