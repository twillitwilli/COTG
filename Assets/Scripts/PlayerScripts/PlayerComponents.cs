using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    private PlayerPrefsSaveData _playerPrefSaveData;

    [Header("--Player Origins--")]
    [SerializeField] private GameObject[] _originPoints;
    public GameObject[] GetAllOriginPoints() { return _originPoints; }
    public GameObject GetOriginPoint(int whichPoint) { return _originPoints[whichPoint]; }


    [Header("--Base Player Components--")]
    [SerializeField] private ControllerInputManager _controllerInput;
    public ControllerInputManager GetControllerInputManager() { return _controllerInput; }

    [SerializeField] private PlayerGroundChecker _groundCheckController;
    public PlayerGroundChecker GetGroundCheckController() { return _groundCheckController; }

    [SerializeField] private GameObject _head;
    public GameObject GetHead() { return _head; }

    [SerializeField] private VRPlayerHand[] _hand;
    public VRPlayerHand[] GetBothHands() { return _hand; }
    public VRPlayerHand GetHand(int whichHand) { return _hand[whichHand]; }

    [SerializeField] private VRSockets[] _handSocket;
    public VRSockets[] GetAllSockets() { return _handSocket; }
    public VRSockets GetHandSocket(int whichSocket) { return _handSocket[whichSocket]; }

    [SerializeField] private EyeManager _eyeManager;
    public EyeManager GetEyeManager() { return _eyeManager; }

    [SerializeField] private AudioSource _musicPlayer;
    public AudioSource GetMusicPlayer() { return _musicPlayer; }

    public PlayerBelt belt;
    public BackAttachments backAttachments;
    public Transform playerTarget;


    [Header("--Visual Effects--")]
    public OnScreenText onScreenText;
    public VignetteAdjustment vignette;
    public PlayerHitEffect hitEffect;
    public PlayerHealthDisplay[] healthDisplay;
    public PlayerBombKeyDisplay[] bombKeyDisplay;
    public DashEffect dashEffect;
    public GameObject visualDashReadyEffect;
    public PostProcessingComponents postProcessingComponents;

    [Header("--Other Player Stuff--")]
    public GetItem getItemRaycast;
    public MapWalletSpawner mapWalletSpawner;
    public HandBombKeyController bombKeyController;
    public PlayerSceneLocation sceneLocation;
    public Transform petSpawnLocation;
    public CurrentMinion minionSpawnLocation;
    public DungeonGear dungeonGear;

    [Header("--Reset Player--")]
    public ResetPlayerToDefault resetPlayer;

    private void Start()
    {
        _playerPrefSaveData = LocalGameManager.Instance.GetPlayerPrefsSaveData();

        _controllerInput.gameObject.SetActive(true);
    }

    public void SavePlayerOrigins()
    {
        for (int i = 0; i < _originPoints.Length; i++)
        {
            PlayerPrefs.SetFloat("originPosX" + i, _originPoints[i].transform.localPosition.x);
            PlayerPrefs.SetFloat("originPosY" + i, _originPoints[i].transform.localPosition.y);
            PlayerPrefs.SetFloat("originPosZ" + i, _originPoints[i].transform.localPosition.z);

            PlayerPrefs.SetFloat("originRotX" + i, _originPoints[i].transform.localEulerAngles.x);
            PlayerPrefs.SetFloat("originRotY" + i, _originPoints[i].transform.localEulerAngles.y);
            PlayerPrefs.SetFloat("originRotZ" + i, _originPoints[i].transform.localEulerAngles.z);
        }
    }

    public void LoadPlayerOrigins()
    {
        _playerPrefSaveData = LocalGameManager.Instance.GetPlayerPrefsSaveData();

        if (_playerPrefSaveData.CheckIfSaveFileExists("originPosX"))
        {
            for (int i = 0; i < _originPoints.Length; i++)
            {
                Vector3 newPos = new Vector3(PlayerPrefs.GetFloat("originPosX" + i), PlayerPrefs.GetFloat("originPosY" + i), PlayerPrefs.GetFloat("originPosZ" + i));
                Vector3 newRot = new Vector3(PlayerPrefs.GetFloat("originRotX" + i), PlayerPrefs.GetFloat("originRotY" + i), PlayerPrefs.GetFloat("originRotZ" + i));
                _originPoints[i].transform.localPosition = newPos;
                _originPoints[i].transform.localEulerAngles = newRot;

                if (Sorcerer.instance != null) { Sorcerer.instance.GetSpellCasting().CalibrateSettings(); }
            }
        }
    }
}
