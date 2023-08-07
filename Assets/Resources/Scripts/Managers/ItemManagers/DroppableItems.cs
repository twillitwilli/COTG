using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemPools/DroppableItems")]
public class DroppableItems : ScriptableObject
{
    public GameObject dropTemplate;
    public GameObject chestItemPedastal;
    public List<GameObject> chests;
    public List<GameObject> droppableItem;
    public List<GameObject> potions;

    [HideInInspector]
    public enum itemType { chest, gold, arcaneOrb, key, health, ritualRune }
}
