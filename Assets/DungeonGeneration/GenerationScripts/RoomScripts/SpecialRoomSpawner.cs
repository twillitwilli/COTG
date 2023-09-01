using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoomSpawner : MonoBehaviour
{
    public enum roomType 
    { 
        bossPortalRoom, 
        itemRoom, 
        ritualRoom, 
        sacrificeRoom, 
        shopRoom 
    }
    public roomType typeOfRoom;
    
    public GameObject spawnTriggers, mapUnexplored, mapExplored, roomIndicator;
    
    public Transform spawnLocation;
    
    public RoomSignController signController;
    
    public GameObject[] roomPrefabs;

    public void ExploredRoom()
    {
        Destroy(mapUnexplored);
        mapExplored.SetActive(true);
        roomIndicator.transform.localPosition = new Vector3(0, 3, 0);
    }

    public void SpawnRoom()
    {
        CompassController compassController = CompassController.Instance;

        spawnTriggers.SetActive(false);

        switch (typeOfRoom)
        {
            case roomType.bossPortalRoom:
                compassController.CompassRevealSpecificRoom(compassController.bossRoom);
                SpawnedObjects(0);
                break;

            case roomType.itemRoom:
                compassController.CompassRevealSpecificRoom(compassController.itemRoom);
                SpawnedObjects(1);
                break;

            case roomType.ritualRoom:
                compassController.CompassRevealSpecificRoom(compassController.ritualRoom);
                SpawnedObjects(2);
                break;

            case roomType.sacrificeRoom:
                compassController.CompassRevealSpecificRoom(compassController.sacrificeRoom);
                SpawnedObjects(3);
                break;

            case roomType.shopRoom:
                compassController.CompassRevealSpecificRoom(compassController.shopRoom);
                SpawnedObjects(4);
                AudioController.Instance.ChangeMusic(AudioController.MusicTracks.ShopRoom);
                break;
        }
    }

    private void SpawnedObjects(int roomPrefab)
    {
        GameObject obj = Instantiate(roomPrefabs[roomPrefab]);
        obj.transform.SetParent(spawnLocation);
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localEulerAngles = new Vector3(0, 0, 0);
        obj.transform.localScale = new Vector3(1, 1, 1);
        if (!signController.signObj.activeSelf) { signController.ActivateSign(); }
    }
}
