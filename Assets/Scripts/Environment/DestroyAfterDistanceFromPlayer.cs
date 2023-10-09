using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDistanceFromPlayer : MonoBehaviour
{
    VRPlayer _player;

    public float distanceFromPlayer;

    void Start()
    {
        _player = LocalGameManager.Instance.player;
    }

    void LateUpdate()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);

        if (distance >= distanceFromPlayer)
            Destroy(gameObject);
    }
}
