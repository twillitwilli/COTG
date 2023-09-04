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
            case ItemPoolType.health:
                if (_healthPool[0] == null)
                    SpawnNewItem(ItemPoolType.health);

                else
                    GetFromHealthPool();
                break;

            case ItemPoolType.gold:
                if (_goldPool[0] == null)
                    SpawnNewItem(ItemPoolType.gold);


                    break;

            case ItemPoolType.bombCrystal:
                break;

            case ItemPoolType.keyCrystal:
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

    GameObject GetFromHealthPool()
    {
        GameObject healthItem = null;

        if (_healthPool[0].activeSelf)
        {
            _healthIdx++;
            _healthIdx = _healthIdx > (_healthPool.Count - 1) ? 0 : _healthIdx;

            bool spawnNewHealthItem = _healthPool[_healthIdx].activeSelf ? true : false;

            if (spawnNewHealthItem)
                SpawnNewItem(ItemPoolType.health);

            else
                healthItem = _healthPool[_healthIdx];
        }

        else
        {
            _healthIdx = 0;
            healthItem = _healthPool[0];
        }

        return healthItem;
    }
}
