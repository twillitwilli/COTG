using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.AbstractClasses;

public class FollowerPetController : MonoSingleton<FollowerPetController>
{
    Transform _petSpawn;
    GameObject _currentPet;
    PlayerPetController _petController;

    public void SpawnPet(VRPlayer player)
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
