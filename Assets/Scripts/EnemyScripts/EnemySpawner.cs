using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unityEngine = UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private VRPlayerController _player;
    private EnemyTrackerController _enemyTrackerController;

    private DungeonBuildParent _dungeonBuildParent;
    private EnemySpawnerTracker _enemySpawnerTracker;

    public RoomModel room;
    public GameObject spawnTriggers, mapUnexplored, mapExplored;
    public bool smallRoom;
    public Transform puzzleSpawnLocation;
    public EnemySpawner[] connectedSpawners;
    public List<GameObject> doorLocks; 
    public List<Transform> spawnLocations;
    [HideInInspector] public int connectedSpawnersDone;

    private List<Transform> tempSpawnLocations = new List<Transform>();
    private bool spawnedEnemies;
    private EnemySpawner masterSpawner;

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
        _player = _gameManager.player;
        _enemyTrackerController = _gameManager.GetEnemyTrackerController(); 

        _dungeonBuildParent = DungeonBuildParent.instance;
        _enemySpawnerTracker = _dungeonBuildParent.GetEnemySpawnerTracker();
        if (_dungeonBuildParent != null) { _enemySpawnerTracker.enemySpawners.Add(this); }
    }

    public void CheckSpawnLocations()
    {
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            if (SpawnClear(i)) tempSpawnLocations.Add(spawnLocations[i]);
        }
        spawnLocations.Clear();
        spawnLocations = tempSpawnLocations;
    }

    public bool SpawnClear(int whichSpawn)
    {
        Collider[] objects = Physics.OverlapSphere(spawnLocations[whichSpawn].position, 0.65f);
        foreach (Collider col in objects) { if (!col.CompareTag("Ground")) { return false; } }
        return true;
    }

    public void ExploredRoom(bool fromConnectedRoom)
    {
        Destroy(mapUnexplored);
        mapExplored.SetActive(true);
        if (!fromConnectedRoom)
        {
            _gameManager.GetTotalStats().AdjustStats(PlayerTotalStats.StatType.roomsExplored);
            if (connectedSpawners != null)
            {
                foreach (EnemySpawner spawners in connectedSpawners) { spawners.ExploredRoom(true); }
            }
        }
    }

    public void SpawnType(bool connectedSpawner)
    {
        _enemyTrackerController.enemyNavMesh.RemoveData();
        _enemyTrackerController.enemyNavMesh.BuildNavMesh();

        if (!connectedSpawner) { masterSpawner = this; }
        if (!room.otherPlayerEnteredRoom)
        {
            if (CoopManager.instance != null) { CoopManager.instance.coopEnemyController.EnteredRoom(room.roomID); }
            int enemySpawnChance = unityEngine::Random.Range(0, 100);
            if (!spawnedEnemies && enemySpawnChance > 10) { SpawnEnemy(connectedSpawner); }
            else if (connectedSpawner && !spawnedEnemies)
            {
                spawnedEnemies = true;
                masterSpawner.connectedSpawnersDone++;
                masterSpawner.AllEnemiesSpawned();
            }
            else
            {
                int puzzleSpawnChance = unityEngine::Random.Range(0, 100);
                if (puzzleSpawnChance > 25) { SpawnPuzzle(); }
                else { UnlockRoom(false); }
                spawnedEnemies = true;
                if (connectedSpawners != null) { foreach (EnemySpawner spawners in connectedSpawners) { spawners.gameObject.SetActive(false); } }
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (!room.roomCleared)
            {
                EnemyTrackerController enemyTracker = _enemyTrackerController;
                foreach (GameObject enemy in enemyTracker.otherPlayerSpawnedEnemies)
                {
                    enemy.SetActive(true);
                }
            }
        }
    }

    private void SpawnEnemy(bool connectedSpawner)
    {
        if (_enemyTrackerController.currentEnemySpawner == null && !connectedSpawner)
        {
            _enemyTrackerController.currentEnemySpawner = this;
            LockRoom(false);
        }
        int spawnCount;
        if (!smallRoom)
        {
            if (LocalGameManager.instance.currentGameMode == LocalGameManager.GameMode.master) 
            { 
                spawnCount = unityEngine::Random.Range(4, 6); 
            }
            else spawnCount = unityEngine::Random.Range(2, 4);

            spawnCount += unityEngine::Random.Range(0, LocalGameManager.instance.currentLevel);
        }
        else { spawnCount = unityEngine::Random.Range(2, (4 + LocalGameManager.instance.currentLevel)); }

        for (int i = 0; i < spawnCount; i++)
        {
            int randomSpawn = unityEngine::Random.Range(0, spawnLocations.Count);
            _enemyTrackerController.GetNewEnemy(spawnLocations[randomSpawn], false, 0, 0, 0, room.roomID);
        }

        spawnedEnemies = true;

        if (connectedSpawners != null && !connectedSpawner)
        {
            foreach (EnemySpawner spawners in connectedSpawners)
            {
                spawners.masterSpawner = this;
                spawners.SpawnType(true);
            }
        }
        else { AllEnemiesSpawned(); }

        if (connectedSpawner)
        {
            masterSpawner.connectedSpawnersDone++;
            masterSpawner.AllEnemiesSpawned();
        }
    }

    public void AllEnemiesSpawned()
    {
        if (connectedSpawners.Length == connectedSpawnersDone) 
        { 
            _enemyTrackerController.AssignEnemyID(room.roomID);
            if (connectedSpawners != null) { foreach (EnemySpawner spawners in connectedSpawners) { spawners.gameObject.SetActive(false); } }
            gameObject.SetActive(false);
        }
    }

    public void SpawnPuzzle()
    {
        Debug.Log("Puzzle Spawn");
    }

    public void LockRoom(bool fromConnectedRoom)
    {
        Debug.Log("Lock Room");
        foreach (GameObject obj in doorLocks) { if (obj != null) { obj.SetActive(true); } }
        if (!fromConnectedRoom && connectedSpawners != null)
        {
            foreach (EnemySpawner spawners in connectedSpawners) { spawners.LockRoom(true); }
        }
    }

    public void UnlockRoom(bool fromConnectedRoom)
    {
        Debug.Log("Unlock Room");
        foreach (GameObject obj in doorLocks) { if (obj != null) { Destroy(obj); } }
        if (!fromConnectedRoom)
        {
            room.roomCleared = true;
            if (connectedSpawners != null)
            {
                foreach (EnemySpawner spawners in connectedSpawners) { spawners.UnlockRoom(true); }
            }
            if (!room.otherPlayerEnteredRoom && CoopManager.instance != null) { CoopManager.instance.coopEnemyController.RoomCleared(room.roomID); }
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (_dungeonBuildParent != null || _enemySpawnerTracker.enemySpawners.Contains(this))
        {
            _enemySpawnerTracker.enemySpawners.Remove(this);
        }
    }
}
