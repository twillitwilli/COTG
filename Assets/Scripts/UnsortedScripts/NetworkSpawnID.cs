using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkSpawnID : MonoBehaviour
{
    public NetworkSpawnTemplate.SpawnType typeOfSpawn;
    public int spawnID;

    [HideInInspector] 
    public int spawnCountID;

    private void Start()
    {
        if (MultiplayerManager.Instance.coop && LocalGameManager.Instance.isHost)
        {
            GameObject newTemplate = PhotonNetwork.Instantiate("NetworkSpawnTemplate", transform.position, transform.rotation);
            
            spawnCountID = MultiplayerManager.Instance.GetNetworkManager().networkSpawnedObjects++;
            TemplateSettings(newTemplate.GetComponent<NetworkSpawnTemplate>());
            
            MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.spawnedPrefabs.Add(newTemplate);
        }
    }

    private void TemplateSettings(NetworkSpawnTemplate newTemplate)
    {
        MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.spawnedObjects++;

        spawnCountID = MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.spawnedObjects;

        newTemplate.spawnCountID = spawnCountID;
        newTemplate.spawnType = typeOfSpawn;
        newTemplate.spawnID = spawnID;
    }
}
