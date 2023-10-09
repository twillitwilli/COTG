using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.AbstractClasses;

public class Wizard : MonoSingleton<Wizard>
{
    [SerializeField] private StaffMagicController _staffController;

    public StaffMagicController GetStaffController() { return _staffController; }
}
