using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.AbstractClasses;

public class DungeonGeneratorPrefabCaller : MonoSingleton<DungeonGeneratorPrefabCaller>
{
    public static DungeonGeneratorPrefabCaller instance;

    [SerializeField] 
    private GameObject _dungeonGeneratorPrefab;
    
    public GameObject roomPrefabObjects;
    
    public GameObject[] loadingAreas;


    private void Awake()
    {
        Instantiate(roomPrefabObjects);

        if (!MultiplayerManager.Instance.coop || MultiplayerManager.Instance.coop && LocalGameManager.Instance.isHost)
        {
            ChatManager.Instance.DebugMessage("Building Dungeon");

            int randomLoadingArea = Random.Range(0, loadingAreas.Length);
            Instantiate(loadingAreas[randomLoadingArea], transform.position, transform.rotation);
        }
        
        else if (MultiplayerManager.Instance.coop && !LocalGameManager.Instance.isHost)
            MultiplayerManager.Instance.GetCoopManager().coopDungeonBuild.CheckSpawnedLoadingArea();
    }

    private void Start()
    {
        PlayerCurse.Instance.RunCurseCheck();

        Invoke("SpawnDungeonGenerator", 5);
    }

    public void SpawnDungeonGenerator()
    {
        Instantiate(_dungeonGeneratorPrefab);
    }
}
