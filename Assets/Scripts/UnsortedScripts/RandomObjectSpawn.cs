using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSpawn : MonoBehaviour
{
    [Range(0, 100)]
    public int spawnChance;
    public GameObject[] objectsThatCanSpawn;

    private void Start()
    {
        int randomObject = Random.Range(0, objectsThatCanSpawn.Length);
        int spawn = Random.Range(0, 100);
        if (spawn < spawnChance) 
        { 
            GameObject spawnedObject = Instantiate(objectsThatCanSpawn[randomObject]);
            spawnedObject.transform.SetParent(transform);
            spawnedObject.transform.localPosition = new Vector3(0, 0, 0);
            spawnedObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            spawnedObject.transform.localScale = new Vector3(0, 0, 0);
            Transform parentTransform = GetComponentInParent<Transform>();
            spawnedObject.transform.SetParent(parentTransform);
            Destroy(gameObject);
        }
    }
}
