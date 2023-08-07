using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyPools/CreateNewBossList")]
public class BossList : ScriptableObject
{
    public List<GameObject> bosses;
}
