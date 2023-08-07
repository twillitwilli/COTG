using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtObject : MonoBehaviour
{
    public GameObject objectToAimAt;

    public void LateUpdate()
    {
        if (objectToAimAt != null) { transform.LookAt(objectToAimAt.transform); }
    }
}
