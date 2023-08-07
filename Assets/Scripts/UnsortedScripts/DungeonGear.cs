using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGear : MonoBehaviour
{
    public bool hasMap, hasCompass, hasEnemyHealthReveal, hasPotionSight;

    public void GotCompass()
    {
        DungeonBuildParent.instance.GetCompassController().CompassReveal();
    }

    public void GotMapReveal()
    {
        DungeonBuildParent.instance.GetMapController().RevealMap();
    }

    public void RemoveGear()
    {
        hasMap = false;
        hasCompass = false;
        hasEnemyHealthReveal = false;
        hasPotionSight = false;
    }
}
