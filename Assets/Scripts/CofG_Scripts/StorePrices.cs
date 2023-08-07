using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePrices : MonoBehaviour
{
    public bool refillShop, spawnKey;
    public Transform boughtPotionSpawn;
    public SpawnRandomScroll scrollSpawn;
    public Transform[] itemSpawns;
    public string[] shopItem;
    public int[] shopPrices;
    public Text[] itemDescriptions;
    public Text[] itemPriceDisplays;
    public GameObject[] spawnedItems;
    private int potionSpawn;

    public void Awake()
    {
        for (int i = 0; i < shopPrices.Length; i++) { shopPrices[i] = Random.Range(50, 150); }
        SetItemPrices();
        SpawnItems();
    }

    public void SetItemPrices()
    {
        for (int i = 0; i < itemPriceDisplays.Length; i++) { itemPriceDisplays[i].text = shopPrices[i].ToString(); }
    }

    public void SpawnItems()
    {
        if (!spawnedItems[0]) { SpawnPotion(); }
        if (!spawnedItems[1]) { SpawnBasicItem(false, 2, shopPrices[1]); }
        if (!spawnedItems[2])
        {
            if (spawnKey) 
            {
                shopItem[2] = "Crystal Key";
                SpawnBasicItem(true, 1, shopPrices[2]);
            }
            else
            {
                int randomItem = Random.Range(0, 10);
                if (randomItem >= 3)
                {
                    shopItem[2] = "Arcane Energy";
                    SpawnBasicItem(true, 0, shopPrices[2]);
                }
                else
                {
                    shopItem[2] = "Crystal Key";
                    SpawnBasicItem(true, 1, shopPrices[2]);
                }
            }
        }
        SetItemDescriptions();
    }

    public void SetItemDescriptions()
    {
        for (int i = 0; i < itemDescriptions.Length; i++) { itemDescriptions[i].text = shopItem[i]; }
    }    

    public void SpawnPotion()
    {
        potionSpawn = Random.Range(0, MasterManager.itemPool.shopItems.potions.Count);
        GameObject newPotion = Instantiate(MasterManager.itemPool.shopItems.potions[potionSpawn]);
        newPotion.transform.SetParent(itemSpawns[0]);
        ResetItemTransforms(newPotion.transform);
        SetItemShopSettings(newPotion.GetComponent<Item>(), shopPrices[0]);
    }

    public void SpawnBasicItem(bool optionalItem, int whichItem, int shopPrice)
    {
        GameObject newItem = Instantiate(MasterManager.itemPool.shopItems.basicItems[whichItem]);
        if (!optionalItem) { newItem.transform.SetParent(itemSpawns[1]); }
        else { newItem.transform.SetParent(itemSpawns[2]); }
        ResetItemTransforms(newItem.transform);
        SetItemShopSettings(newItem.GetComponent<Item>(), shopPrice);
    }

    public void ResetItemTransforms(Transform item)
    {
        item.localPosition = new Vector3(0, 0, 0);
        item.localEulerAngles = new Vector3(0, 0, 0);
        item.localScale = new Vector3(1, 1, 1);
    }

    public void SetItemShopSettings(Item newItem, int shopPrice)
    {
        newItem.SetStoreSettings(this, shopPrice);
    }

    public void BoughtPotion()
    {
        GameObject newPotion = Instantiate(MasterManager.itemPool.droppableItems.potions[potionSpawn]);
        newPotion.transform.SetParent(boughtPotionSpawn);
        ResetItemTransforms(newPotion.transform);
    }

    public bool CheckStoreRefill()
    {
        for (int i = 0; i < spawnedItems.Length; i++) { if (!spawnedItems[i]) { return true; } }
        return false;
    }

    public void StoreRefill()
    {
        Invoke("SpawnItems", 3);
    }
}
