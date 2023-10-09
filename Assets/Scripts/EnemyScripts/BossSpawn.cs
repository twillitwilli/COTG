using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    VRPlayer _player;

    [SerializeField] 
    GameObject 
        _portalPrefab, 
        _endOfDemoPrefab;

    [SerializeField] 
    Transform 
        _portalSpawnLocation, 
        _bossSpawnLocation;

    public GameObject spawnedBoss { get; set; }

    public async void Start()
    {
        EnemyTrackerController.Instance.enemyNavMesh.BuildNavMesh();
        LocalGameManager.Instance.player.GetPlayerComponents().GetEyeManager().EyesOpening();

        await Task.Delay(3000);

        SpawnBoss();
    }

    public void SpawnBoss()
    {
        int randomBoss = Random.Range(0, MasterManager.enemyPool.bossLists[LocalGameManager.Instance.currentLevel].bosses.Count);
        spawnedBoss = Instantiate(MasterManager.enemyPool.bossLists[LocalGameManager.Instance.dungeonType].bosses[randomBoss], _bossSpawnLocation);
    }

    public void BossDead()
    {
        if (LocalGameManager.Instance.IsDemo())
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

    void PortalSettings(GameObject portal)
    {
        portal.transform.SetParent(_portalSpawnLocation);
        portal.transform.localPosition = new Vector3(0, 0, 0);
        portal.transform.localEulerAngles = new Vector3(0, 0, 0);
        portal.transform.localScale = new Vector3(1, 1, 1);
    }
}
