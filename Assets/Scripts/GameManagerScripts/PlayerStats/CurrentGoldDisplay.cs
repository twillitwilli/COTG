using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentGoldDisplay : MonoBehaviour
{
    [SerializeField] 
    private Text displayGold;

    public void UpdateDisplay(int goldAmount)
    {
        displayGold.text = goldAmount.ToString();
    }
}
