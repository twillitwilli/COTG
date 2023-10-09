using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QTArts.AbstractClasses;

public class CrystalController : MonoSingleton<CrystalController>
{
    PlayerComponents _playerComponents;
    
    VRHand 
        _bombHand, 
        _keyHand;

    [SerializeField]
    GameObject 
        _currentBombCrystal, 
        _currentKeyCrystal;

    [SerializeField]
    Material 
        _bombCrystalMat, 
        _keyCrystalMat, 
        _depletedMaterial;

    [SerializeField]
    GameObject[] _ignitedBombPrefabs;

    GameObject _currentIgnitedBomb;

    public override void Awake()
    {
        base.Awake();

        LocalGameManager.playerCreated += NewPlayerCreated;
    }

    public async void NewPlayerCreated(VRPlayer player)
    {
        _playerComponents = player.GetPlayerComponents();

        ChangeCrystalHands();
    }

    public void ChangeCrystalHands()
    {
        bool rightHandIsPrimary = _playerComponents.GetHand(1).IsPrimaryHand() ? true : false;
        _bombHand = rightHandIsPrimary ? _playerComponents.GetHand(0) : _playerComponents.GetHand(1);
        _keyHand = rightHandIsPrimary ? _playerComponents.GetHand(1) : _playerComponents.GetHand(0);

        SpawnBombCrystalOnHand();
        SpawnKeyCrystalOnHand();
    }

    // BOMB CRYSTAL SETTINGS

    void SpawnBombCrystalOnHand()
    {
        _currentBombCrystal.SetActive(true);
        _currentBombCrystal.GetComponent<BoxCollider>().enabled = true;

        Vector3 bombPos;
        Vector3 bombRot;

        if (!_bombHand.IsRightHand())
        {
            bombPos = new Vector3(-0.0242f, 0.001700003f, -0.004800003f);
            bombRot = new Vector3(166.872f, 81.849f, 90.011f);
        }
        else
        {
            bombPos = new Vector3(0.0227f, -1.007495e-09f, 0.002f);
            bombRot = new Vector3(-13.128f, 81.799f, 90);
        }

        Vector3 bombScale = new Vector3(0.4f, 0.4f, 0.4f);

        _bombHand.ParentObjectToFixedHandPosition(_currentBombCrystal, bombPos, bombRot, bombScale);
    }

    public void GrabBombCrystal(GrabController grabController)
    {
        if (grabController.GetHand().IsPrimaryHand() && PlayerStats.Instance.data.currentArcaneCrystals > 0)
        {
            Vector3 bombPos = new Vector3(0.0227f, -8.784453e-10f, 0.002f);
            Vector3 bombRot = new Vector3(0, 79.953f, 90);
            Vector3 bombScale = new Vector3(0.4f, 0.4f, 0.4f);

            grabController.ParentGrabbable(_currentBombCrystal, bombPos, bombRot, bombScale);

            _currentBombCrystal.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void DropBombCrystal()
    {
        if (_bombHand.GetGrabController().currentObjectGrabbed == ItemPoolManager.GrabbableItem.bomb)
            SpawnBombCrystalOnHand();
    }

    public void IgniteBomb(GrabController grabController)
    {
        _currentBombCrystal.SetActive(false);
        PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.arcaneCrystals, -1);

        _currentIgnitedBomb = Instantiate(_ignitedBombPrefabs[MagicController.Instance.magicIdx]);

        Vector3 bombPos = new Vector3(0, 0, 0);
        Vector3 bombRot = new Vector3(0, 0, 0);
        Vector3 bombScale = new Vector3(1, 1, 1);

        grabController.ParentGrabbable(_currentIgnitedBomb, bombPos, bombRot, bombScale);
    }

    public void ThrowBomb(VRHand currentHand)
    {
        Collider collider = _currentIgnitedBomb.GetComponent<Collider>();
        collider.isTrigger = false;

        currentHand.ApplyHandVelocity(_currentIgnitedBomb.GetComponent<Rigidbody>(), false);
        _currentIgnitedBomb.transform.SetParent(null);

        BombTimer bombSettings = _currentIgnitedBomb.GetComponent<BombTimer>();
        bombSettings.StartBombTimer();

        SceneManager.MoveGameObjectToScene(_currentIgnitedBomb, SceneManager.GetActiveScene());
        _currentIgnitedBomb = null;
        SpawnBombCrystalOnHand();
    }

    // KEY CRYSTAL SETTINGS

    public void SpawnKeyCrystalOnHand()
    {
        _currentKeyCrystal.SetActive(true);
        _currentKeyCrystal.GetComponent<BoxCollider>().enabled = true;

        Vector3 keyPos;
        Vector3 keyRot;

        if (!_keyHand.IsRightHand())
        {
            keyPos = new Vector3(0.0227f, 0, 0.002f);
            keyRot = new Vector3(0, 80, 90);
        }

        else
        {
            keyPos = new Vector3(-0.02419997f, 0.001700029f, -0.004799998f);
            keyRot = new Vector3(0, -96.415f, -90);
        }

        Vector3 keyScale = new Vector3(0.4f, 0.4f, 0.4f);

        _keyHand.ParentObjectToFixedHandPosition(_currentKeyCrystal, keyPos, keyRot, keyScale);
    }

    public void GrabKeyCrystal(GrabController grabController)
    {
        if (!grabController.GetHand().IsPrimaryHand() && PlayerStats.Instance.data.currentKeys > 0)
        {
            Vector3 keyPos = new Vector3(0, 0, 0);
            Vector3 keyRot = new Vector3(0, 0, 0);
            Vector3 keyScale = new Vector3(1, 1, 1);
            grabController.ParentGrabbable(_currentKeyCrystal, keyPos, keyRot, keyScale);
        }
    }
}
