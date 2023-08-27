using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrabController : MonoBehaviour
{
    public Transform grabbableSpawnLocation;

    [SerializeField]
    private VRPlayerHand _hand, _oppositeHand;

    [SerializeField]
    private VRPlayerController _player;

    [SerializeField]
    private GrabController _oppositeHandGrabController;

    [SerializeField]
    private ClimbingController _climbController, _oppositeHandClimbController;

    [SerializeField]
    private MenuRaycast _menuRaycast;

    [SerializeField]
    private HandAnimationState _handAnimations;

    public PlayerItemGrabbable.PlayerItem currentObjectGrabbed { get; private set; }

    [HideInInspector] 
    public bool holdingIgnitedBomb;

    public TelekinesisRaycast telekinesisController;
    public LayerMask ignoreLayers;

    public void UseGrabController(bool buttonDown)
    {
        Debug.Log("Used Grab Controller");
        if (buttonDown)
        {
            if (_player.toggleGrip && currentObjectGrabbed != PlayerItemGrabbable.PlayerItem.nothing)
                ReleaseGrip();

            else if (currentObjectGrabbed == PlayerItemGrabbable.PlayerItem.nothing)
            {
                GameObject newObject = GetNearestGrabbable();
                if (newObject != null)
                {
                    currentObjectGrabbed = newObject.GetComponent<PlayerItemGrabbable>().whichItem;

                    switch (currentObjectGrabbed)
                    {
                        case PlayerItemGrabbable.PlayerItem.map:
                            MapController.Instance.GrabMap(this);
                            _handAnimations.SwitchHandState(HandAnimationState.HandState.holdingMap);
                            break;

                        case PlayerItemGrabbable.PlayerItem.wallet:
                            WalletController.Instance.GrabWallet(this);
                            _handAnimations.SwitchHandState(HandAnimationState.HandState.holdingWallet);
                            break;

                        case PlayerItemGrabbable.PlayerItem.bomb:
                            CrystalController.Instance.GrabBombCrystal(this);
                            _handAnimations.SwitchHandState(HandAnimationState.HandState.holdingBombCrystal);
                            break;

                        case PlayerItemGrabbable.PlayerItem.key:
                            _bombKeyController.GrabKeyCrystal(this);
                            break;

                        case PlayerItemGrabbable.PlayerItem.potion:
                            _potionController.GrabPotion(_hand);
                            break;

                        case PlayerItemGrabbable.PlayerItem.staff:
                            break;

                        case PlayerItemGrabbable.PlayerItem.bow:
                            break;

                        case PlayerItemGrabbable.PlayerItem.rune:
                            break;

                        case PlayerItemGrabbable.PlayerItem.climbable:
                            _climbController.GrabClimbable(false, currentGrabbableObj.transform);
                            break;

                        case PlayerItemGrabbable.PlayerItem.jar:
                            break;
                    }
                }
            }
        }

        else
        {
            if (!_player.toggleGrip && currentObjectGrabbed != PlayerItemGrabbable.PlayerItem.nothing)
                ReleaseGrip();
        }
    }

    public void UseTriggerController(bool buttonDown)
    {
        TriggerGrabController(buttonDown);

        if (currentObjectGrabbed != PlayerItemGrabbable.PlayerItem.nothing)
            return;

        else if (currentObjectGrabbed == PlayerItemGrabbable.PlayerItem.nothing && _menuRaycast.MenuRayActive())
            _menuRaycast.ShootRaycast();
    }

    private void TriggerGrabController(bool buttonDown)
    {
        //add telekinetic throwing here
        if (buttonDown)
        {
            if (currentObjectGrabbed != PlayerItemGrabbable.PlayerItem.nothing)
            {
                switch (currentObjectGrabbed)
                {
                    case PlayerItemGrabbable.PlayerItem.bomb:
                        CrystalController.Instance.IgniteBomb(this);
                        currentObjectGrabbed = PlayerItemGrabbable.PlayerItem.ignitedBomb;
                        _handAnimations.SwitchHandState(HandAnimationState.HandState.holdingArcaneBomb);
                        break;

                    case PlayerItemGrabbable.PlayerItem.bowString:
                        if (_hand.IsPrimaryHand())
                        {
                            Conjurer.instance.GetBowController().GetBow().GrabString();
                        }

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
        if (currentObjectGrabbed != PlayerItemGrabbable.PlayerItem.nothing)
        {
            switch (currentObjectGrabbed)
            {
                case PlayerItemGrabbable.PlayerItem.map:
                    MapController.Instance.ResetMap(this);
                    break;

                case PlayerItemGrabbable.PlayerItem.wallet:
                    WalletController.Instance.ResetWallet(this);
                    break;

                case PlayerItemGrabbable.PlayerItem.bomb:
                    CrystalController.Instance.DropBombCrystal();
                    break;

                case PlayerItemGrabbable.PlayerItem.ignitedBomb:
                    CrystalController.Instance.ThrowBomb(GetHand());
                    break;

                case PlayerItemGrabbable.PlayerItem.key:
                    _bombKeyController.DropKeyCrystal();
                    break;

                case PlayerItemGrabbable.PlayerItem.potion:
                    break;

                case PlayerItemGrabbable.PlayerItem.staff:
                    break;

                case PlayerItemGrabbable.PlayerItem.bow:
                    break;

                case PlayerItemGrabbable.PlayerItem.rune:
                    break;

                case PlayerItemGrabbable.PlayerItem.climbable:
                    break;

                case PlayerItemGrabbable.PlayerItem.jar:
                    break;

                case PlayerItemGrabbable.PlayerItem.classCard:
                    break;
            }
        }

        _handAnimations.SwitchHandState(HandAnimationState.HandState.idle);
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
        bool holdingItem = currentObjectGrabbed != PlayerItemGrabbable.PlayerItem.nothing ? true : false;

        return holdingItem;
    }

    public VRPlayerHand GetHand() { return _hand; }

    public ClimbingController GetClimbingController() { return _climbController; }

    public GrabController GetOppositeGrabController() { return GetHand().GetOppositeHand().GetGrabController(); }
}
