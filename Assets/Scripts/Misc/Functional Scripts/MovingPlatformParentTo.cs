using UnityEngine;

public class MovingPlatformParentTo : MonoBehaviour
{
    public string req = "Must be child of Gameobject with Moving Platform Trigger Script which needs to have a collider with isTrigger on";

    private MovingPlatformTrigger platformTrigger;

    private void Awake()
    {
        platformTrigger = GetComponentInParent<MovingPlatformTrigger>();
    }

    public void ParentObject()
    {
        foreach (GameObject Grabbables in platformTrigger.objectsThatEnter)
        {
            Grabbables.transform.SetParent(transform);
        }
    }

    public void UnparentObjects()
    {
        foreach (GameObject Grabbables in platformTrigger.objectsThatEnter)
        {
            Grabbables.transform.SetParent(null);
        }
    }
}
