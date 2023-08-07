using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [HideInInspector] public List<GameObject> targets = new List<GameObject>();
    [HideInInspector] public int totalTargets;
    public bool destroyOnAllTargetsDestroyed;
    public List<GameObject> objToDestroy;

    public void TargetHit(GameObject targetHit)
    {
        targets.Remove(targetHit);
        totalTargets--;
        if (totalTargets <= 0)
        {
            if(destroyOnAllTargetsDestroyed) { foreach (GameObject obj in objToDestroy) { Destroy(obj); } }
        }
    }
}
