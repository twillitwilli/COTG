using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSignController : MonoBehaviour
{
    public GameObject signObj;

    private void Awake()
    {
        if (LocalGameManager.instance.inTutorial) { ActivateSign(); }
        else { signObj.SetActive(false); }
    }

    public void ActivateSign()
    {
        signObj.SetActive(true);
        signObj.transform.SetParent(null);
    }
}
