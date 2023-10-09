using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    VRPlayer _player;
    PlayerComponents _playerComponents;

    public NetworkPlayerComponents components;
    
    private PhotonView photonView;
    
    private Transform 
        headRig, 
        leftHandRig, 
        rightHandRig, 
        playerTargeting;

    //add health and arcane stats for hand display
    public bool isDead { get; set; }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        _player = LocalGameManager.Instance.player;
        _playerComponents = _player.GetPlayerComponents();

        photonView = GetComponent<PhotonView>();

        headRig = _player.head;
        leftHandRig = _playerComponents.GetHand(0).GetHandModel().transform;
        rightHandRig = _playerComponents.GetHand(1).GetHandModel().transform;
        playerTargeting = _playerComponents.playerTarget;

        if (photonView.IsMine)
        {
            foreach (var meshRenderers in GetComponentsInChildren<Renderer>())
            {
                meshRenderers.enabled = false;
            }
        }
    }

    public void Update()
    {
        if (photonView.IsMine)
        {
            MapPosition(components.head, headRig);
            MapPosition(components.hand[0], leftHandRig);
            MapPosition(components.hand[1], rightHandRig);
            MapPosition(components.playerTarget, playerTargeting);
        }
    }

    void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}
