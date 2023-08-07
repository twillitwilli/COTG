using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunParentTriggerOnEnable : MonoBehaviour
{
    public ParentTrigger parentTriggerScript;

    private void OnEnable()
    {
        parentTriggerScript.CastOverlapBox();
    }
}
