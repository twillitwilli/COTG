using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrabController : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private GearController _gearController;
    private MagicController _magicController;

    private VRPlayerHand _hand, _oppositeHand;
    private VRPlayerController _player;
    private GrabController _oppositeHandGrabController;
    private ClimbingController _climbController, _oppositeHandClimbController;

    private MapWalletSpawner _mapWalletController;
    private HandBombKeyController _bombKeyController;
    private PlayerPotionController _potionController;

    [HideInInspector] public GameObject currentGrabbableObj;

    public Transform grabbableSpawnLocation;
    //item index 0 = nothingToGrab, 1 = map, 2 = wallet, 3 = bomb, 4 = key, 5 = potion, 6 = staff, 7 = bow, 8 = bowString, 9 = rune, 10 = climbable, 11 = jar, 12 = classCard
    [SerializeField] private bool[] _holdingItem;

    [HideInInspector] public bool holdingIgnitedBomb;

    public TelekinesisRaycast telekinesisController;
    public LayerMask ignoreLayers;

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
        _gearController = _gameManager.GetGearController();
        _magicController = _gameManager.GetMagicController();

        _hand = GetComponent<VRPlayerHand>();
        _player = _hand.GetPlayer();
        _climbController = GetComponent<ClimbingController>();

        _oppositeHand = _hand.GetOppositeHand();
        _oppositeHandGrabController = _oppositeHand.GetGrabController();
        _oppositeHandClimbController = _oppositeHandGrabController.GetClimbingController();

        _mapWalletController = _player.GetPlayerComponents().mapWalletSpawner;
        _bombKeyController = _gearController.GetBombKeyController();
        _potionController = LocalGameManager.instance.GetPotionController();
    }

    public void UseGrabController(bool buttonDown)
    {
        Debug.Log("Used Grab Controller");
        if (buttonDown)
        {
            if (_player.toggleGrip && CheckIfHoldingAnything()) { ReleaseGrip(); }
            else if (!CheckIfHoldingAnything())
            {
                if (currentGrabbableObj == null && GetNearestGrabbable() != null) { currentGrabbableObj = GetNearestGrabbable(); }
                if (currentGrabbableObj != null)
                {
                    switch (currentGrabbableObj.GetComponent<PlayerItemGrabbable>().whichItem)
                    {
                        case PlayerItemGrabbable.PlayerItem.map:
                            _mapWalletController.GrabMap(this);
                            _holdingItem[1] = true;
                            break;

                        case PlayerItemGrabbable.PlayerItem.wallet:
                            _mapWalletController.GrabWallet(this);
                            _holdingItem[2] = true;
                            break;

                        case PlayerItemGrabbable.PlayerItem.bomb:
                            _bombKeyController.GrabBombCrystal(this);
                            _holdingItem[3] = true;
                            break;

                        case PlayerItemGrabbable.PlayerItem.key:
                            _bombKeyController.GrabKeyCrystal(this);
                            _holdingItem[4] = true;
                            break;

                        case PlayerItemGrabbable.PlayerItem.potion:
                            _potionController.GrabPotion(_hand);
                            _holdingItem[5] = true;
                            break;

                        case PlayerItemGrabbable.PlayerItem.staff:
                            _holdingItem[6] = true;
                            break;

                        case PlayerItemGrabbable.PlayerItem.bow:
                            _holdingItem[7] = true;
                            break;

                        case PlayerItemGrabbable.PlayerItem.rune:
                            _holdingItem[9] = true;
                            break;

                        case PlayerItemGrabbable.PlayerItem.climbable:
                            _climbController.GrabClimbable(false, currentGrabbableObj.transform);
                            _holdingItem[10] = true;
                            break;

                        case PlayerItemGrabbable.PlayerItem.jar:
                            _holdingItem[11] = true;
                            break;
                    }
                }
            }
        }
        else { if (!_player.toggleGrip && CheckIfHoldingAnything()) { ReleaseGrip(); } }
    }

    public void UseTriggerController(bool buttonDown)
    {
        //add telekinetic throwing here
        if (buttonDown)
        {
            if (currentGrabbableObj != null)
            {
                switch (currentGrabbableObj.GetComponent<PlayerItemGrabbable>().whichItem)
                {
                    case PlayerItemGrabbable.PlayerItem.bomb:
                        _bombKeyController.IgniteBomb(_hand);
                        break;

                    case PlayerItemGrabbable.PlayerItem.bowString:
                        if (_hand.IsPrimaryHand()) 
                        {
                            Conjurer.instance.GetBowController().GetBow().GrabString(); 
                        }

                        _holdingItem[8] = true;
                        break;

                    case PlayerItemGrabbable.PlayerItem.climbable:
                        _climbController.GrabClimbable(true, currentGrabbableObj.transform);
                        break;
                }
            }
            else
            {
                if (GetNearestGrabbable() != null)
                {
                    currentGrabbableObj = GetNearestGrabbable();
                    if (currentGrabbableObj == null) { return; }

                    switch (currentGrabbableObj.GetComponent<PlayerItemGrabbable>().whichItem)
                    {
                        case PlayerItemGrabbable.PlayerItem.bowString:
                            if (_hand.IsPrimaryHand())
                            {
                                Conjurer.instance.GetBowController().GetBow().GrabString();
                            }

                            _holdingItem[8] = true;
                            break;

                        case PlayerItemGrabbable.PlayerItem.climbable:
                            _climbController.GrabClimbable(true, currentGrabbableObj.transform);
                            break;
                    }
                }
            }
        }
        else
        {
            if (currentGrabbableObj != null)
            {
                switch (currentGrabbableObj.GetComponent<PlayerItemGrabbable>().whichItem)
                {
                    case PlayerItemGrabbable.PlayerItem.bomb:
                        _bombKeyController.ThrowBomb(_hand);
                        break;

                    case PlayerItemGrabbable.PlayerItem.bowString:
                        if (_hand.IsPrimaryHand())
                        {
                            Conjurer.instance.GetBowController().GetBow().GrabString();
                        }

                        _holdingItem[8] = true;
                        break;

                    case PlayerItemGrabbable.PlayerItem.climbable:
                        _climbController.GrabClimbable(true, currentGrabbableObj.transform);
                        break;
                }
            }
        }
    }

    public void ReleaseGrip()
    {
        switch (currentGrabbableObj.GetComponent<PlayerItemGrabbable>().whichItem)
        {
            case PlayerItemGrabbable.PlayerItem.map:
                _holdingItem[1] = false;
                _mapWalletController.ResetMap(this);
                break;

            case PlayerItemGrabbable.PlayerItem.wallet:
                _holdingItem[2] = false;
                _mapWalletController.ResetWallet(this);
                break;

            case PlayerItemGrabbable.PlayerItem.bomb:
                _holdingItem[3] = false;
                holdingIgnitedBomb = false;
                _bombKeyController.DropBombCrystal();
                break;

            case PlayerItemGrabbable.PlayerItem.key:
                _holdingItem[4] = false;
                _bombKeyController.DropKeyCrystal();
                break;

            case PlayerItemGrabbable.PlayerItem.potion:
                _holdingItem[5] = false;
                break;

            case PlayerItemGrabbable.PlayerItem.staff:
                _holdingItem[6] = false;
                break;

            case PlayerItemGrabbable.PlayerItem.bow:
                _holdingItem[7] = false;
                break;

            case PlayerItemGrabbable.PlayerItem.rune:
                _holdingItem[9] = false;
                break;

            case PlayerItemGrabbable.PlayerItem.climbable:
                _holdingItem[10] = false;
                break;

            case PlayerItemGrabbable.PlayerItem.jar:
                _holdingItem[11] = false;
                break;

            case PlayerItemGrabbable.PlayerItem.classCard:
                _holdingItem[12] = false;
                break;
        }
    }

    public GameObject GetNearestGrabbable()
    {
        Collider[] grabbableObjects = Physics.OverlapSphere(_hand.transform.position, 0.1f);
        GameObject nearest = null;
        float minDistance = float.MaxValue;
        float distance = 1;
        foreach (Collider grabbableObject in grabbableObjects)
        {
            if (grabbableObject.GetComponent<PlayerItemGrabbable>())
            {
                distance = Vector3.Distance(grabbableObject.transform.position, _hand.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = grabbableObject.gameObject;
                }
            }
        }

        if (nearest != null) { nearest.GetComponent<PlayerItemGrabbable>().currentHand = _hand; }

        return nearest;
    }

    public void ParentGrabbable(GameObject grabbableObj, Vector3 pos, Vector3 rot, Vector3 scale)
    {
        grabbableObj.transform.SetParent(grabbableSpawnLocation);
        grabbableObj.transform.localPosition = pos;
        grabbableObj.transform.localEulerAngles = rot;
        grabbableObj.transform.localScale = scale;
    }

    private bool CheckForWall() //not in use
    {
        RaycastHit hit;
        //"vector3.distance" returns a float value distance between 2 vector3 points ("to" point 1, "from" point 2)
        float range = Vector3.Distance(_player.head.transform.position, _hand.transform.position);
        //Raycast (origin of where the ray shoots from, direction the ray shoots (always "to - from" for the direction to be for specific point between to objects, "out hit" automatically sends it to the raycasthit hit variable, "range" is just the range of the ray, )
        if (Physics.Raycast(_hand.transform.position, _player.head.transform.position - _hand.transform.position, out hit, range, -ignoreLayers))
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Ground"))
            {
                return true;
                //Debug.Log("ray hit wall or ground");
            }
        }
        return false;
    }

    public void ClearAllGrabbableInfo()
    {
        GrabbableReset();
        _climbController.ClimbingReset();
    }

    public void GrabbableReset()
    {
        if (currentGrabbableObj != null)
        {
            currentGrabbableObj.GetComponent<PlayerItemGrabbable>().currentHand = null;
            currentGrabbableObj = null;
        }

        for (int i = 0; i < _holdingItem.Length; i++) { _holdingItem[i] = false; }

        telekinesisController.heldObject = null;
        telekinesisController.telekineticGrabEffect.SetActive(false);

        if (GetComponentInChildren<VRGrabbableObject>()) { Destroy(GetComponentInChildren<VRGrabbableObject>().gameObject); }
    }

    //item index 0 = nothingToGrab, 1 = map, 2 = wallet, 3 = bomb, 4 = key, 5 = potion, 6 = staff, 7 = bow, 8 = bowString, 9 = rune, 10 = climbable, 11 = jar
    public bool CheckIfHoldingAnything()
    {
        for (int i = 0; i < _holdingItem.Length; i++) { if (_holdingItem[i]) { return true; } }
        return false;
    }

    public bool CheckIfHoldingSpecificThing(int holdableIdx)
    {
        if (_holdingItem[holdableIdx]) { return true; }
        else return false;
    }

    public VRPlayerHand GetHand() { return _hand; }

    public ClimbingController GetClimbingController() { return _climbController; }
}
