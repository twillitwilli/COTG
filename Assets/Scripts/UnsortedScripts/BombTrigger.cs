using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    [HideInInspector] public VRPlayerController player;

    private PlayerTotalStats _totalStats;

    private void Start()
    {
        _totalStats = LocalGameManager.Instance.GetTotalStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BreakableObject>())
        {
            BreakableObject breakableObject = other.gameObject.GetComponent<BreakableObject>();

            if (player != null)
            {
                switch (LocalGameManager.Instance.currentGameMode)
                {
                    case LocalGameManager.GameMode.master | LocalGameManager.GameMode.normal:
                        switch (breakableObject.objectType)
                        {
                            case BreakableObject.BreakableObjectType.jar:
                                _totalStats.AdjustStats(PlayerTotalStats.StatType.jarsBroken);
                                break;

                            case BreakableObject.BreakableObjectType.rock:
                                _totalStats.AdjustStats(PlayerTotalStats.StatType.rocksBroken);
                                break;
                        }
                        break;
                }
            }
            breakableObject.BreakObjectWithBomb();
        }
    }
}
