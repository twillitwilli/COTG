using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomScroll : MonoBehaviour
{
    public ItemPools.ItemScrollType typeOfScroll;
    [Header("--For 2 items--")]
    public SpawnRandomScroll otherItemScrollSpawner;
    private StorePrices store;
    [HideInInspector] public GameObject spawnedScroll;
    private Vector2Int scrollParameters;

    private void Start()
    {
        if (typeOfScroll == ItemPools.ItemScrollType.shopRoom) { store = GetComponentInParent<StorePrices>(); } 
        SpawnScroll();
    }

    public void SpawnScroll()
    {
        switch (typeOfScroll)
        {
            case ItemPools.ItemScrollType.random:
                int randomPool = GetRandomItemPool();
                if (randomPool == 2) { SpawnBlankScroll(true); }
                else { SpawnBlankScroll(false); }
                AssignScroll(randomPool);
                break;
            case ItemPools.ItemScrollType.itemRoom:
                SpawnBlankScroll(false);
                AssignScroll(1);
                break;
            case ItemPools.ItemScrollType.shopRoom:
                SpawnBlankScroll(false);
                AssignScroll(0);
                break;
            case ItemPools.ItemScrollType.ritualRoom:
                SpawnBlankScroll(true);
                AssignScroll(2);
                break;
        }
        if (CheckIfItemScrollAlreadySpawned(scrollParameters)) 
        {
            Destroy(spawnedScroll);
            SpawnScroll(); 
        }
        else 
        {
            LocalGameManager.instance.spawnedScrolls.Add(scrollParameters);
            ScrollSettings(spawnedScroll); 
        }
    }

    public void SpawnBlankScroll(bool isCursed)
    {
        if (!isCursed) { spawnedScroll = Instantiate(MasterManager.itemPool.blankNormalScroll, transform.position, transform.rotation); }
        else { spawnedScroll = Instantiate(MasterManager.itemPool.blankCursedScroll, transform.position, transform.rotation); }
    }

    public void AssignScroll(int scrollType)
    {

    }

    public bool CheckIfItemScrollAlreadySpawned(Vector2Int scrollParameters)
    {
        if (LocalGameManager.instance.spawnedScrolls.Count > 0)
        {
            for (int i = 0; i < LocalGameManager.instance.spawnedScrolls.Count; i++)
            {
                if (LocalGameManager.instance.spawnedScrolls[i] == scrollParameters) { return true; }
            }
        }
        return false;
    }

    private int GetRandomItemPool()
    {
        return Random.Range(0, MasterManager.itemPool.itemScrolls.Count);
    }  

    private void ScrollSettings(GameObject newScroll)
    {

    }
    
    public void ShopScrollRespawn()
    {
        if (store.refillShop) { Invoke("SpawnScroll", 3); }
    }
}
