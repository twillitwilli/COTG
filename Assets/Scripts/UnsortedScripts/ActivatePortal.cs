using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePortal : MonoBehaviour
{
    public bool hardModePortal;
    public GameObject[] activatePortal;
    public GameObject[] otherPortals;
    private bool portalActivated;

    public void LateUpdate()
    {
        if (!portalActivated && CoopManager.instance != null && !LocalGameManager.Instance.isHost && CoopManager.instance.portalActive)
        {
            EnableObjects(activatePortal, true);
            if (CoopManager.instance.closeOtherPortals)
            {
                switch (LocalGameManager.Instance.currentGameMode)
                {
                    case LocalGameManager.GameMode.master:
                        if (hardModePortal)
                        {
                            EnableObjects(otherPortals, false);
                        }
                        break;

                    default:
                        if (!hardModePortal)
                        {
                            EnableObjects(otherPortals, false);
                        }
                        break;
                }
            }
            portalActivated = true;
        }
    }

    public void EnableObjects(GameObject[] objects, bool enable)
    {
        if (objects.Length > 0) { foreach (GameObject obj in objects) { obj.SetActive(enable); } }
    }
}
