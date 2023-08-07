using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObjects : MonoBehaviour
{
    public GameObject[] environmentObjects;

    private void Start()
    {
        for (int i = 0; i < environmentObjects.Length; i++)
        {
            if (environmentObjects[i].GetComponent<BreakableObject>())
            {
                BreakableObject breakableObj = environmentObjects[i].GetComponent<BreakableObject>();
                if (breakableObj.objectType == BreakableObject.BreakableObjectType.rock) { breakableObj.objectID = i; }
            }
        }
    }
}
