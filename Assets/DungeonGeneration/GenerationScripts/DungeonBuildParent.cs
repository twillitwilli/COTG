using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DungeonBuildParent : MonoSingleton<DungeonBuildParent>
{
    public Rooms rooms;

    [HideInInspector] 
    public string dungeonType;

    private void Awake()
    {
        rooms = GetComponent<Rooms>();
    }

    public void DungeonBuildCompleted()
    {
        transform.SetParent(null);

        Destroy(DungeonGenerationV3.Instance.gameObject);

        CheckRoomList();
        CheckSpawners();
        RenderOff();
        rooms.AssignRoomID();

        LocalGameManager.Instance.dungeonBuildCompleted = true;
        ChatManager.Instance.DebugMessage("Dungeon Build Ready: Level " + LocalGameManager.Instance.currentLevel);
        Debug.Log("Dungeon Build Completed");

        if (CoopManager.instance != null)
        {
            CoopManager.instance.coopDungeonBuild.dungeonFloorsCompleted.Add(true);
            if (LocalGameManager.Instance.isHost) 
            {
                CoopManager.instance.coopDungeonBuild.dungeonCompleted = true;
                CoopManager.instance.coopDungeonBuild.DungeonBuildCompleted(); 
            }
        }
    }

    private async Task CheckRoomList()
    {
        for (int i = 0; i < rooms.rooms.Count; i++) 
        { 
            if (!rooms.rooms[i])
                rooms.rooms.Remove(rooms.rooms[i]);
        }
    }

    private void CheckSpawners()
    {
        for (int i = 0; i < EnemyTrackerController.Instance.enemySpawners.Count; i++)
            EnemyTrackerController.Instance.enemySpawners[i].CheckSpawnLocations();
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
}
