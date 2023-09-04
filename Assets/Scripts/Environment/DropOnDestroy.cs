using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [Range(0, 100)]
    public int dropPercentage;

    [HideInInspector] 
    public bool disableDrop;

    public bool dropSpecificItem, isEnemy;

    public DroppableItems.itemType typeOfItem;

    private void OnDestroy()
    {
        if (!disableDrop)
        {
            if (isEnemy)
                LocalGameManager.Instance.SpawnSpecificItem(ItemDropSelection.ItemType.soul, transform);

            if (!dropSpecificItem && Random.Range(0, 100) <= (dropPercentage + PlayerStats.Instance.GetLuck()))
                LocalGameManager.Instance.SpawnRandomDrop(transform);

            else if (dropSpecificItem)
            {
                switch (typeOfItem)
                {
                    case DroppableItems.itemType.gold:
                        LocalGameManager.Instance.SpawnSpecificItem(ItemDropSelection.ItemType.gold, transform);
                        break;

                    case DroppableItems.itemType.key:
                        LocalGameManager.Instance.SpawnSpecificItem(ItemDropSelection.ItemType.key, transform);
                        break;

                    case DroppableItems.itemType.arcaneOrb:
                        LocalGameManager.Instance.SpawnSpecificItem(ItemDropSelection.ItemType.arcaneEnergy, transform);
                        break;

                    case DroppableItems.itemType.health:
                        LocalGameManager.Instance.SpawnSpecificItem(ItemDropSelection.ItemType.health, transform);
                        break;
                }
            }
        }
    }
}
