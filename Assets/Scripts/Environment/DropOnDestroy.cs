using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private VRPlayerController _player;

    [Range(0, 100)]
    public int dropPercentage;
    [HideInInspector] public bool disableDrop;
    public bool dropSpecificItem, isEnemy;
    public DroppableItems.itemType typeOfItem;

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
        _player = _gameManager.player;
    }

    private void OnDestroy()
    {
        if (!disableDrop)
        {
            if (isEnemy)
            {
                _gameManager.SpawnSpecificItem(ItemDropSelection.ItemType.soul, transform);
            }
            if (!dropSpecificItem && Random.Range(0, 100) <= (dropPercentage + LocalGameManager.instance.GetPlayerStats().GetLuck()))
            {
                _gameManager.SpawnRandomDrop(transform);
            }
            else if (dropSpecificItem)
            {
                switch (typeOfItem)
                {
                    case DroppableItems.itemType.gold:
                        _gameManager.SpawnSpecificItem(ItemDropSelection.ItemType.gold, transform);
                        break;
                    case DroppableItems.itemType.key:
                        _gameManager.SpawnSpecificItem(ItemDropSelection.ItemType.key, transform);
                        break;
                    case DroppableItems.itemType.arcaneOrb:
                        _gameManager.SpawnSpecificItem(ItemDropSelection.ItemType.arcaneEnergy, transform);
                        break;
                    case DroppableItems.itemType.health:
                        _gameManager.SpawnSpecificItem(ItemDropSelection.ItemType.health, transform);
                        break;
                }
            }
        }
    }
}
