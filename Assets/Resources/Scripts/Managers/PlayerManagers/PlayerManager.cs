using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStuff/CreateNewPlayerManager")]
public class PlayerManager : ScriptableObject
{
    public GameObject playerPrefab;
    public GameObject mapInHand, mapCamera, rolledUpMap, wallet, keyDisplay, chat, chatKeyboard, bombCrystal, keyCrystal, bombCrystalForHand, keyCrystalForHand;
}
