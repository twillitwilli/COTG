using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSelection : MonoBehaviour
{
    public GameObject dropParent;
    public bool spawnSpecificItem;

    public ItemPoolManager.DroppableItem droppableItem;

    private GameObject newDrop;

    private void Start()
    {
        if (!spawnSpecificItem)
            RandomItem();

        else
        {
            switch (droppableItem)
            {
                case ItemPoolManager.DroppableItem.health:
                    newDrop = ItemPoolManager.Instance.GetItem(ItemPoolManager.ItemPoolType.health);
                    break;

                case ItemPoolManager.DroppableItem.gold:
                    newDrop = ItemPoolManager.Instance.GetItem(ItemPoolManager.ItemPoolType.gold);
                    break;

                case ItemPoolManager.DroppableItem.arcaneEnergy:
                    newDrop = ItemPoolManager.Instance.GetItem(ItemPoolManager.ItemPoolType.bombCrystal);
                    break;

                case ItemPoolManager.DroppableItem.key:
                    newDrop = ItemPoolManager.Instance.GetItem(ItemPoolManager.ItemPoolType.keyCrystal);
                    break;

                case ItemPoolManager.DroppableItem.ritualRune:
                    newDrop = ItemPoolManager.Instance.GetItem(ItemPoolManager.ItemPoolType.rune);
                    break;

                case ItemPoolManager.DroppableItem.soul:
                    newDrop = ItemPoolManager.Instance.GetItem(ItemPoolManager.ItemPoolType.soul);
                    break;
            }

            ItemSettings();
        }
    }

    private void RandomItem()
    {
        int itemChance = Random.Range(1, 100);

        //gold chance
        if (itemChance <= 65)
            newDrop = ItemPoolManager.Instance.GetItem(ItemPoolManager.ItemPoolType.gold);

        ///arcane chance
        else if (itemChance > 65 && itemChance <= 80)
            newDrop = ItemPoolManager.Instance.GetItem(ItemPoolManager.ItemPoolType.bombCrystal);

        //health chance
        else if (itemChance > 85 && itemChance <= 92)
            newDrop = ItemPoolManager.Instance.GetItem(ItemPoolManager.ItemPoolType.health);

        //key chance
        else if (itemChance > 92 && itemChance < 98)
            newDrop = ItemPoolManager.Instance.GetItem(ItemPoolManager.ItemPoolType.keyCrystal);

        //ritual rune chance
        else
            newDrop = ItemPoolManager.Instance.GetItem(ItemPoolManager.ItemPoolType.rune);

        ItemSettings();
    }

    private void ItemSettings()
    {
        newDrop.GetComponent<Item>().dropParent = dropParent;
        newDrop.transform.SetParent(transform);
        newDrop.transform.localPosition = new Vector3(0, 0, 0);
        newDrop.transform.localEulerAngles = new Vector3(90, 0, 0);
    }
}
