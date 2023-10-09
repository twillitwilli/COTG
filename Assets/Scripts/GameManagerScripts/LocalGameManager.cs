using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using QTArts.AbstractClasses;

public class LocalGameManager : MonoSingleton<LocalGameManager>
{
    public enum GameMode 
    { 
        inLobby = 0, 
        tutorial = 1, 
        normal = 2, 
        master = 3 
    }
    public GameMode currentGameMode;

    public enum SceneSelection
    {
        testArea,
        tutorial,
        titleScene,
        dungeon
    }

    public enum SpawnLocation
    {
        spawnPoint,
        loadingAreaSpawn,
        moveableSpawnPoint
    }

    [SerializeField] 
    GameObject playerPrefab;

    public VRPlayer player { get; private set; }

    public delegate void PlayerCreated(VRPlayer newPlayer);
    public static event PlayerCreated playerCreated;

    [SerializeField] 
    bool 
        hardResetPlayerData, 
        devMode, 
        demoMode;

    [SerializeField] 
    Camera _mapCamera;

    EyeManager _eyeManager;

    [HideInInspector] 
    public bool 
        hasCalibrated, 
        isHost, 
        dungeonBuildCompleted, 
        loadDungeon;

    [HideInInspector] 
    public List<Vector2Int> spawnedScrolls = new List<Vector2Int>();

    [SerializeField]
    public Transform moveableSpawnPoint;

    [HideInInspector] 
    public GameObject 
        loadingBox, 
        spawnedBossArena;

    public int dungeonType { get; set; }
    public int currentLevel { get; set; }
    public int saveFile { get; set; }


    void Start()
    {
        if (hardResetPlayerData && PlayerPrefsSaveData.Instance.CheckIfSaveFileExists("ReturningPlayer"))
            PlayerPrefs.SetInt("ReturningPlayer", (false ? 1 : 0));

        if (player == null)
            PlayerSpawner();

        MovePlayer(0);

        currentGameMode = GameMode.inLobby;

        if (devMode)
            Debug.Log("Dev Mode Active");

        if (demoMode)
            Debug.Log("Demo Mode Active");
    }

    public void NewLoadingBoxSettings(GameObject newLoadingBox)
    {
        newLoadingBox.transform.SetParent(null);
        DontDestroyOnLoad(newLoadingBox);
        newLoadingBox.transform.position = new Vector3(1000, 0, 1000);
    }

    public void PlayerSpawner()
    {
        GameObject newPlayer = Instantiate(playerPrefab);
        player = newPlayer.GetComponent<VRPlayer>();
        newPlayer.transform.SetParent(null);
        DontDestroyOnLoad(newPlayer);

        _eyeManager = player.GetPlayerComponents().GetEyeManager();

        playerCreated(player);
    }

    public void PlayerBackToTitleScreen()
    {
        spawnedScrolls.Clear();
        Loading(SceneSelection.titleScene);
        AreaLoaded();
    }

    public async void Loading(SceneSelection whichScene)
    {
        Debug.Log("LoadingDungeon");

        Loader.Load(Loader.Scene.LoadingScreen);

        MovePlayer(SpawnLocation.loadingAreaSpawn);

        await Task.Delay(1000);

        OpenEyes();

        ChangeScene(whichScene);
    }

    public void CloseEyes()
    {
        _eyeManager.EyesClosing();
    }

    void OpenEyes()
    {
        _eyeManager.EyesOpening();
    }

    public void AreaLoaded()
    {
        EnemyTrackerController.Instance.enemyNavMesh.RemoveData();
    }

    public void MovePlayer(SpawnLocation whichSpawnLocation)
    {
        switch (whichSpawnLocation)
        {
            case SpawnLocation.spawnPoint:
                player.transform.position = new Vector3(0, 0, 0);
                player.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;

            case SpawnLocation.loadingAreaSpawn:
                player.transform.position = new Vector3(0, 0, 0);
                player.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;

            case SpawnLocation.moveableSpawnPoint:
                player.transform.position = moveableSpawnPoint.transform.position;
                player.transform.rotation = moveableSpawnPoint.transform.rotation;
                break;
        }
    }

    public void ResetPlayer()
    {
        Destroy(player.gameObject);
        PlayerSpawner();
    }

    public void PlayerDeadReset()
    {
        EnemyTrackerController.Instance.Reset();
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

        AudioController.Instance.ChangeMusic(AudioController.MusicTracks.Forest);
    }

    public void CheckDungeonType()
    {
        if (currentLevel == 0)
            dungeonType = 0;

        else if (currentLevel > 0 && currentLevel < 4)
            dungeonType = 1;

        else if (currentLevel > 3 && currentLevel < 7)
            dungeonType = 2;
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

    public void SpawnSpecificItem(ItemPoolManager.DroppableItem itemType, Transform spawnLocation)
    {
        GameObject spawnedDrop = Instantiate(MasterManager.itemPool.droppableItems.dropTemplate, spawnLocation);
        spawnedDrop.transform.SetParent(null);
        spawnedDrop.transform.localEulerAngles = new Vector3(-90, 0, 0);
        spawnedDrop.transform.localScale = new Vector3(1, 1, 1);
        DropTemplateController dropSettings = spawnedDrop.GetComponent<DropTemplateController>();
        dropSettings.UseSpecificItemDrop(itemType);
    }

    public Camera GetMapCamera() { return _mapCamera; }
    public bool IsDemo() { return demoMode; }
    public bool IsDevMode() { return devMode; }

    public void ActivateDevMode()
    {
        devMode = true;

        Debug.Log("Player Activated Dev Mode");
        player.GetPlayerComponents().onScreenText.PrintText("Dev Mode Active", true);
    }
}
