using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [Tooltip("0 = 1st axis, 1 = 2nd axis, 2 = 3rd axis")]
    public int rotateState;

    public float rotationSpeed = 30.0f;

    private void LateUpdate()
    {
        if (rotateState < 0)
        {
            rotateState = 0;
        }

        if (rotateState == 0)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
        }
        
        else if (rotateState == 1)
        {
            transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime, Space.Self);
        }

        else if (rotateState == 2)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);
        }

        if (rotateState > 2)
        {
            rotateState = 2;
        }
    }
}
