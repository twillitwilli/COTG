using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoolManager : MonoSingleton<ItemPoolManager>
{
    public enum ItemPoolType
    {
        gold,
        bombCrystal,
        ignitedBomb,
        keyCrystal,
        rune,
        classCard,
        jar,
        rock,
        health,
        soul,
        chest,
        itemPedastal
    }

    public enum GrabbableItem
    {
        nothing,
        map,
        wallet,
        bomb,
        key,
        potion,
        staff,
        bow,
        bowString,
        rune,
        climbable,
        jar,
        classCard,
        ignitedBomb
    }

    public enum DroppableItem
    {
        health, 
        gold, 
        arcaneEnergy, 
        key, 
        ritualRune, 
        soul
    }

    public enum ChestType
    {
        free,
        key,
        soul,
        health,
        gold
    }

    List<GameObject> _healthPool = new List<GameObject>();
    int _healthIdx;

    List<GameObject> _goldPool = new List<GameObject>();
    int _goldIdx;

    List<GameObject> _bombCrystalPool = new List<GameObject>();
    int _bombCrystalIdx;

    List<GameObject> _ignitedBomb = new List<GameObject>();
    int _ignitedBombIdx;

    List<GameObject> _keyCrystalPool = new List<GameObject>();
    int _keyCrystalIdx;

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.transform.SetParent(transform);

        obj.transform.position = transform.position;

        obj.SetActive(false);
    }

    public GameObject GetItem(ItemPoolType poolType)
    {
        GameObject newItem = null;

        switch (poolType)
        {
            // HEALTH DROPS
            case ItemPoolType.health:

                if (_healthPool[0] == null)
                    newItem = SpawnNewItem(ItemPoolType.health);

                else
                {
                    _healthIdx++;
                    _healthIdx = _healthIdx > (_healthPool.Count - 1) ? 0 : _healthIdx;

                    newItem = GetItemFromPool(_healthIdx, _healthPool, ItemPoolType.health);

                    if (_healthPool.IndexOf(newItem) == 0)
                        _healthIdx = 0;
                }

                break;

            // GOLD DROPS
            case ItemPoolType.gold:

                if (_goldPool[0] == null)
                    newItem = SpawnNewItem(ItemPoolType.gold);

                else
                {
                    _goldIdx++;
                    _goldIdx = _goldIdx > (_goldPool.Count - 1) ? 0 : _goldIdx;

                    newItem = GetItemFromPool(_goldIdx, _goldPool, ItemPoolType.gold);

                    if (_goldPool.IndexOf(newItem) == 0)
                        _goldIdx = 0;
                }

                    break;

            // BOMB CRYSTAL DROPS
            case ItemPoolType.bombCrystal:

                if (_bombCrystalPool[0] == null)
                    newItem = SpawnNewItem(ItemPoolType.bombCrystal);

                else
                {
                    _bombCrystalIdx++;
                    _bombCrystalIdx = _bombCrystalIdx > (_bombCrystalPool.Count - 1) ? 0 : _bombCrystalIdx;

                    newItem = GetItemFromPool(_bombCrystalIdx, _bombCrystalPool, ItemPoolType.bombCrystal);

                    if (_bombCrystalPool.IndexOf(newItem) == 0)
                        _bombCrystalIdx = 0;
                }

                break;

            // KEY CRYSTAL DROPS
            case ItemPoolType.keyCrystal:

                if (_keyCrystalPool[0] == null)
                    newItem = SpawnNewItem(ItemPoolType.keyCrystal);

                else
                {
                    _keyCrystalIdx++;
                    _keyCrystalIdx = _keyCrystalIdx > (_keyCrystalPool.Count - 1) ? 0 : _keyCrystalIdx;

                    newItem = GetItemFromPool(_keyCrystalIdx, _keyCrystalPool, ItemPoolType.keyCrystal);

                    if (_keyCrystalPool.IndexOf(newItem) == 0)
                        _keyCrystalIdx = 0;
                }

                break;
        }

        return newItem;
    }

    GameObject SpawnNewItem(ItemPoolType poolType)
    {
        GameObject newItem = null;

        switch (poolType)
        {
            case ItemPoolType.health:
                newItem = Instantiate(MasterManager.itemPool.droppableItems.droppableItem[2]);

                _healthPool.Add(newItem);
                break;

            case ItemPoolType.gold:
                newItem = Instantiate(MasterManager.itemPool.droppableItems.droppableItem[0]);

                _goldPool.Add(newItem);
                break;

            case ItemPoolType.bombCrystal:
                newItem = Instantiate(MasterManager.itemPool.droppableItems.droppableItem[1]);

                _bombCrystalPool.Add(newItem);
                break;

            case ItemPoolType.keyCrystal:
                newItem = Instantiate(MasterManager.itemPool.droppableItems.droppableItem[3]);

                _keyCrystalPool.Add(newItem);
                break;
        }

        newItem.transform.SetParent(transform);

        return newItem;
    }

    GameObject GetItemFromPool(int poolIdx, List<GameObject> whichPool, ItemPoolType whichItem)
    {
        GameObject itemFromPool = null;

        if (whichPool[0].activeSelf)
        {
            bool spawnNewItem = whichPool[poolIdx].activeSelf ? true : false;

            if (spawnNewItem)
                itemFromPool = SpawnNewItem(whichItem);

            else
                itemFromPool = whichPool[poolIdx];
        }

        else
            itemFromPool = whichPool[0];

        if (!itemFromPool.activeSelf)
            itemFromPool.SetActive(true);

        return itemFromPool;
    }
}
