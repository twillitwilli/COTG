using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    [HideInInspector] public VRPlayerController player;

    private PlayerTotalStats _totalStats;

    private void Start()
    {
        _totalStats = LocalGameManager.instance.GetTotalStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BreakableObject>())
        {
            BreakableObject breakableObject = other.gameObject.GetComponent<BreakableObject>();

            if (player != null)
            {
                switch (breakableObject.objectType)
                {
                    case BreakableObject.BreakableObjectType.jar:
                        _totalStats.AdjustStat(PlayerTotalStats.StatType.jarsBroken, 0);
                        break;

                    case BreakableObject.BreakableObjectType.rock:
                        _totalStats.AdjustStat(PlayerTotalStats.StatType.rocksBroken, 0);
                        break;
                }
                
            }
            breakableObject.BreakObjectWithBomb();
        }
    }
}
