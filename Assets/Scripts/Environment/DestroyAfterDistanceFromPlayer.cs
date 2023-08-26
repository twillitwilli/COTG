using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDistanceFromPlayer : MonoBehaviour
{
    private VRPlayerController _player;

    public float distanceFromPlayer;

    private void Start()
    {
        _player = LocalGameManager.Instance.player;
    }

    private void LateUpdate()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        if (distance >= distanceFromPlayer) { Destroy(gameObject); }
    }
}
