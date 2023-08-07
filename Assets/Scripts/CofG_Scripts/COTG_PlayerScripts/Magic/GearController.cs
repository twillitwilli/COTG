using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    [Header("Class Gear")]
    [SerializeField] private StaffMagicController _staffController;
    [SerializeField] private BowMagicController _bowController;
    [SerializeField] private SpellCasting _spellCastingController;
    [SerializeField] private WandController _wandController;
    [SerializeField] private DaggerController _daggerController;

    public StaffMagicController GetStaffController() { return _staffController; }
    public BowMagicController GetBowController() { return _bowController; }
    public SpellCasting GetSpellCasting() { return _spellCastingController; }
    public WandController GetWandController() { return _wandController; }
    public DaggerController GetDaggerController() { return _daggerController; }

    [Header("Dungeon Gear")]
    [SerializeField] private MapController _mapController;
    [SerializeField] private CompassController _compassController;
    [SerializeField] private HandBombKeyController _bombKeyController;

    private bool _hasMap, _hasCompass, _hasEnemyHealthReveal, _hasPotionSight;

    public MapController GetMapController() { return _mapController; }
    public CompassController GetCompassController() { return _compassController; }
    public HandBombKeyController GetBombKeyController() { return _bombKeyController; }

    public bool HasMap() { return _hasMap; }
    public bool HasCompass() { return _hasCompass; }
    public bool HasHealthReveal() { return _hasEnemyHealthReveal; }
    public bool HasPotionSight() { return _hasPotionSight; }

    public void SwitchWeapon(MagicController.ClassType whichClass)
    {
        switch (whichClass)
        {
            case MagicController.ClassType.Wizard:
                break;

            case MagicController.ClassType.Conjurer:
                break;

            case MagicController.ClassType.Sorcerer:
                break;

            case MagicController.ClassType.Mage:
                break;

            case MagicController.ClassType.Enchanter:
                break;

            case MagicController.ClassType.Warlock:
                break;

            case MagicController.ClassType.Witch:
                break;

            case MagicController.ClassType.Tarot:
                break;

            default:
                break;
        }
    }

    private void DisableAllWeapons()
    {

    }

    public void ResetDungeonGear()
    {

    }
}
