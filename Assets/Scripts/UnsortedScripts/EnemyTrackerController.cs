using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using QTArts.AbstractClasses;

public class EnemyTrackerController : MonoSingleton<EnemyTrackerController>
{
    VRPlayer _player;

    public NavMeshSurface enemyNavMesh { get; set; }

    [HideInInspector] 
    public bool 
        enemiesDead, 
        bossesDead, 
        reaperExists, 
        doorsLocked;

    [HideInInspector] 
    public int 
        currentEnemies, 
        currentBosses;

    public GameObject spawnedReaper { get; set; }

    [HideInInspector]
    public List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

    [HideInInspector] 
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    [HideInInspector] 
    public List<GameObject> otherPlayerSpawnedEnemies = new List<GameObject>();

    public GameObject spawnedBoss { get; set; }

    public EnemySpawner currentEnemySpawner { get; set; }

    public bool hasEnemyHealthReveal { get; private set; }

    public void SetEnemyHealthReveal(bool hasHealthReveal)
    {
        hasEnemyHealthReveal = hasHealthReveal;
    }

    public override void Awake()
    {
        base.Awake();

        enemyNavMesh = GetComponent<NavMeshSurface>();

        LocalGameManager.playerCreated += NewPlayerCreated;
    }

    void NewPlayerCreated(VRPlayer player)
    {
        _player = player;
    }

    public void Reset()
    {
        spawnedEnemies.Clear();
        spawnedBoss = null;
        otherPlayerSpawnedEnemies.Clear();
    }

    public void CheckSpawners()
    {
        for (int i = 0; i < enemySpawners.Count; i++)
        {
            enemySpawners[i].CheckSpawnLocations();
        }
    }

    public void GetNewEnemy(Transform spawnLocation, bool networkSpawn, int enemy, int levelOfEnemy, int ID, int roomID)
    {
        if (!networkSpawn)
        {
            int spawnEnemy = EnemySpawnSelection(LocalGameManager.Instance.currentLevel);
            EnemyController.Enemy enemyName = MasterManager.enemyPool.enemyStatManagers[spawnEnemy].enemyType;
            int enemyLevel = EnemySpawnLevel(enemyName);
            EnemySpawn(spawnEnemy, enemyLevel, spawnLocation, networkSpawn, ID, roomID);
        }

        else
            EnemySpawn(enemy, levelOfEnemy, spawnLocation, networkSpawn, ID, roomID);
    }

    public void EnemySpawn(int enemy, int level, Transform spawnLocation, bool networkSpawn, int ID, int roomID)
    {
        GameObject newEnemy = Instantiate(MasterManager.enemyPool.spawnableEnemies[enemy], spawnLocation.position, spawnLocation.rotation);
        newEnemy.transform.SetParent(null);
        newEnemy.transform.localScale = new Vector3(1, 1, 1);

        if (!networkSpawn)
        {
            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
            enemyController.roomID = roomID;
        }
        else
        {
            EnemyTracker enemyTracker = newEnemy.GetComponent<EnemyTracker>();
            enemyTracker.otherPlayerSpawned = true;
            otherPlayerSpawnedEnemies.Add(newEnemy);

            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
            enemyController.spawnID = ID;
            enemyController.roomID = roomID;
            enemyController.enemyID = enemy;
            enemyController.enemyLevel = level;

            if (_player.roomID == roomID)
                newEnemy.gameObject.SetActive(true);

            else
                newEnemy.gameObject.SetActive(false);
        }
    }

    public int EnemySpawnSelection(int currentLevel)
    {
        switch (currentLevel)
        {
            case 1:
                return Random.Range(0, 3);

            case 2:
                return Random.Range(0, 5);

            case 3:
                return Random.Range(0, 7);
        }

        return 0;
    }

    public int EnemySpawnLevel(EnemyController.Enemy enemyType)
    {
        PlayerProgressStats progressionStats = PlayerProgressStats.Instance;

        int enemyLevel = 0;

        switch(enemyType)
        {
            case EnemyController.Enemy.bat:
                enemyLevel = Random.Range(0, progressionStats.batLevel);
                break;

            case EnemyController.Enemy.bee:
                enemyLevel = Random.Range(0, progressionStats.beeLevel);
                break;

            case EnemyController.Enemy.bunny:
                enemyLevel = Random.Range(0, progressionStats.bunnyLevel);
                break;

            case EnemyController.Enemy.goblin:
                enemyLevel = Random.Range(0, progressionStats.goblinLevel);
                break;

            case EnemyController.Enemy.mushroom:
                enemyLevel = Random.Range(0, progressionStats.mushroomLevel);
                break;

            case EnemyController.Enemy.plant:
                enemyLevel = Random.Range(0, progressionStats.plantLevel);
                break;

            case EnemyController.Enemy.wolf:
                enemyLevel = Random.Range(0, progressionStats.wolfLevel);
                break;
        }

        return enemyLevel;
    }

    public void AssignEnemyID(int roomID)
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i] != null)
            {
                EnemyController enemy = spawnedEnemies[i].GetComponent<EnemyController>();

                if (!enemy.idAssigned)
                {
                    enemy.spawnID = i;
                    enemy.idAssigned = true;
                }
            }

            else
            {
                spawnedEnemies.Remove(spawnedEnemies[i]);
                i--;
            }
        }

        if (MultiplayerManager.Instance.coop)
            MultiplayerManager.Instance.GetCoopManager().coopEnemyController.SendEnemySpawnInfo(roomID);
    }

    public void UpdateEnemyTracker(EnemyTracker enemyTracker, int enemyCount)
    {
        int previousEnemyCount = currentEnemies;
        currentEnemies += enemyCount;

        if (currentEnemies > previousEnemyCount)
            enemyTracker.spawnCount = currentEnemies;

        if (!doorsLocked && currentEnemies > 0)
        {
            if (currentBosses <= 0)
                AudioController.Instance.ChangeMusic(AudioController.MusicTracks.Combat);

            enemiesDead = false;
            doorsLocked = true;
        }

        else if (!enemiesDead && currentEnemies <= 0)
        {
            doorsLocked = false;
            currentEnemies = 0;
            RoomCleared();
            enemiesDead = true;
        }
    }

    public void UpdateBossTracker(int bossCount)
    {
        currentBosses += bossCount;
        if (currentBosses > 0)
        {
            AudioController.Instance.ChangeMusic(AudioController.MusicTracks.Boss);
            bossesDead = false;
        }

        else if (!bossesDead && currentBosses <= 0)
        {
            LocalGameManager.Instance.spawnedBossArena.GetComponent<BossSpawn>().BossDead();
            RoomCleared();
            bossesDead = true;
        }
    }

    public EnemyController GetEnemy(int spawnID, bool fromOtherPlayerSpawnedEnemies)
    {
        if (!fromOtherPlayerSpawnedEnemies)
        {
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                if (spawnedEnemies[i].GetComponent<EnemyController>().spawnID == spawnID)
                    return spawnedEnemies[i].GetComponent<EnemyController>();
            }
        }

        else
        {
            for (int i = 0; i < otherPlayerSpawnedEnemies.Count; i++)
            {
                if (otherPlayerSpawnedEnemies[i].GetComponent<EnemyController>().spawnID == spawnID)
                    return otherPlayerSpawnedEnemies[i].GetComponent<EnemyController>();
            }
        }

        Debug.Log("no matching enemy found");

        return null;
    }

    public void RoomCleared()
    {
        if (currentBosses <= 0 && currentEnemies <= 0)
        {
            if (currentEnemySpawner != null)
                currentEnemySpawner.UnlockRoom(false);

            enemyNavMesh.RemoveData();
            AudioController.Instance.ChangeMusic(AudioController.MusicTracks.Forest);
        }
    }

    public void KillAllEnemies()
    {
        if (spawnedEnemies.Count > 0)
        {
            foreach (GameObject enemy in spawnedEnemies) 
            { 
                enemy.GetComponent<EnemyController>().EnemyDead(); 
            }

            spawnedEnemies.Clear();
        }
    }

    public void ReaperTrackingController()
    {

    }

    public void KillReaper()
    {
        if (spawnedReaper)
            Destroy(spawnedReaper);
    }
}
