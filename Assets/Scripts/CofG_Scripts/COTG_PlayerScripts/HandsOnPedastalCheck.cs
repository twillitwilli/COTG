using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsOnPedastalCheck : MonoBehaviour
{
    private GetItem _getItem;

    private void Start()
    {
        _getItem = LocalGameManager.Instance.player.GetPlayerComponents().getItemRaycast;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pedastal")) { CheckForScroll(); }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pedastal")) { HandRemoved(); }
    }

    public void CheckForScroll()
    {
        _getItem.handCheck++;
        _getItem.CheckIfCanGetItem();
    }

    public void HandRemoved()
    {
        _getItem.handCheck--;
        if (_getItem.handCheck <= 0) { _getItem.handCheck = 0; }
    }
}
