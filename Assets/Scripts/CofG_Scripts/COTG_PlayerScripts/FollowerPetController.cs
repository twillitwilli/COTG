using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerPetController : MonoBehaviour
{
    private Transform _petSpawn;
    private GameObject _currentPet;
    private PlayerPetController _petController;

    public void SpawnPet(VRPlayerController player)
    {
        if (_currentPet == null)
        {
            _petSpawn = player.GetPlayerComponents().petSpawnLocation;
            _currentPet = Instantiate(MasterManager.pet.pets[0], _petSpawn.position, _petSpawn.rotation);
            _currentPet.transform.SetParent(_petSpawn);
            _petController = _currentPet.GetComponent<PlayerPetController>();
        }
    }

    public PlayerPetController GetPet() { return _petController; }
}
