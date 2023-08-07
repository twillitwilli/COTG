using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentTrigger : MonoBehaviour
{
    [Header("Half Scale of cube size in world space")]
    public Vector3 sizeOfBox;

    public bool destroyAllObjects, parentTo, destroyParentsOfObjects, unparentFrom, grabRigidbody, useGravity, isKinematic;
    
    public string tagToLookFor;

    [Tooltip("Transform must be child and default transform settings")]
    public Transform ifParentToTransform;

    public void CastOverlapBox()
    {
        Collider[] grabbableObjects = Physics.OverlapBox(transform.position, sizeOfBox, transform.rotation);
        foreach (Collider col in grabbableObjects)
        {
            if (col.gameObject.CompareTag(tagToLookFor))
            {
                if (grabRigidbody)
                {
                    Rigidbody rb = col.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        if (useGravity)
                        {
                            rb.useGravity = true;
                        }

                        else if (!useGravity)
                        {
                            rb.useGravity = false;
                        }

                        if (isKinematic)
                        {
                            rb.isKinematic = true;
                        }

                        else if (!isKinematic)
                        {
                            rb.isKinematic = false;
                        }
                    }
                }

                if (parentTo)
                {
                    col.transform.SetParent(ifParentToTransform);
                }

                else if (unparentFrom)
                {
                    col.transform.SetParent(null);
                }

                if (destroyParentsOfObjects)
                {
                    Destroy(col.transform.parent.gameObject);
                }
            }
        }
    }
}
