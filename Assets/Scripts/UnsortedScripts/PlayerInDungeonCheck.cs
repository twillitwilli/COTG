using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInDungeonCheck : MonoBehaviour
{
    public void Start()
    {
        if (!LocalGameManager.instance.inDungeon) { gameObject.SetActive(false); }
    }
}
