using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public enum ChestType { locked, free }
    [HideInInspector] public Animator chestAnimator;
    //spawnpoints -- 0 = itemPedastal, 1 = potion, <1 = smallDrops
    public List<Transform> spawnPoints;

    private void Awake()
    {
        transform.SetParent(null);
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        chestAnimator = GetComponent<Animator>();
        chestAnimator.SetBool("Open", false);
    }

    public void UnlockChest()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        chestAnimator.SetBool("Open", true);
        Invoke("PickLoot", 1);
    }

    public void PickLoot()
    {
        int lootSelection = Random.Range(0, 100);
        if (lootSelection < 75) { SpawnSmallLoot(); }
        else if (lootSelection > 75 && lootSelection < 95) { SpawnPotion(); }
        else { SpawnItemPedastal(); }
        if (LocalGameManager.instance.inDungeon)
        {
            LocalGameManager.instance.GetTotalStats().AdjustStat(PlayerTotalStats.StatType.chestsOpened, 0);
        }
    }

    public void SpawnSmallLoot()
    {
        int spawnItemCount = Random.Range(1, spawnPoints.Count);
        for (int i = 0; i < spawnItemCount; i++)
        {
            int randomSpawnPoint = Mathf.RoundToInt(Random.Range(2f, spawnPoints.Count));
            LocalGameManager.instance.SpawnRandomDrop(spawnPoints[randomSpawnPoint]);
            spawnPoints.Remove(spawnPoints[randomSpawnPoint]);
        }
        Destroy(gameObject);
    }

    public void SpawnPotion()
    {
        int randomPotion = Mathf.RoundToInt(Random.Range(0, MasterManager.itemPool.droppableItems.potions.Count));
        GameObject newPotion = Instantiate(MasterManager.itemPool.droppableItems.potions[randomPotion], spawnPoints[1]);
        ResetPositioning(newPotion);
        Destroy(gameObject);
    }

    public void SpawnItemPedastal()
    {
        GameObject newPedastal = Instantiate(MasterManager.itemPool.droppableItems.chestItemPedastal, spawnPoints[0]);
        ResetPositioning(newPedastal);
        Destroy(gameObject);
    }

    public void ResetPositioning(GameObject obj)
    {
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localEulerAngles = new Vector3(0, 0, 0);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.SetParent(null);
    }
}
