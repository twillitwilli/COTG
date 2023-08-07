using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public enum WeaponType { staff, bow, hands, wand, sword, gems, book }
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private GameObject[] weapon;

    private GameObject _currentWeapon;
}
