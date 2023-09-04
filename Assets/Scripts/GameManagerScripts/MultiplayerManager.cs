using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoSingleton<MultiplayerManager>
{
    [SerializeField]
    private NetworkManager _networkManager;

    public NetworkManager GetNetworkManager() { return _networkManager; }
}
