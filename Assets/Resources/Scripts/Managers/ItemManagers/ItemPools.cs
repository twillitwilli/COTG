using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemPools/NewItemManager")]
public class ItemPools : ScriptableObject
{
    public DroppableItems droppableItems;
    public ShopItems shopItems;
    public enum ItemScrollType { random, itemRoom, shopRoom, ritualRoom }
    public GameObject blankNormalScroll, blankCursedScroll;
    public List<ItemScrolls> itemScrolls;
    public LockedItems lockedItems;
}
