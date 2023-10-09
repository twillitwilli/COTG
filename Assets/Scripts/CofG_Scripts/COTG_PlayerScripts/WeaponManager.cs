using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public enum WeaponType 
    { 
        staff, 
        bow, 
        hands, 
        wand, 
        sword, 
        gems, 
        book 
    }

    [SerializeField] 
    WeaponType _weaponType;

    [SerializeField] 
    GameObject[] weapon;

    GameObject _currentWeapon;
}
