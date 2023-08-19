using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameManager : MonoBehaviour
{
    public static LocalGameManager instance;

    public enum GameMode { inLobby = 0, tutorial = 1, normal = 2, master = 3 }
    public GameMode currentGameMode;

    [SerializeField] private GameObject playerPrefab;

    public VRPlayerController player { get; private set; }

    public delegate void PlayerCreated(VRPlayerController newPlayer);
    public static event PlayerCreated playerCreated;

    public enum SceneSelection { testArea, tutorial, titleScene, dungeon }

    [SerializeField] private bool hardResetPlayerData, devMode, demoMode;
    [SerializeField] private Camera _mapCamera;
    [SerializeField] private Transform[] _spawnLocations;

    [SerializeField] private ControllerType _controllerType;
    [SerializeField] private OptionsMenu _optionsMenu;
    [SerializeField] private AudioController _audioController;
    [SerializeField] private VisualSettings _visualSettings;
    [SerializeField] private PostProcessingController _postProcessingController;
    [SerializeField] private PlayerPrefsSaveData _playerPrefsSaveData;
    [SerializeField] private GameTimer _gameTimer;

    [SerializeField] private PlayerCardContnroller _playerCardController;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private PlayerTotalStats _playerTotalStats;
    [SerializeField] private MagicController _magicController;
    [SerializeField] private GearController _gearController;
    [SerializeField] private PlayerPotionController _potionController;
    [SerializeField] private PlayerCurse _curseController;

    [SerializeField] private EnemyTrackerController _enemyTrackerController;

    [SerializeField] private FollowerPetController _followerPetController;

    [SerializeField] private NetworkManager _networkManager;
    [SerializeField] private ChatManager _chatManager;

    private EyeManager _eyeManager;

    [HideInInspector] public bool hasCalibrated, isHost, dungeonBuildCompleted, loadDungeon;

    [HideInInspector] public List<Vector2Int> spawnedScrolls = new List<Vector2Int>();

    [HideInInspector] public GameObject loadingBox, spawnedBossArena;

    [HideInInspector] public int dungeonType, currentLevel;

    private void Awake()
    {
        if (hardResetPlayerData && _playerPrefsSaveData.CheckIfSaveFileExists("ReturningPlayer"))
        {
            PlayerPrefs.SetInt("ReturningPlayer", (false ? 1 : 0));
        }
    }

    private void Start()
    {
        if (!instance) { instance = this; }
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        if (player == null) { PlayerSpawner(); }
        MovePlayer(0);

        currentGameMode = GameMode.inLobby;

        if (devMode) { Debug.Log("Dev Mode Active"); }
        if (demoMode) { Debug.Log("Demo Mode Active"); }
    }

    public void NewLoadingBoxSettings(GameObject newLoadingBox)
    {
        newLoadingBox.transform.SetParent(null);
        DontDestroyOnLoad(newLoadingBox);
        newLoadingBox.transform.position = new Vector3(1000, 0, 1000);
    }

    public void PlayerSpawner()
    {
        GameObject newPlayer = Instantiate(playerPrefab, _spawnLocations[0]);
        player = newPlayer.GetComponent<VRPlayerController>();
        newPlayer.transform.SetParent(null);
        DontDestroyOnLoad(newPlayer);

        playerCreated(player);
    }

    public void PlayerBackToTitleScreen()
    {
        spawnedScrolls.Clear();
        Loading(SceneSelection.titleScene);
        AreaLoaded();
    }

    public void Loading(SceneSelection whichScene)
    {
        Debug.Log("LoadingDungeon");
        Loader.Load(Loader.Scene.LoadingScreen);
        MovePlayer(1);
        Invoke("OpenEyes", 1);
        ChangeScene(whichScene);
    }

    public void CloseEyes()
    {
        _eyeManager.EyesClosing();
    }

    private void OpenEyes()
    {
        _eyeManager.EyesOpening();
    }

    public void AreaLoaded()
    {
        _enemyTrackerController.enemyNavMesh.RemoveData();
    }

    public void MovePlayer(int spawnLocation)
    {
        player.transform.position = _spawnLocations[spawnLocation].position;
        player.transform.rotation = _spawnLocations[spawnLocation].rotation;
    }

    public void ResetPlayer()
    {
        Destroy(player.gameObject);
        PlayerSpawner();
    }

    public void PlayerDeadReset()
    {
        _enemyTrackerController.Reset();
    }

    public void ChangeScene(SceneSelection whichScene)
    {
        switch (whichScene)
        {
            case SceneSelection.tutorial:
                Loader.Load(Loader.Scene.CotGTutorial);
                currentGameMode = GameMode.tutorial;
                break;

            case SceneSelection.titleScene:
                Loader.Load(Loader.Scene.CotGTitleScreen);
                currentGameMode = GameMode.inLobby;
                break;

            case SceneSelection.dungeon:
                CheckDungeonType();
                Loader.Load(Loader.Scene.DungeonGeneration_V3_1);
                break;

            case SceneSelection.testArea:
                Loader.Load(Loader.Scene.VRPlayer_Test_Scene);
                currentGameMode = GameMode.inLobby;
                break;
        }

        _audioController.ChangeMusic(AudioController.MusicTracks.Forest);
    }

    public void CheckDungeonType()
    {
        if (currentLevel == 0) { dungeonType = 0; }
        else if (currentLevel > 0 && currentLevel < 4) { dungeonType = 1; }
        else if (currentLevel > 3 && currentLevel < 7) { dungeonType = 2; }
    }

    public void SpawnRandomDrop(Transform spawnLocation)
    {
        int spawnChest = Random.Range(0, 100);
        if (spawnChest < 90)
        {
            GameObject spawnedDrop = Instantiate(MasterManager.itemPool.droppableItems.dropTemplate, spawnLocation);
            spawnedDrop.transform.SetParent(null);
            spawnedDrop.transform.localEulerAngles = new Vector3(-90, 0, 0);
            spawnedDrop.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            Debug.Log("Chest Count = " + MasterManager.itemPool.droppableItems.chests.Count);
            int whichChest = Random.Range(0, MasterManager.itemPool.droppableItems.chests.Count + 1);
            GameObject newChest = Instantiate(MasterManager.itemPool.droppableItems.chests[whichChest], spawnLocation);
            newChest.transform.SetParent(null);
            newChest.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
    }

    public void SpawnSpecificItem(ItemDropSelection.ItemType itemType, Transform spawnLocation)
    {
        GameObject spawnedDrop = Instantiate(MasterManager.itemPool.droppableItems.dropTemplate, spawnLocation);
        spawnedDrop.transform.SetParent(null);
        spawnedDrop.transform.localEulerAngles = new Vector3(-90, 0, 0);
        spawnedDrop.transform.localScale = new Vector3(1, 1, 1);
        DropTemplateController dropSettings = spawnedDrop.GetComponent<DropTemplateController>();
        dropSettings.UseSpecificItemDrop(itemType);
    }

    public Camera GetMapCamera() { return _mapCamera; }
    public ControllerType GetControllerType() { return _controllerType; }
    public OptionsMenu GetOptionsMenu() { return _optionsMenu; }
    public AudioController GetAudioController() { return _audioController; }
    public VisualSettings GetVisualSettings() { return _visualSettings; }
    public PostProcessingController GetPostProcessingController() { return _postProcessingController; }
    public PlayerPrefsSaveData GetPlayerPrefsSaveData() { return _playerPrefsSaveData; }
    public GameTimer GetGameTimer() { return _gameTimer; }

    public PlayerCardContnroller GetPlayerCardController() { return _playerCardController; }
    public PlayerStats GetPlayerStats() { return _playerStats; }
    public PlayerTotalStats GetTotalStats() { return _playerTotalStats; }
    public MagicController GetMagicController() { return _magicController; }
    public GearController GetGearController() { return _gearController; }
    public PlayerPotionController GetPotionController() { return _potionController; }
    public PlayerCurse GetCurseController() { return _curseController; }


    public EnemyTrackerController GetEnemyTrackerController() { return _enemyTrackerController; }


    public FollowerPetController GetPetController() { return _followerPetController; }


    public NetworkManager GetNetworkManager() { return _networkManager; }
    public ChatManager GetChatManager() { return _chatManager; }


    public Transform[] GetSpawnLocations() { return _spawnLocations; }
    public bool IsDemo() { return demoMode; }
    public bool IsDevMode() { return devMode; }

    public void ActivateDevMode()
    {
        devMode = true;
        Debug.Log("Player Activated Dev Mode");
        player.GetPlayerComponents().onScreenText.PrintText("Dev Mode Active", true);
    }
}
