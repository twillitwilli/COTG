using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfPlayerHasKey : MonoBehaviour
{
    private void LateUpdate()
    {
        if (PlayerStats.Instance.data.currentKeys > 0)
            Destroy(gameObject);
    }
}
