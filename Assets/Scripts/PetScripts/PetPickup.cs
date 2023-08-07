using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetPickup : MonoBehaviour
{
    [SerializeField] private PlayerPetController petController;
    public float pickupRange;
    public Transform spawnLocation;
    public List<GameObject> petHoldingItems;

    [HideInInspector] public bool isHoldingItem;
    private bool setPickupCooldown, foundPickup;
    private float currentLowestDistance = 100, cooldownTimer;
    [HideInInspector] public GameObject closestObj;
    private List<GameObject> pickupableItems = new List<GameObject>();

    public void FixedUpdate()
    {
        if (petController.pet.pickingUpItem && closestObj == null) { ClearItemChecks(); }
        if (!isHoldingItem && !petController.pet.pickingUpItem && PickupCooldown()) { GetClosestItem(); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (petController.pet.pickingUpItem && other.gameObject.GetComponent<Item>() && other.gameObject.GetComponent<Item>().PetCanPickup())
        {
            other.gameObject.GetComponent<Item>().PetPickingUp(this);
            ClearItemChecks();
        }
    }

    private void GetClosestItem()
    {
        Collider[] itemsAround = Physics.OverlapSphere(transform.position, (pickupRange / 2));
        for (int i = 0; i < itemsAround.Length; i++)
        {
            if (itemsAround[i].GetComponent<Item>() && itemsAround[i].GetComponent<Item>().PetCanPickup())
            {
                pickupableItems.Add(itemsAround[i].gameObject);
            }
        }
        for (int i = 0; i < pickupableItems.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, pickupableItems[i].gameObject.transform.position);
            if (distance < currentLowestDistance)
            {
                foundPickup = true;
                closestObj = pickupableItems[i].gameObject;
            }
        }
        if (foundPickup)
        {
            petController.pet.changedTarget = true;
            petController.pet.pickingUpItem = true;
        }
        setPickupCooldown = true;
    }

    public bool PickupCooldown()
    {
        if (setPickupCooldown)
        {
            cooldownTimer = Random.Range(5, 10);
            setPickupCooldown = false;
        }
        if (cooldownTimer > 0) { cooldownTimer -= Time.deltaTime; }
        else if (cooldownTimer <= 0)
        {
            cooldownTimer = 0;
            return true;
        }
        return false;
    }

    public void ClearItemChecks()
    {
        currentLowestDistance = 100;
        petController.pet.pickingUpItem = false;
        petController.pet.changedTarget = true;
        closestObj = null;
        pickupableItems.Clear();
    }

    public void ResetTransform(GameObject obj)
    {
        obj.transform.SetParent(spawnLocation);
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localEulerAngles = new Vector3(0, 0, 0);
        obj.transform.localScale = new Vector3(2, 2, 2);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
