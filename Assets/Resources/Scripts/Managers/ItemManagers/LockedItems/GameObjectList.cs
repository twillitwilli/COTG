using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemPools/CreateGameobjectList")]
public class GameObjectList : ScriptableObject
{
    public List<GameObject> obj;
}
