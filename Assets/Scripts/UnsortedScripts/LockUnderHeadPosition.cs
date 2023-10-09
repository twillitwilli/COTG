using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockUnderHeadPosition : MonoBehaviour
{
    [SerializeField]
    VRPlayer player;

    private void LateUpdate()
    {
        transform.localPosition = new Vector3(player.head.localPosition.x, transform.localPosition.y, player.head.localPosition.z);
    }
}
