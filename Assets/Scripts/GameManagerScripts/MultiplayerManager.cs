using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.AbstractClasses;

public class MultiplayerManager : MonoSingleton<MultiplayerManager>
{
    public bool coop { get; private set; }
    public void ToggleCoop(bool coopStatus)
    {
        coop = coopStatus;
    }

    [SerializeField]
    NetworkManager _networkManager;

    public NetworkManager GetNetworkManager() { return _networkManager; }

    [SerializeField]
    CoopManager _coopManager;

    public CoopManager GetCoopManager() { return _coopManager; }
}
