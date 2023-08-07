using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkSpawnID : MonoBehaviour
{
    public NetworkSpawnTemplate.SpawnType typeOfSpawn;
    public int spawnID;
    [HideInInspector] public int spawnCountID;

    private void Start()
    {
        if (CoopManager.instance != null && LocalGameManager.instance.isHost)
        {
            GameObject newTemplate = PhotonNetwork.Instantiate("NetworkSpawnTemplate", transform.position, transform.rotation);
            spawnCountID = LocalGameManager.instance.GetNetworkManager().networkSpawnedObjects++;
            TemplateSettings(newTemplate.GetComponent<NetworkSpawnTemplate>());
            CoopManager.instance.coopDungeonBuild.spawnedPrefabs.Add(newTemplate);
        }
    }

    private void TemplateSettings(NetworkSpawnTemplate newTemplate)
    {
        CoopManager.instance.coopDungeonBuild.spawnedObjects++;
        spawnCountID = CoopManager.instance.coopDungeonBuild.spawnedObjects;
        newTemplate.spawnCountID = spawnCountID;
        newTemplate.spawnType = typeOfSpawn;
        newTemplate.spawnID = spawnID;
    }
}
