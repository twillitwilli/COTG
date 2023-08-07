using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoomSpawner : MonoBehaviour
{
    private DungeonBuildParent _dungeonBuildParent;
    private CompassController _compassController;

    private AudioController _audioController;

    public enum roomType { bossPortalRoom, itemRoom, ritualRoom, sacrificeRoom, shopRoom }
    public roomType typeOfRoom;
    public GameObject spawnTriggers, mapUnexplored, mapExplored, roomIndicator;
    public Transform spawnLocation;
    public RoomSignController signController;
    public GameObject[] roomPrefabs;

    private void Start()
    {
        _dungeonBuildParent = DungeonBuildParent.instance;
        _compassController = _dungeonBuildParent.GetCompassController();

        _audioController = LocalGameManager.instance.GetAudioController();
    }

    public void ExploredRoom()
    {
        Destroy(mapUnexplored);
        mapExplored.SetActive(true);
        roomIndicator.transform.localPosition = new Vector3(0, 3, 0);
    }

    public void SpawnRoom()
    {
        spawnTriggers.SetActive(false);

        switch (typeOfRoom)
        {
            case roomType.bossPortalRoom:
                _compassController.CompassRevealSpecificRoom(_compassController.bossRoom);
                SpawnedObjects(0);
                break;

            case roomType.itemRoom:
                _compassController.CompassRevealSpecificRoom(_compassController.itemRoom);
                SpawnedObjects(1);
                break;

            case roomType.ritualRoom:
                _compassController.CompassRevealSpecificRoom(_compassController.ritualRoom);
                SpawnedObjects(2);
                break;

            case roomType.sacrificeRoom:
                _compassController.CompassRevealSpecificRoom(_compassController.sacrificeRoom);
                SpawnedObjects(3);
                break;

            case roomType.shopRoom:
                _compassController.CompassRevealSpecificRoom(_compassController.shopRoom);
                SpawnedObjects(4);
                _audioController.ChangeMusic(AudioController.MusicTracks.ShopRoom);
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
