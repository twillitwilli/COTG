using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.AbstractClasses;

public class RockPool : MultipleObjectPoolSingleton<RockPool>
{
    public override int ObjectRaritySelection()
    {
        int rockSelection = Random.Range(0, 100);

        if (rockSelection < 85) // Normal Rocks
            return 0;

        else if (rockSelection > 85 && rockSelection < 95) // Loot Rocks
            return 1;

        else // Dark Elemental Rocks
            return 2;
    }
}
