using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneratorPrefabCaller : MonoBehaviour
{
    public static DungeonGeneratorPrefabCaller instance;

    private LocalGameManager _gameManager;
    private ChatManager _chatManager;

    [SerializeField] private GameObject _dungeonGeneratorPrefab;
    public GameObject roomPrefabObjects;
    public GameObject[] loadingAreas;


    private void Awake()
    {
        if (!instance) { instance = this; }
        else { Destroy(gameObject); }
        Instantiate(roomPrefabObjects);

        _gameManager = LocalGameManager.instance;
        _chatManager = _gameManager.GetChatManager();

        if (CoopManager.instance == null || CoopManager.instance != null && LocalGameManager.instance.isHost)
        {
            _chatManager.DebugMessage("Building Dungeon");
            int randomLoadingArea = Random.Range(0, loadingAreas.Length);
            Instantiate(loadingAreas[randomLoadingArea], transform.position, transform.rotation);
        }
        else if (CoopManager.instance != null && !LocalGameManager.instance.isHost)
        {
            CoopManager.instance.coopDungeonBuild.CheckSpawnedLoadingArea();
        }
    }

    private void Start()
    {
        _gameManager.GetCurseController().RunCurseCheck();

        Invoke("SpawnDungeonGenerator", 5);
    }

    public void SpawnDungeonGenerator()
    {
        Instantiate(_dungeonGeneratorPrefab);
    }
}
