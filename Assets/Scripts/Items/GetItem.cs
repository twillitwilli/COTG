using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    [SerializeField] 
    VRPlayer player;
    
    public LayerMask ignoreLayers;
    public Transform raycastDirection;
    public GameObject effect;

    public int handCheck;
    
    public bool canGetItem { get; set; }

    bool onlyRunOnce;
    float range;

    void Update()
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

    void ShootRayCast()
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
