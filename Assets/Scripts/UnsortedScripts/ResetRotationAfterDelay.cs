using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotationAfterDelay : MonoBehaviour
{
    public float delay;

    private void Start()
    {
        Invoke("ResetRotation", delay);
    }

    private void ResetRotation()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
