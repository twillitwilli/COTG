using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InifiniteJarSpawner : MonoBehaviour
{
    public GameObject jarPrefab;
    public Transform[] spawnLocations;
    public GameObject[] spawnedJars;

    public void LateUpdate()
    {
        for (int i = 0; i < spawnedJars.Length; i++)
        {
            if (spawnedJars[i] == null)
            {
                GameObject newJar = Instantiate(jarPrefab, spawnLocations[i]);
                spawnedJars[i] = newJar;
            }
        }
    }
}
