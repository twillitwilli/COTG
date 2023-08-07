using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBuildParent : MonoBehaviour
{
    public static DungeonBuildParent instance;

    public Rooms rooms;
    private MapController _mapController;
    private CompassController _compassController;
    private EnemySpawnerTracker _enemySpawnerTracker;

    [HideInInspector] public string dungeonType;

    private LocalGameManager _gameManager;
    private ChatManager _chatManager;

    private void Awake()
    {
        if (!instance) { instance = this; }
        else Destroy(gameObject);

        rooms = GetComponent<Rooms>();
        _mapController = GetComponent<MapController>();
        _compassController = GetComponent<CompassController>();
        _enemySpawnerTracker = GetComponent<EnemySpawnerTracker>();

        _gameManager = LocalGameManager.instance;
        _chatManager = _gameManager.GetChatManager();
    }

    public void DungeonBuildCompleted()
    {
        transform.SetParent(null);
        Destroy(DungeonGenerationV3.instance.gameObject);
        DungeonGenerationV3.instance = null;
        CheckRoomList();
        CheckSpawners();
        RenderOff();
        rooms.AssignRoomID();
        _gameManager.dungeonBuildCompleted = true;
        _chatManager.DebugMessage("Dungeon Build Ready: Level " + LocalGameManager.instance.currentLevel);
        Debug.Log("Dungeon Build Completed");

        if (CoopManager.instance != null)
        {
            CoopManager.instance.coopDungeonBuild.dungeonFloorsCompleted.Add(true);
            if (LocalGameManager.instance.isHost) 
            {
                CoopManager.instance.coopDungeonBuild.dungeonCompleted = true;
                CoopManager.instance.coopDungeonBuild.DungeonBuildCompleted(); 
            }
        }
    }

    private void CheckRoomList()
    {
        for (int i = 0; i < rooms.rooms.Count; i++) { if (!rooms.rooms[i]) { rooms.rooms.Remove(rooms.rooms[i]); } }
    }

    private void CheckSpawners()
    {
        for (int i = 0; i < _enemySpawnerTracker.enemySpawners.Count; i++) { _enemySpawnerTracker.enemySpawners[i].CheckSpawnLocations(); }
    }

    private void RenderOff()
    {
        for (int i = 0; i < rooms.rooms.Count; i++)
        {
            if (rooms.rooms[i]) { rooms.rooms[i].GetComponent<RoomModel>().rendererLink.renderedObjects.SetActive(false); }
        }
        rooms.startingRoom.GetComponent<RoomModel>().rendererLink.renderedObjects.SetActive(true);
    }

    public Rooms GetRooms()
    {
        return rooms;
    }

    public MapController GetMapController()
    {
        return _mapController;
    }

    public CompassController GetCompassController()
    {
        return _compassController;
    }

    public EnemySpawnerTracker GetEnemySpawnerTracker()
    {
        return _enemySpawnerTracker;
    }
}
