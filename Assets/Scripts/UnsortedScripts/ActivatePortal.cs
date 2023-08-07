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
        if (!portalActivated && CoopManager.instance != null)
        {
            if (!LocalGameManager.instance.isHost && CoopManager.instance.portalActive)
            {
                EnableObjects(activatePortal, true);
                if (CoopManager.instance.closeOtherPortals)
                {
                    if (hardModePortal && LocalGameManager.instance.hardMode)
                    {
                        EnableObjects(otherPortals, false);
                    }
                    else if (!hardModePortal && !LocalGameManager.instance.hardMode)
                    {
                        EnableObjects(otherPortals, false);
                    }
                }
                portalActivated = true;
            }
        }
    }

    public void EnableObjects(GameObject[] objects, bool enable)
    {
        if (objects.Length > 0) { foreach (GameObject obj in objects) { obj.SetActive(enable); } }
    }
}
