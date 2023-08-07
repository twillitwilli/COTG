using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    [SerializeField] private VRPlayerController player;
    public LayerMask ignoreLayers;
    public Transform raycastDirection;
    public GameObject effect;

    //[HideInInspector]
    public int handCheck;
    [HideInInspector]
    public bool canGetItem;

    private bool onlyRunOnce;
    private float range;

    private void Update()
    {
        if (canGetItem)
        {
            effect.SetActive(true);
            Invoke("ShootRayCast", 1f);
        }

        if (onlyRunOnce && !canGetItem)
        {
            effect.SetActive(false);
            onlyRunOnce = false;
        }
    }

    public void CheckIfCanGetItem()
    {
        if (handCheck >= 2) { canGetItem = true; }
        else if (handCheck < 2) { canGetItem = false; }
    }

    private void ShootRayCast()
    {
        if (canGetItem)
        {
            RaycastHit hit;
            range = Vector3.Distance(raycastDirection.position, transform.position);
            if (Physics.Raycast(transform.position, raycastDirection.position - transform.position, out hit, range, -ignoreLayers))
            {
                ItemScrollTrigger itemScrollTrigger;
                if (hit.collider.gameObject.TryGetComponent<ItemScrollTrigger>(out itemScrollTrigger))
                {
                    itemScrollTrigger.AbsorbScrollKnowledge();
                }
            }
            canGetItem = false;
            onlyRunOnce = true;
        }
    }
}
