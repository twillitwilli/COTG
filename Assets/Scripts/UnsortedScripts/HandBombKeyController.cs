using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandBombKeyController : MonoBehaviour
{
    private VRPlayerController _player;
    private VRPlayerHand _leftHand, _rightHand;
    private VRPlayerHand _primaryHand, _offHand;
    private PlayerStats _playerStats;

    [SerializeField] private Material _bombCrystalMat, _keyCrystalMat;

    private GameObject _currentBombCrystal, _currentKeyCrystal;

    [SerializeField] private GameObject[] _ignitedBombCrystal;

    [HideInInspector] public bool holdingActiveBomb;

    private GameObject _newIgnitedBomb;

    public void ChangeCrystalHands()
    {
        if (_leftHand == null || _rightHand == null)
        {
            _player = LocalGameManager.instance.player;
            _playerStats = LocalGameManager.instance.GetPlayerStats();
            _leftHand = _player.GetPlayerComponents().GetHand(0);
            _rightHand = _player.GetPlayerComponents().GetHand(1);
        }

        if(_player.isLeftHanded)
        {
            _primaryHand = _leftHand;
            _offHand = _rightHand;
        }
        else
        {
            _primaryHand = _rightHand;
            _offHand = _leftHand;
        }

        _primaryHand.GetHandRenderer().materials[5] = _keyCrystalMat;
        _offHand.GetHandRenderer().materials[5] = _bombCrystalMat;

        SpawnBombCrystalOnHand();
        SpawnKeyCrystalOnHand();
    }

    // BOMB CRYSTAL SETTINGS

    public void SpawnBombCrystalOnHand()
    {
        if (_currentBombCrystal != null)
        {
            Destroy(_currentBombCrystal);
            _currentBombCrystal = null;
        }

        _currentBombCrystal = Instantiate(MasterManager.playerManager.bombCrystalForHand);

        Vector3 bombPos = new Vector3(0,0,0);
        Vector3 bombRot = new Vector3(0, 0, 0);
        Vector3 bombScale = new Vector3(0.4f, 0.4f, 0.4f);

        if (_primaryHand == _leftHand)
        {
            bombPos = new Vector3(-0.0242f, 0.001700003f, -0.004800003f);
            bombRot = new Vector3(166.872f, 81.849f, 90.011f);
        }
        else
        {
            bombPos = new Vector3(0.0227f, -1.007495e-09f, 0.002f);
            bombRot = new Vector3(-13.128f, 81.799f, 90);
        }
        
        _offHand.ParentObjectToFixedHandPosition(_currentBombCrystal, bombPos, bombRot, bombScale);
    }

    public void GrabBombCrystal(GrabController grabController)
    {
        if (grabController.GetHand() == _primaryHand && _playerStats.GetCurrentArcaneCrystals() > 0)
        {
            Destroy(_currentBombCrystal);
            _currentKeyCrystal = Instantiate(MasterManager.playerManager.bombCrystal);
            Vector3 bombPos = new Vector3(0.0227f, -8.784453e-10f, 0.002f);
            Vector3 bombRot = new Vector3(0, 79.953f, 90);
            Vector3 bombScale = new Vector3(0.4f, 0.4f, 0.4f);
            grabController.ParentGrabbable(_currentBombCrystal, bombPos, bombRot, bombScale);
            grabController.GetHand().GetHandAnimationState().SwitchHandState(HandAnimationState.HandState.holdingBombCrystal);
        }
    }

    public void DropBombCrystal()
    {
        Destroy(_currentBombCrystal);
        SpawnBombCrystalOnHand();
    }

    public void IgniteBomb(VRPlayerHand currentHand)
    {
        Destroy(_currentBombCrystal);
        _playerStats.AdjustArcaneCrystalAmount(-1);
        _newIgnitedBomb = Instantiate(MasterManager.playerMagicController.arcaneBombInHand);
        Vector3 bombPos = new Vector3(0, 0, 0);
        Vector3 bombRot = new Vector3(0, 0, 0);
        Vector3 bombScale = new Vector3(1, 1, 1);
        currentHand.GetGrabController().ParentGrabbable(_currentBombCrystal, bombPos, bombRot, bombScale);
        currentHand.GetHandAnimationState().SwitchHandState(HandAnimationState.HandState.holdingArcaneBomb);
        currentHand.GetGrabController().holdingIgnitedBomb = true;
    }

    public void ThrowBomb(VRPlayerHand currentHand)
    {
        Collider collider = _newIgnitedBomb.GetComponent<Collider>();
        collider.isTrigger = false;
        currentHand.ApplyHandVelocity(_newIgnitedBomb.GetComponent<Rigidbody>(), false);
        _newIgnitedBomb.transform.SetParent(null);
        BombTimer bombSettings = _newIgnitedBomb.GetComponent<BombTimer>();
        bombSettings.player = _player;
        bombSettings.startTimer = true;
        SceneManager.MoveGameObjectToScene(_newIgnitedBomb, SceneManager.GetActiveScene());
        _newIgnitedBomb = null;
        currentHand.GetHandAnimationState().SwitchHandState(HandAnimationState.HandState.idle);
    }

    // KEY CRYSTAL SETTINGS

    public void SpawnKeyCrystalOnHand()
    {
        if (_currentKeyCrystal != null)
        {
            Destroy(_currentKeyCrystal);
            _currentKeyCrystal = null;
        }

        _currentKeyCrystal = Instantiate(MasterManager.playerManager.keyCrystalForHand);

        Vector3 keyPos = new Vector3(0, 0, 0);
        Vector3 keyRot = new Vector3(0, 0, 0);
        Vector3 keyScale = new Vector3(0.4f, 0.4f, 0.4f);

        if (_primaryHand == _leftHand)
        {
            keyPos = new Vector3(0.0227f, 0, 0.002f);
            keyRot = new Vector3(0, 80, 90);
        }
        else
        {
            keyPos = new Vector3(-0.02419997f, 0.001700029f, -0.004799998f);
            keyRot = new Vector3(0, -96.415f, -90);
        }

        _primaryHand.ParentObjectToFixedHandPosition(_currentKeyCrystal, keyPos, keyRot, keyScale);
    }

    public void GrabKeyCrystal(GrabController grabController)
    {
        if (grabController.GetHand() == _offHand && _playerStats.GetCurrentKeys() > 0)
        {
            Destroy(_currentKeyCrystal);
            _currentKeyCrystal = Instantiate(MasterManager.playerManager.keyCrystal);
            Vector3 keyPos = new Vector3(0, 0, 0);
            Vector3 keyRot = new Vector3(0, 0, 0);
            Vector3 keyScale = new Vector3(1, 1, 1);
            grabController.ParentGrabbable(_currentKeyCrystal, keyPos, keyRot, keyScale);
            grabController.GetHand().GetHandAnimationState().SwitchHandState(HandAnimationState.HandState.holdingKeyCrystal);
        }
    }

    public void DropKeyCrystal()
    {
        Destroy(_currentKeyCrystal);
        SpawnKeyCrystalOnHand();
    }
}
