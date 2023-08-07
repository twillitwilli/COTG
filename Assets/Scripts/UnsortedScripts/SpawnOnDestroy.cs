using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
    public GameObject spawnObject;
    public Vector3 offsetSpawn;
    [HideInInspector]
    public bool disableSpawn = true;

    private void Awake()
    {
        disableSpawn = true;
    }

    private void OnDestroy()
    {
        if (!disableSpawn)
        {
            GameObject newEffect = Instantiate(spawnObject, transform.position + offsetSpawn, transform.rotation);
            newEffect.transform.SetParent(null);
        }
    }
}
