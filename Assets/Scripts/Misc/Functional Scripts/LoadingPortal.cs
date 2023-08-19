using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoadingPortal : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private GameTimer _gameTimer;
    private PlayerPrefsSaveData _playerPrefSaveData;
    private PlayerTotalStats _playerTotalStats;

    private EnemyTrackerController _enemyTrackerController;

    private VRPlayerController _player;
    private EyeManager _eyeManager;
    
    public enum GameType { Normal, Hard, Tutorial, TestingArea, ExitGame, ToBossArena, ToNextFloor, LoadSavedDungeon, moveToSpawn, titleScreen, saveDungeon, beginnerDungeon }
    public GameType GameMode;

    public bool movePlayerInScene, portalDisabled;
    public int whichSpawnLocation;

    public List<GameObject> enableObjs, disableObjs;

    private void Awake()
    {
        _gameManager = LocalGameManager.instance;
        _gameTimer = _gameManager.GetGameTimer();
        _playerPrefSaveData = _gameManager.GetPlayerPrefsSaveData();
        _playerTotalStats = _gameManager.GetTotalStats();

        _enemyTrackerController = _gameManager.GetEnemyTrackerController();

        if (_gameManager.player != null)
        {
            _player = _gameManager.player;
            _eyeManager = _player.GetPlayerComponents().GetEyeManager();
        }
        else LocalGameManager.playerCreated += NewPlayerCreated;
    }

    private void Start()
    {
        switch(GameMode)
        {
            //temp game block
            case GameType.ToNextFloor:
                if (LocalGameManager.instance.currentLevel >= 2) { gameObject.SetActive(false); }
                break;
        }
    }

    public void NewPlayerCreated(VRPlayerController player)
    {
        _player = player;
        _eyeManager = _player.GetPlayerComponents().GetEyeManager();
    }

    private async void OnTriggerEnter(Collider other)
    {
        VRPlayerController player;
        if (other.TryGetComponent<VRPlayerController>(out player)) 
        {
            if (!movePlayerInScene)
            {
                _eyeManager.EyesClosing();

                if (GameMode == GameType.ExitGame) { Application.Quit(); }

                else 
                {
                    await Task.Delay(1000);

                    PortalSettings();
                }
            }
            else PortalSettings();
        }
    }

    public void PortalSettings()
    {
        switch (GameMode)
        {
            case GameType.beginnerDungeon:
                _gameManager.currentGameMode = LocalGameManager.GameMode.tutorial;
                NewDungeonSettings();
                break;

            case GameType.Normal:
                _gameManager.currentGameMode = LocalGameManager.GameMode.normal;
                NewDungeonSettings();
                break;

            case GameType.Hard:
                _gameManager.currentGameMode = LocalGameManager.GameMode.master;
                NewDungeonSettings();
                break;

            case GameType.TestingArea:
                _gameManager.Loading(LocalGameManager.SceneSelection.testArea);
                break;

            case GameType.Tutorial:
                _gameManager.Loading(LocalGameManager.SceneSelection.tutorial);
                break;

            case GameType.ToBossArena:
                if(DungeonBuildParent.instance) Destroy(DungeonBuildParent.instance.gameObject);
                SpawnBossArena();
                _gameManager.AreaLoaded();
                break;

            case GameType.ToNextFloor:
                _gameManager.currentLevel++;
                _gameManager.dungeonBuildCompleted = false;
                if (CoopManager.instance != null) 
                { 
                    if (LocalGameManager.instance.isHost) 
                    {
                        CoopManager.instance.coopDungeonBuild.ClearDungeonRoomList();
                        CoopManager.instance.coopDungeonBuild.dungeonCompleted = false;
                        CoopManager.instance.MoveOtherPlayer();
                    }
                    else 
                    {
                        CoopManager.instance.coopDungeonBuild.ClearDungeonRoomList();
                        CoopManager.instance.coopDungeonBuild.dungeonCompleted = false;
                        _gameManager.loadDungeon = true;
                        _gameManager.dungeonBuildCompleted = false;
                        CoopManager.instance.coopDungeonBuild.CheckDungeonBuild(); 
                    }
                }
                _gameManager.Loading(LocalGameManager.SceneSelection.dungeon);
                break;

            case GameType.moveToSpawn:
                EnableObjects();
                _gameManager.MovePlayer(whichSpawnLocation);
                DisableObjects();
                break;

            case GameType.titleScreen:
                _gameManager.PlayerBackToTitleScreen();
                break;

            case GameType.saveDungeon:
                SavePlayerDungeonStats saveSystem = GetComponent<SavePlayerDungeonStats>();
                saveSystem.SaveDungeon();
                break;

            case GameType.LoadSavedDungeon:
                SavePlayerDungeonStats loadSystem = GetComponent<SavePlayerDungeonStats>();
                loadSystem.LoadDungeon();

                _gameManager.Loading(LocalGameManager.SceneSelection.dungeon);
                break;
        }
    }

    public void NewDungeonSettings()
    {
        _gameManager.dungeonBuildCompleted = false;
        _gameManager.currentLevel = 1;
        
        if (CoopManager.instance != null)
        {
            if (_gameManager.isHost)
            {
                CoopPortalSettings();
                _gameTimer.BeginTimer();
            }
            else CoopManager.instance.coopDungeonBuild.CheckDungeonBuild();
        }


        BasePlayerSettings(_player);
        _playerTotalStats.AdjustStats(PlayerTotalStats.StatType.totalRuns);

        _gameManager.Loading(LocalGameManager.SceneSelection.dungeon);
    }

    private void CoopPortalSettings()
    {
        CoopManager.instance.coopDungeonBuild.ClearDungeonRoomList();
        CoopManager.instance.coopDungeonBuild.dungeonCompleted = false;
        CoopManager.instance.isHardMode = LocalGameManager.instance.currentGameMode == LocalGameManager.GameMode.master ? true : false;
        CoopManager.instance.portalActive = true;
        CoopManager.instance.PortalStatus();
    }

    private void BasePlayerSettings(VRPlayerController player)
    {
        player.GetPlayerComponents().resetPlayer.ResetPlayer(false);
    }

    public void SpawnBossArena()
    {
        _enemyTrackerController.enemyNavMesh.RemoveData();
        _enemyTrackerController.enemyNavMesh.BuildNavMesh();
        _gameManager.spawnedBossArena = Instantiate(MasterManager.bossArena.bossArenas[Random.Range(0, MasterManager.bossArena.bossArenas.Length)]);
        _gameManager.spawnedBossArena.transform.localScale = new Vector3(30, 30, 30);
    }

    public void EnableObjects()
    {
        if (enableObjs.Count > 0) foreach (GameObject obj in enableObjs) { obj.SetActive(true); }
    }

    public void DisableObjects()
    {
        if (disableObjs.Count > 0) { foreach (GameObject obj in disableObjs) { obj.SetActive(false); } }
    }
}
