using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnDestroy : MonoBehaviour
{
    public List<GameObject> objsToDestroy;

    public void OnDestroy()
    {
        foreach (GameObject obj in objsToDestroy)
        {
            Destroy(obj);
        }
    }
}
