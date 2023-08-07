using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemPools/ShopItems")]
public class ShopItems : ScriptableObject
{
    public List<GameObject> basicItems;
    public List<GameObject> potions;
}
