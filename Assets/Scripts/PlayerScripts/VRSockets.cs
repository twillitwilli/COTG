using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSockets : MonoBehaviour
{
    public Vector3 originalScale { get; set; }

    public void AttachObject(PlayerItemGrabbable obj)
    {
        obj.transform.SetParent(transform);
        obj.attachedToPocket = true;
        originalScale = gameObject.transform.localScale;
        obj.transform.localScale /= 2;
    }

    public void DetachObject(PlayerItemGrabbable obj)
    {
        obj.transform.localScale = originalScale;
        obj.attachedToPocket = false;
    }

    public void EmptyPockets()
    {
        if (transform.childCount > 0) { Destroy(transform.GetChild(0).gameObject); }
    }
}
