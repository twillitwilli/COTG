using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public static Wizard instance;
    [SerializeField] private StaffMagicController _staffController;

    private void Start()
    {
        if (!instance) { instance = this; }
        else { Destroy(gameObject); }
    }

    public StaffMagicController GetStaffController() { return _staffController; }

    private void OnDestroy()
    {
        if (instance == this) { instance = null; }
    }
}
