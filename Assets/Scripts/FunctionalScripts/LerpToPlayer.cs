using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToPlayer : MonoBehaviour
{
    private VRPlayerController _player;
    public float speed;

    private void Start()
    {
        _player = LocalGameManager.Instance.player;
    }

    private void LateUpdate()
    {
        float xPos = Mathf.Lerp(transform.position.x, _player.playerCollider.center.x, Time.deltaTime / speed);
        float yPos = Mathf.Lerp(transform.position.y, _player.playerCollider.center.y, Time.deltaTime / speed);
        float zPos = Mathf.Lerp(transform.position.z, _player.playerCollider.center.z, Time.deltaTime / speed);

        transform.position = new Vector3(xPos, yPos, zPos);
    }
}
