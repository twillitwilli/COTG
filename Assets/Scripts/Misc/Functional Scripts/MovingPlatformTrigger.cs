using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformTrigger : MonoBehaviour
{
    public string req = "Must be parent of Empty Gameobject with Moving Platform Trigger Script on it, set to 0,0,0 position and rotation, and 1,1,1 scale";

    public List<GameObject> objectsThatEnter { get; set; }

    MovingPlatformParentTo parentToScript;

    void Awake()
    {
        parentToScript = GetComponentInChildren<MovingPlatformParentTo>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<VRPlayer>())
            other.gameObject.transform.SetParent(parentToScript.transform);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<VRPlayer>())
        {
            other.gameObject.transform.SetParent(null);
            DontDestroyOnLoad(other.gameObject);
        }
    }

    public void Unparent()
    {
        parentToScript.UnparentObjects();
        ClearLists();
    }

    private void ClearLists()
    {
        objectsThatEnter.Clear();
    }
}
