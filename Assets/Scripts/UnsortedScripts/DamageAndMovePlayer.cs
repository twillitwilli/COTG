using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAndMovePlayer : MonoBehaviour
{
    public string objectName;
    public float damageAmount;

    private LocalGameManager _gameManager;

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        VRPlayerController player;
        if (other.gameObject.TryGetComponent<VRPlayerController>(out player))
        {
            _gameManager.GetPlayerStats().AdjustHealth(-damageAmount, objectName);
            player.transform.position = _gameManager.GetSpawnLocations()[3].position;
            player.transform.rotation = _gameManager.GetSpawnLocations()[3].rotation;
        }
    }
}
