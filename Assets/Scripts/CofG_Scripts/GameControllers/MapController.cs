using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoSingleton<MapController>
{
    private PlayerComponents _playerComponents;

    public bool hasMapReveal;

    [SerializeField]
    private GameObject _rolledUpMapPrefab, _openedMapPrefab;

    private GameObject _rolledUpMapObject, _openedMapObject;

    [HideInInspector]
    public List<GameObject> mapBlocks = new List<GameObject>();

    private void Awake()
    {
        LocalGameManager.playerCreated += NewPlayerCreated;
    }

    public async void NewPlayerCreated(VRPlayerController player)
    {
        _playerComponents = player.GetPlayerComponents();

        SpawnNewMap();
    }

    public void SpawnNewMap()
    {
        GameObject map = Instantiate(_rolledUpMapPrefab);
        _rolledUpMapObject = map;

        for (int i = 0; i < _playerComponents.GetBothHands().Length; i++)
        {
            if (_playerComponents.GetBothHands()[i].IsPrimaryHand())
            {
                map.transform.SetParent(_playerComponents.GetAccessoryItemSlot(i));
            }
        }

        map.transform.localPosition = new Vector3(0, 0, 0);
        map.transform.localEulerAngles = new Vector3(90, 0, 0);
        map.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }

    public void GrabMap(GrabController grabController)
    {
        if (_rolledUpMapObject != null)
            Destroy(_rolledUpMapObject);

        if (_openedMapObject == null)
            _openedMapObject = Instantiate(_openedMapPrefab);

        if (grabController.GetHand().IsRightHand())
        {
            Vector3 mapPos = new Vector3(0.004255578f, -0.008148912f, -0.1995229f);
            Vector3 mapRot = new Vector3(88.854f, 0.712f, 93.76f);
            Vector3 mapScale = new Vector3(0.02499999f, 0.01f, 0.02499999f);
            grabController.ParentGrabbable(_openedMapObject, mapPos, mapRot, mapScale);
        }

        else
        {
            Vector3 mapPos = new Vector3(0.0121588f, -0.02328259f, -0.5700652f);
            Vector3 mapRot = new Vector3(88.854f, 0.712f, 93.76f);
            Vector3 mapScale = new Vector3(-0.07142855f, 0.02857143f, 0.07142855f);
            grabController.ParentGrabbable(_openedMapObject, mapPos, mapRot, mapScale);
        }

        if (grabController.GetOppositeGrabController().currentObjectGrabbed == PlayerItemGrabbable.PlayerItem.map)
            grabController.GetOppositeGrabController().ReleaseGrip();
    }

    public void ResetMap(GrabController grabController)
    {
        if (grabController.GetOppositeGrabController().currentObjectGrabbed != PlayerItemGrabbable.PlayerItem.map)
        {
            if (_openedMapObject != null)
                Destroy(_openedMapObject);

            if (_rolledUpMapObject != null)
                Destroy(_rolledUpMapObject);

            SpawnNewMap();
        }
    }

    public void RevealDungeonMap()
    {
        foreach (GameObject obj in mapBlocks) { Destroy(obj); }
    }

    public void ResetMapBlocks()
    {
        mapBlocks.Clear();
    }
}
