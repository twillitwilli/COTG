using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekineticGrab : MonoBehaviour
{
    [SerializeField] 
    VRHand hand;

    public float 
        range = 1, 
        diameter = 0.5f;

    PlayerItemGrabbable closestObject;

    public PlayerItemGrabbable TelekinesisGrab()
    {
        Collider[] grabbableObjects = Physics.OverlapBox(transform.position, new Vector3(diameter, range, diameter), transform.rotation);
        
        if (grabbableObjects.Length > 0)
        {
            for (int i = 0; i < grabbableObjects.Length; i++)
            {
                if (grabbableObjects[i].GetComponent<PlayerItemGrabbable>() != null)
                {
                    if (closestObject == null)
                        closestObject = grabbableObjects[i].GetComponent<PlayerItemGrabbable>();

                    else
                    {
                        float currentObjDistance = Vector3.Distance(hand.transform.position, closestObject.transform.position);
                        float checkNewObjDistance = Vector3.Distance(hand.transform.position, grabbableObjects[i].transform.position);
                        
                        if (checkNewObjDistance < currentObjDistance)
                            closestObject = grabbableObjects[i].GetComponent<PlayerItemGrabbable>();
                    }
                }
            }

            if (closestObject != null)
                return closestObject;
        }

        return null;
    }
}
