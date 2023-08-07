using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnTransform : MonoBehaviour
{
    public Transform targetTransform;

    private void Update()
    {
        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
    }
}
