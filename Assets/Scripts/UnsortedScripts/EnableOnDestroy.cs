using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnDestroy : MonoBehaviour
{
    public GameObject[] objectsToEnable;

    private void OnDestroy()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
    }
}
