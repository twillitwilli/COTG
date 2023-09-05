using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfPlayerHasKey : MonoBehaviour
{
    private void LateUpdate()
    {
        if (PlayerStats.Instance.GetCurrentKeys() > 0)
            Destroy(gameObject);
    }
}
