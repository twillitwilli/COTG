using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CoopManager : MonoBehaviourPun
{
    private LocalGameManager _gameManager;
    private VRPlayerController _currentPlayer;

    public static CoopManager instance;
    public GameObject otherPlayerTracker;

    [HideInInspector] public PhotonView photonComponent;
    [HideInInspector] public CoopDungeonBuild coopDungeonBuild;
    [HideInInspector] public CoopEnemyController coopEnemyController;
    [HideInInspector] public bool isMaster, isDead, playerIDCreated, allPlayersDead, portalActive, isHardMode, closeOtherPortals;
    [HideInInspector] public int totalPlayers, playerID;
    [HideInInspector] public Vector3 otherPlayerTargetPos;

    private void Awake()
    {
        _gameManager = LocalGameManager.Instance;
        _currentPlayer = _gameManager.player;
    }

    private void OnEnable()
    {
        if (!instance) { instance = this; }
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        isMaster = true;
        _gameManager.isHost = true;

        photonComponent = GetComponent<PhotonView>();
        coopDungeonBuild = GetComponent<CoopDungeonBuild>();
        coopEnemyController = GetComponent<CoopEnemyController>();
    }

    public void Update()
    {
        if (!_gameManager.GetPlayerStats().IsPlayerDead())
        {
            if (isMaster && totalPlayers > 0) { SendPlayerTrackingInfo(_currentPlayer.GetPlayerComponents().playerTarget.position); }
            else if (!isMaster) { SendPlayerTrackingInfo(_currentPlayer.GetPlayerComponents().playerTarget.position); }
        }
        else
        {
            if (isMaster && totalPlayers > 0) { SendPlayerTrackingInfo(new Vector3(10000, 10000, 10000)); }
            else if (!isMaster) { SendPlayerTrackingInfo(new Vector3(10000, 10000, 10000)); }
        }
    }

    public void SendChatMessage(string message)
    {
        photonComponent.RPC("ChatMessage", RpcTarget.Others, message);
    }

    [PunRPC]
    public void ChatMessage(string message)
    {
        _gameManager.GetChatManager().ChatMessage("[From Other Player] " + message);
    }

    public void SendPlayerTrackingInfo(Vector3 position)
    {
        photonComponent.RPC("UpdateOtherPlayerTargeting", RpcTarget.Others, position.x, position.y, position.z);
    }

    [PunRPC]
    public void UpdateOtherPlayerTargeting(int posX, int posY, int posZ)
    {
        otherPlayerTracker.transform.position = new Vector3(posX, posY, posZ);
    }

    [PunRPC]
    public void NewPlayerJoined()
    {
        totalPlayers++;
        if (isMaster) 
        { 
            photonComponent.RPC("MasterClientExists", RpcTarget.Others);
            if (coopDungeonBuild.spawnedPrefabs.Count > 0)
            {
                for (int i = 0; i < coopDungeonBuild.spawnedPrefabs.Count; i++)
                {
                    if (coopDungeonBuild.spawnedPrefabs[i] != null)
                    {
                        NetworkSpawnTemplate networkSpawnedTemplate = coopDungeonBuild.spawnedPrefabs[i].GetComponent<NetworkSpawnTemplate>();
                        string spawnType = networkSpawnedTemplate.spawnTypeName;
                        int spawnID = networkSpawnedTemplate.spawnID;
                        int spawnCountID = networkSpawnedTemplate.spawnCountID;
                        photonComponent.RPC("SpawnNewObject", RpcTarget.Others, spawnType, spawnID, spawnCountID);
                    }
                }
            }
        }
    }

    [PunRPC]
    public void MasterClientExists()
    {
        totalPlayers++;
        isMaster = false;
        _gameManager.isHost = false;
        CheckIfPortalActive();
    }

    public void PlayerLeft()
    {
        totalPlayers--;
        if (!isMaster)
        {
            isMaster = true;
            _gameManager.isHost = true;
        }
        otherPlayerTracker.transform.position = new Vector3(10000, 10000, 10000);
    }

    public void PlayerDied()
    {
        if (isMaster && totalPlayers == 0) { AllPlayersDead(); }
        else { photonComponent.RPC("OtherPlayerDied", RpcTarget.Others); }
    }

    [PunRPC]
    public void OtherPlayerDied()
    {
        if (isDead) { photonComponent.RPC("AllPlayersDead", RpcTarget.All); }
    }

    [PunRPC]
    public void AllPlayersDead()
    {
        _gameManager.GetTotalStats().SavePlayerProgress(_gameManager.GetPlayerStats().saveFile);
        _currentPlayer.GetPlayerComponents().resetPlayer.ResetPlayer(true);
    }

    [PunRPC]
    public void NewObjectSettings(string spawnType, int spawnID, int spawnCountID)
    {
        if (spawnType == "LoadingAreas") { SpawnNewObject(NetworkSpawnTemplate.SpawnType.loadingAreas, spawnID, spawnCountID); }
        else if (spawnType == "Rooms") { SpawnNewObject(NetworkSpawnTemplate.SpawnType.rooms, spawnID, spawnCountID); }
        else { Debug.Log("Network SpawnType no match found"); }
    }

    public void SpawnNewObject(NetworkSpawnTemplate.SpawnType spawnName, int spawnID, int spawnCountID)
    {
        switch (spawnName)
        {
            case NetworkSpawnTemplate.SpawnType.loadingAreas:
                break;
        }
    }

    public void CheckIfPortalActive()
    {
        photonComponent.RPC("PortalStatus", RpcTarget.Others);
    }

    [PunRPC]
    public void PortalStatus()
    {
        if (portalActive) { photonComponent.RPC("PortalActive", RpcTarget.Others, portalActive, isHardMode); }
    }

    [PunRPC]
    public void PortalActive(bool active, bool isHard)
    {
        portalActive = active;
        isHardMode = isHard;
        closeOtherPortals = true;

        if (isHard) { _gameManager.currentGameMode = LocalGameManager.GameMode.master; }
        else { _gameManager.currentGameMode = LocalGameManager.GameMode.normal; }
    }

    public void MoveOtherPlayer()
    {
        photonComponent.RPC("MovePlayerIntoDungeon", RpcTarget.Others);
    }

    [PunRPC]
    public void MovePlayerIntoDungeon()
    {
        coopDungeonBuild.ClearDungeonRoomList();
        coopDungeonBuild.dungeonCompleted = false;
        _gameManager.loadDungeon = true;
        _gameManager.dungeonBuildCompleted = false;
        _gameManager.Loading(LocalGameManager.SceneSelection.dungeon);
    }
}
