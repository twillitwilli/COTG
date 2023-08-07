using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabOnTrigger : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Left Hand") || other.gameObject.CompareTag("Right Hand")) { SpawnPrefab(); }
    }

    private void SpawnPrefab()
    {
        Instantiate(prefab, spawnLocation);
    }
}
