using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransform : MonoBehaviour
{
    public Transform anchor, resetObject;

    public void FixedUpdate()
    {
        Invoke("ResetDelay", 1);
    }

    private void ResetDelay()
    {
        if (resetObject.position != anchor.position)
        {
            resetObject.position = anchor.position;
        }
    }
}
