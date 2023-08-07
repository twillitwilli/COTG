using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfPlayerHasKey : MonoBehaviour
{
    private void LateUpdate()
    {
        if (LocalGameManager.instance.GetPlayerStats().GetCurrentKeys() > 0) { Destroy(gameObject); }
    }
}
