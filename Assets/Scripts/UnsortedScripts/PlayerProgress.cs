using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProgress
{
    public int saveFile;
    public float timePlayed;
    public int totalDeaths, shopItemsBought, itemScrollsAbsorbed, chestsOpened, enemiesKilled, bossesKilled, golemsKilled;
}
