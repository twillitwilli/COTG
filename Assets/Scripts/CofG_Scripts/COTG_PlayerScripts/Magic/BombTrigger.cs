using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BreakableObject>())
        {
            BreakableObject breakableObject = other.gameObject.GetComponent<BreakableObject>();

            switch (LocalGameManager.Instance.currentGameMode)
            {
                case LocalGameManager.GameMode.master | LocalGameManager.GameMode.normal:
                    switch (breakableObject.objectType)
                    {
                        case BreakableObject.BreakableObjectType.jar:
                            PlayerTotalStats.Instance.AdjustStats(PlayerTotalStats.StatType.jarsBroken);
                            break;

                        case BreakableObject.BreakableObjectType.rock:
                            PlayerTotalStats.Instance.AdjustStats(PlayerTotalStats.StatType.rocksBroken);
                            break;
                    }
                    break;
            }

            breakableObject.BreakObjectWithBomb();
        }
    }
}
