using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    private LocalGameManager _gameManger;
    private VRPlayerController _player;
    private EnemyTrackerController _enemyTrackerController;

    [SerializeField] private GameObject _portalPrefab, _endOfDemoPrefab;
    [SerializeField] private Transform _portalSpawnLocation, _bossSpawnLocation;

    [HideInInspector]
    public GameObject spawnedBoss;

    public void Start()
    {
        _gameManger = LocalGameManager.instance;
        _gameManger.GetEnemyTrackerController().enemyNavMesh.BuildNavMesh();
        _gameManger.player.GetPlayerComponents().GetEyeManager().EyesOpening();
        Invoke("SpawnBoss", 3);
    }

    public void SpawnBoss()
    {
        int randomBoss = Random.Range(0, MasterManager.enemyPool.bossLists[_gameManger.currentLevel].bosses.Count);
        spawnedBoss = Instantiate(MasterManager.enemyPool.bossLists[_gameManger.dungeonType].bosses[randomBoss], _bossSpawnLocation);
    }

    public void BossDead()
    {
        if (_gameManger.IsDemo())
        {
            GameObject newPortal = Instantiate(_endOfDemoPrefab, _portalSpawnLocation);
            PortalSettings(newPortal);
        }
        else
        {
            GameObject newPortal = Instantiate(_portalPrefab, _portalSpawnLocation);
            PortalSettings(newPortal);
        }
    }

    private void PortalSettings(GameObject portal)
    {
        portal.transform.SetParent(_portalSpawnLocation);
        portal.transform.localPosition = new Vector3(0, 0, 0);
        portal.transform.localEulerAngles = new Vector3(0, 0, 0);
        portal.transform.localScale = new Vector3(1, 1, 1);
    }
}
