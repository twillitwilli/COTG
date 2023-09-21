using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.AbstractClasses;

public class ChestPool : MultipleObjectPoolSingleton<ChestPool>
{
    public override int ObjectRaritySelection()
    {
        int chestSelection = Random.Range(0, 100);

        if (chestSelection < 75) // Key Chest
            return 0;

        else if (chestSelection > 75 && chestSelection < 85) // Blood Chest
            return 1;

        else if (chestSelection > 85 && chestSelection < 95) // Gold Chest
            return 2;

        else // Free Chest
            return 3;
    }
}
