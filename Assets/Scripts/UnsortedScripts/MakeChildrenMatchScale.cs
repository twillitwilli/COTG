using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeChildrenMatchScale : MonoBehaviour
{
    public GameObject[] childrenObjects;

    public void Start()
    {
        if (childrenObjects.Length > 0)
        {
            Invoke("MatchLocalScale", 0.1f);
        }
    }

    public void MatchLocalScale() // for particle scaling
    {
        foreach (GameObject obj in childrenObjects)
        {
            obj.transform.localScale = transform.localScale;
        }
    }
}
