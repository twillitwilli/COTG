using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemPools/LockedItems")]
public class LockedItems : ScriptableObject
{
    public List<ItemScrolls> itemScrolls;
    public List<GameObjectList> chests, potions;
}
