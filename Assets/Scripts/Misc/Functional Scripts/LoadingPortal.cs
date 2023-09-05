using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoadingPortal : MonoBehaviour
{
    private VRPlayerController _player;

    public enum PortalTo 
    { 
        NormalDungeon, 
        HardDungeon, 
        Tutorial, 
        TestingArea, 
        ExitGame, 
        ToBossArena, 
        ToNextFloor, 
        LoadSavedDungeon, 
        MoveToSpawn, 
        TitleScreen, 
        SaveDungeon, 
        BeginnerDungeon 
    }

    public PortalTo portalLocation;

    public bool movePlayerInScene, portalDisabled;
    public int whichSpawnLocation;

    public List<GameObject> enableObjs, disableObjs;

    private void Awake()
    {
        LocalGameManager.playerCreated += NewPlayerCreated;
    }

    private void Start()
    {
        switch(portalLocation)
        {
            //temp game block
            case PortalTo.ToNextFloor:
                if (LocalGameManager.Instance.currentLevel >= 2)
                    gameObject.SetActive(false);
                break;
        }
    }

    public void NewPlayerCreated(VRPlayerController player)
    {
        _player = player;
    }

    private async void OnTriggerEnter(Collider other)
    {
        VRPlayerController player;

        if (other.TryGetComponent<VRPlayerController>(out player)) 
        {
            if (!movePlayerInScene)
            {
                LocalGameManager.Instance.CloseEyes();

                if (portalLocation == PortalTo.ExitGame)
                    Application.Quit();

                else 
                {
                    await Task.Delay(1000);

                    PortalSettings();
                }
            }
            else
                PortalSettings();
        }
    }

    public void PortalSettings()
    {
        switch (portalLocation)
        {
            case PortalTo.BeginnerDungeon:
                LocalGameManager.Instance.currentGameMode = LocalGameManager.GameMode.tutorial;
                NewDungeonSettings();
                break;

            case PortalTo.NormalDungeon:
                LocalGameManager.Instance.currentGameMode = LocalGameManager.GameMode.normal;
                NewDungeonSettings();
                break;

            case PortalTo.HardDungeon:
                LocalGameManager.Instance.currentGameMode = LocalGameManager.GameMode.master;
                NewDungeonSettings();
                break;

            case PortalTo.TestingArea:
                LocalGameManager.Instance.Loading(LocalGameManager.SceneSelection.testArea);
                break;

            case PortalTo.Tutorial:
                LocalGameManager.Instance.Loading(LocalGameManager.SceneSelection.tutorial);
                break;

            case PortalTo.ToBossArena:
                if(DungeonBuildParent.Instance)
                    Destroy(DungeonBuildParent.Instance.gameObject);

                SpawnBossArena();
                LocalGameManager.Instance.AreaLoaded();
                break;

            case PortalTo.ToNextFloor:
                LocalGameManager.Instance.currentLevel++;
                LocalGameManager.Instance.dungeonBuildCompleted = false;

                if (MultiplayerManager.Instance.coop) 
                { 
                    if (LocalGameManager.Instance.isHost) 
                    {
                        MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.ClearDungeonRoomList();
                        MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.dungeonCompleted = false;
                        MultiplayerManager.Instance.GetCoopManager().MoveOtherPlayer();
                    }

                    else 
                    {
                        MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.ClearDungeonRoomList();
                        MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.dungeonCompleted = false;

                        LocalGameManager.Instance.loadDungeon = true;
                        LocalGameManager.Instance.dungeonBuildCompleted = false;

                        MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.CheckDungeonBuild(); 
                    }
                }
                LocalGameManager.Instance.Loading(LocalGameManager.SceneSelection.dungeon);
                break;

            case PortalTo.MoveToSpawn:
                EnableObjects();
                LocalGameManager.Instance.MovePlayer(whichSpawnLocation);
                DisableObjects();
                break;

            case PortalTo.TitleScreen:
                LocalGameManager.Instance.PlayerBackToTitleScreen();
                break;

            case PortalTo.SaveDungeon:
                SavePlayerDungeonStats saveSystem = GetComponent<SavePlayerDungeonStats>();
                saveSystem.SaveDungeon();
                break;

            case PortalTo.LoadSavedDungeon:
                SavePlayerDungeonStats loadSystem = GetComponent<SavePlayerDungeonStats>();
                loadSystem.LoadDungeon();

                LocalGameManager.Instance.Loading(LocalGameManager.SceneSelection.dungeon);
                break;
        }
    }

    public void NewDungeonSettings()
    {
        LocalGameManager.Instance.dungeonBuildCompleted = false;
        LocalGameManager.Instance.currentLevel = 1;
        
        if (MultiplayerManager.Instance.coop)
        {
            if (LocalGameManager.Instance.isHost)
                CoopPortalSettings();

            else
                MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.CheckDungeonBuild();
        }

        BasePlayerSettings(_player);
        PlayerTotalStats.Instance.AdjustStats(PlayerTotalStats.StatType.totalRuns);

        LocalGameManager.Instance.Loading(LocalGameManager.SceneSelection.dungeon);
    }

    private void CoopPortalSettings()
    {
        MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.ClearDungeonRoomList();
        MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.dungeonCompleted = false;
        MultiplayerManager.Instance.GetCoopManager().isHardMode = LocalGameManager.Instance.currentGameMode == LocalGameManager.GameMode.master ? true : false;
        MultiplayerManager.Instance.GetCoopManager().portalActive = true;
        MultiplayerManager.Instance.GetCoopManager().PortalStatus();
    }

    private void BasePlayerSettings(VRPlayerController player)
    {
        player.GetPlayerComponents().resetPlayer.ResetPlayer(false);
    }

    public void SpawnBossArena()
    {
        EnemyTrackerController.Instance.enemyNavMesh.RemoveData();
        EnemyTrackerController.Instance.enemyNavMesh.BuildNavMesh();

        LocalGameManager.Instance.spawnedBossArena = Instantiate(MasterManager.bossArena.bossArenas[Random.Range(0, MasterManager.bossArena.bossArenas.Length)]);
        LocalGameManager.Instance.spawnedBossArena.transform.localScale = new Vector3(30, 30, 30);
    }

    public void EnableObjects()
    {
        if (enableObjs.Count > 0)
        {
            foreach (GameObject obj in enableObjs)
            {
                obj.SetActive(true);
            }
        }
    }

    public void DisableObjects()
    {
        if (disableObjs.Count > 0) 
        { 
            foreach (GameObject obj in disableObjs) 
            { 
                obj.SetActive(false); 
            } 
        }
    }
}
