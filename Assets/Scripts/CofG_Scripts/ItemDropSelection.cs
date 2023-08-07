using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSelection : MonoBehaviour
{
    public GameObject dropParent;
    public bool spawnSpecificItem;
    public enum ItemType { health, gold, arcaneEnergy, key, ritualRune, soul }
    public ItemType itemType;

    private int itemToSpawn;

    private void Start()
    {
        if (!spawnSpecificItem) { RandomItem(); }
        else
        {
            switch (itemType)
            {
                case ItemType.health:
                    itemToSpawn = 2;
                    break;
                case ItemType.gold:
                    itemToSpawn = 0;
                    break;
                case ItemType.arcaneEnergy:
                    itemToSpawn = 1;
                    break;
                case ItemType.key:
                    itemToSpawn = 3;
                    break;
                case ItemType.ritualRune:
                    itemToSpawn = 4;
                    break;
                case ItemType.soul:
                    itemToSpawn = 5;
                    break;
            }
            ItemDrop(itemToSpawn);
        }
    }

    private void RandomItem()
    {
        int itemChance = Random.Range(1, 100);
        //gold chance
        if (itemChance <= 65) { ItemDrop(0); }
        ///arcane chance
        else if (itemChance > 65 && itemChance <= 80) { ItemDrop(1); }
        //health chance
        else if (itemChance > 85 && itemChance <= 92) { ItemDrop(2); }
        //key chance
        else if (itemChance > 92 && itemChance < 99) { ItemDrop(3); }
        //ritual rune chance
        else if (itemChance == 99) { ItemDrop(4); }
    }

    private void ItemDrop(int item)
    {
        GameObject droppedItem = Instantiate(MasterManager.itemPool.droppableItems.droppableItem[item]);
        droppedItem.GetComponent<Item>().dropParent = dropParent;
        droppedItem.transform.SetParent(transform);
        droppedItem.transform.localPosition = new Vector3(0, 0, 0);
        droppedItem.transform.localEulerAngles = new Vector3(90, 0, 0);
    }
}
