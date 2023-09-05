using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private OnScreenText _onScreenText;

    public bool autoConnect;
    public bool sendChatMessage;
    public string chatMessage;

    [HideInInspector] 
    public string roomName;
    
    [HideInInspector] 
    public GameObject networkPlayer, coopManager;
    
    [HideInInspector] 
    public int connectedPlayers, networkSpawnedObjects;

    private void Awake()
    {
        LocalGameManager.playerCreated += ConfigureSettingsForNewPlayer;
    }

    public void ConfigureSettingsForNewPlayer(VRPlayerController player)
    {
        _onScreenText = player.GetPlayerComponents().onScreenText;

        if (autoConnect)
        {
            roomName = "testRoom";
            ConnectToServer();
        }
    }

    public void ConnectToServer()
    {
        if (roomName != null)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Trying to connect to server...");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect to server");

        base.OnConnectedToMaster();

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();

        _onScreenText.PrintText("Created Coop Room", true);
        coopManager = PhotonNetwork.Instantiate("Coop Manager", transform.position, transform.rotation);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        _onScreenText.PrintText("New Player Joined Room", true);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        networkPlayer = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
        _onScreenText.PrintText("Joined Room", true);
    }

    public void SpawnNetworkTemplate(NetworkSpawnTemplate.SpawnType spawnType, bool randomSpawn)
    {
        GameObject newTemplate = PhotonNetwork.Instantiate("NetworkSpawnTemplate", transform.position, transform.rotation);
        NetworkSpawnTemplate templateSettings = newTemplate.GetComponent<NetworkSpawnTemplate>();
        templateSettings.spawnType = spawnType;
        templateSettings.randomSpawn = randomSpawn;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        MultiplayerManager.Instance.GetCoopManager().PlayerLeft();
        _onScreenText.PrintText("Left Multiplayer", true);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        PhotonNetwork.Destroy(networkPlayer);
        PhotonNetwork.Destroy(coopManager);

        LocalGameManager.Instance.isHost = false;

        MultiplayerManager.Instance.ToggleCoop(false);
    }
}
