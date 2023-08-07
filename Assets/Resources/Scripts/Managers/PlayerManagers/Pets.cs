using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStuff/CreateNewPetList")]
public class Pets : ScriptableObject
{
    public List<GameObject> pets;
}
