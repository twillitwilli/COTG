using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSpikeSpawners : MonoBehaviour
{
    public GameObject spikePrefab;
    public List<Transform> spawnPoints;
    private int createdSpikes;

    private void Start()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Invoke("SpawnSpike", 0.25f);
        }
    }

    private void SpawnSpike()
    {
        GameObject newSpike = Instantiate(spikePrefab, spawnPoints[createdSpikes]);
        createdSpikes++;
    }
}
