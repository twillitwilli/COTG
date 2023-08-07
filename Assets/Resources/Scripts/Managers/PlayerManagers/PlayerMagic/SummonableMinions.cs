using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStuff/CreateNewMinionList")]
public class SummonableMinions : ScriptableObject
{
    public GameObject[] minion;
}
