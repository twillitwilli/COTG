using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletController : MonoSingleton<WalletController>
{
    private PlayerComponents _playerComponents;

    [SerializeField]
    private GameObject _walletPrefab;

    private GameObject _walletObject;

    private CurrentGoldDisplay _goldDisplay;

    private void Awake()
    {
        LocalGameManager.playerCreated += NewPlayerCreated;
    }

    public async void NewPlayerCreated(VRPlayerController player)
    {
        _playerComponents = player.GetPlayerComponents();

        SpawnNewWallet(PlayerStats.Instance.GetCurrentGold());
    }

    public void SpawnNewWallet(int goldAmount)
    {
        _walletObject = Instantiate(_walletPrefab);

        for (int i = 0; i < _playerComponents.GetBothHands().Length; i++)
        {
            if (!_playerComponents.GetBothHands()[i].IsPrimaryHand())
            {
                _walletObject.transform.SetParent(_playerComponents.GetAccessoryItemSlot(i));
                _walletObject.transform.localPosition = new Vector3(0, 0, 0);
                _walletObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                _walletObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        _goldDisplay = _walletObject.GetComponentInChildren<CurrentGoldDisplay>();
        _goldDisplay.UpdateDisplay(goldAmount);
    }

    public void GrabWallet(GrabController grabController)
    {
        Vector3 walletPos;
        Vector3 walletRot;
        Vector3 walletScale;

        if (!grabController.GetHand().IsRightHand())
        {
            walletPos = new Vector3(-0.042f, 0.01379f, -0.06352501f);
            walletRot = new Vector3(-86.95f, 78.667f, 97.206f);
            walletScale = new Vector3(5, 5, 5);
        }

        else
        {
            walletPos = new Vector3(-0.03900001f, 0.035f, -0.154f);
            walletRot = new Vector3(-86.95f, 78.667f, 97.206f);
            walletScale = new Vector3(-14.28571f, 14.28571f, 14.28571f);
        }

        grabController.ParentGrabbable(_walletObject, walletPos, walletRot, walletScale);

        if (grabController.GetOppositeGrabController().currentObjectGrabbed == PlayerItemGrabbable.PlayerItem.wallet)
            grabController.GetOppositeGrabController().ReleaseGrip();
    }

    public void ResetWallet(GrabController grabController)
    {
        if (grabController.GetOppositeGrabController().currentObjectGrabbed != PlayerItemGrabbable.PlayerItem.wallet)
        {
            if (_walletObject != null)
                Destroy(_walletObject);

            SpawnNewWallet(PlayerStats.Instance.GetCurrentGold());
        }
    }

    public void UpdateGoldDisplay(int goldValue)
    {
        _goldDisplay.UpdateDisplay(goldValue);
    }    
}
