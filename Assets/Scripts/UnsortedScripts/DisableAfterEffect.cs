using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterEffect : MonoBehaviour
{
    public void DisableGameobject()
    {
        gameObject.SetActive(false);
    }
}
