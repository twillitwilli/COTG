using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapWalletSpawner : MonoBehaviour
{
    private VRPlayerController _player;
    private PlayerComponents _playerComponents;
    private PlayerStats _playerStats;

    public Transform[] accSlot;

    [HideInInspector] public GameObject rolledUpMapObject, openedMapObject, walletObject;

    private void Start()
    {
        _player = LocalGameManager.Instance.player;
        _playerComponents = _player.GetPlayerComponents();
        _playerStats = LocalGameManager.Instance.GetPlayerStats();

        Invoke("NewPlayer", 1);
    }

    private void NewPlayer()
    {
        SpawnNewMap();
        SpawnNewWallet(LocalGameManager.Instance.GetPlayerStats().GetCurrentGold());
        GetComponent<HandBombKeyController>().ChangeCrystalHands();
    }

    // MAP SETTINGS

    public void SpawnNewMap()
    {
        GameObject map = Instantiate(MasterManager.playerManager.rolledUpMap);
        rolledUpMapObject = map;

        for (int i = 0; i < _playerComponents.GetBothHands().Length; i++)
        {
            if (_playerComponents.GetBothHands()[i].IsPrimaryHand())
            {
                map.transform.SetParent(accSlot[i]);
            }
        }

        map.transform.localPosition = new Vector3(0, 0, 0);
        map.transform.localEulerAngles = new Vector3(90, 0, 0);
        map.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }

    public void GrabMap(GrabController grabController)
    {
        VRPlayerHand hand = grabController.GetHand();
        HandAnimationState handAnimator = hand.GetHandAnimationState();

        GrabController oppositeHandGrabController = hand.GetOppositeHand().GetGrabController();

        if (rolledUpMapObject) { Destroy(rolledUpMapObject); }
        if (openedMapObject == null) { openedMapObject = Instantiate(MasterManager.playerManager.mapInHand); }
        openedMapObject.GetComponent<MapItem>().player = _player;
        if (!hand.IsRightHand())
        {
            Vector3 mapPos = new Vector3(0.004255578f, -0.008148912f, -0.1995229f);
            Vector3 mapRot = new Vector3(88.854f, 0.712f, 93.76f);
            Vector3 mapScale = new Vector3(0.02499999f, 0.01f, 0.02499999f);
            grabController.ParentGrabbable(openedMapObject, mapPos, mapRot, mapScale);
        }
        else
        {
            Vector3 mapPos = new Vector3(0.0121588f, -0.02328259f, -0.5700652f);
            Vector3 mapRot = new Vector3(88.854f, 0.712f, 93.76f);
            Vector3 mapScale = new Vector3(-0.07142855f, 0.02857143f, 0.07142855f);
            grabController.ParentGrabbable(openedMapObject, mapPos, mapRot, mapScale);
        }
        handAnimator.SwitchHandState(HandAnimationState.HandState.holdingMap);

        if (oppositeHandGrabController.CheckIfHoldingSpecificThing(1))
        {
            oppositeHandGrabController.ReleaseGrip();
        }
    }

    public void ResetMap(GrabController grabController)
    {
        GrabController oppositeHandGrabController = grabController.GetHand().GetOppositeHand().GetGrabController();

        if (!oppositeHandGrabController.CheckIfHoldingSpecificThing(1))
        {
            if (openedMapObject != null) { Destroy(openedMapObject); }
            if (rolledUpMapObject != null) { Destroy(rolledUpMapObject); }
            SpawnNewMap();
        }
    }

    // WALLET SETTINGS

    public void SpawnNewWallet(int goldAmount)
    {
        walletObject = Instantiate(MasterManager.playerManager.wallet);

        for (int i = 0; i < _playerComponents.GetBothHands().Length; i++)
        {
            if (!_playerComponents.GetBothHands()[i].IsPrimaryHand())
            {
                walletObject.transform.SetParent(accSlot[i]);
            }
        }

        walletObject.transform.localPosition = new Vector3(0, 0, 0);
        walletObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        walletObject.transform.localScale = new Vector3(1, 1, 1);
        CurrentGoldDisplay goldDisplay = walletObject.GetComponentInChildren<CurrentGoldDisplay>();
        goldDisplay.UpdateDisplay(goldAmount);
    }

    public void GrabWallet(GrabController grabController)
    {
        VRPlayerHand hand = grabController.GetHand();
        HandAnimationState handAnimator = hand.GetHandAnimationState();

        GrabController oppositeHandGrabController = hand.GetOppositeHand().GetGrabController();

        walletObject.GetComponent<WalletItem>().player = _player;
        CurrentGoldDisplay goldDisplay = walletObject.GetComponentInChildren<CurrentGoldDisplay>();
        goldDisplay.walletInHand = true;
        goldDisplay.player = _player;
        if (!grabController.GetHand().IsRightHand()) 
        {
            Vector3 walletPos = new Vector3(-0.042f, 0.01379f, -0.06352501f);
            Vector3 walletRot = new Vector3(-86.95f, 78.667f, 97.206f);
            Vector3 walletScale = new Vector3(5, 5, 5);
            grabController.ParentGrabbable(walletObject, walletPos, walletRot, walletScale);
        }
        else 
        {
            Vector3 walletPos = new Vector3(-0.03900001f, 0.035f, -0.154f);
            Vector3 walletRot = new Vector3(-86.95f, 78.667f, 97.206f);
            Vector3 walletScale = new Vector3(-14.28571f, 14.28571f, 14.28571f);
            grabController.ParentGrabbable(walletObject, walletPos, walletRot, walletScale);
        }
        handAnimator.SwitchHandState(HandAnimationState.HandState.holdingWallet);
        if (grabController.CheckIfHoldingSpecificThing(2))
        {
            oppositeHandGrabController.ReleaseGrip();
        }
    }

    public void ResetWallet(GrabController grabController)
    {
        if (!grabController.GetHand().GetOppositeHand().GetGrabController().CheckIfHoldingSpecificThing(2))
        {
            if (walletObject != null) { Destroy(walletObject); }
            SpawnNewWallet(_playerStats.GetCurrentGold());
        }
    }
}
