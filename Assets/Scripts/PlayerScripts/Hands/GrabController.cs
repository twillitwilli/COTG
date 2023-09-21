using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrabController : MonoBehaviour
{
    public Transform grabbableSpawnLocation;

    private VRPlayerController _player;
    private VRPlayerHand _hand;
    private ClimbingController _climbController;

    [SerializeField]
    private VRPlayerHand _oppositeHand;

    [SerializeField]
    private GrabController _oppositeHandGrabController;

    [SerializeField]
    private ClimbingController _oppositeHandClimbController;

    [SerializeField]
    private MenuRaycast _menuRaycast;

    [SerializeField]
    private HandAnimationState _handAnimations;

    public ItemPoolManager.GrabbableItem currentObjectGrabbed { get; private set; }
    private GameObject _currentGrabbedObj;

    [HideInInspector] 
    public bool holdingIgnitedBomb;

    public TelekinesisRaycast telekinesisController;
    public LayerMask ignoreLayers;

    private void Awake()
    {
        _player = LocalGameManager.Instance.player;
        _hand = GetComponent<VRPlayerHand>();
        _climbController = GetComponent<ClimbingController>();
    }

    public void UseGrabController(bool buttonDown)
    {
        Debug.Log("Used Grab Controller");
        if (buttonDown)
        {
            if (_player.toggleGrip && currentObjectGrabbed != ItemPoolManager.GrabbableItem.nothing)
                ReleaseGrip();

            else if (currentObjectGrabbed == ItemPoolManager.GrabbableItem.nothing)
            {
                _currentGrabbedObj = GetNearestGrabbable();

                if (_currentGrabbedObj == null)
                    return;

                currentObjectGrabbed = _currentGrabbedObj.GetComponent<PlayerItemGrabbable>().grabbableItem;

                switch (currentObjectGrabbed)
                {
                    case ItemPoolManager.GrabbableItem.map:
                        MapController.Instance.GrabMap(this);
                        _handAnimations.SwitchHandState(HandAnimationState.HandState.holdingMap);
                        break;

                    case ItemPoolManager.GrabbableItem.wallet:
                        WalletController.Instance.GrabWallet(this);
                        _handAnimations.SwitchHandState(HandAnimationState.HandState.holdingWallet);
                        break;

                    case ItemPoolManager.GrabbableItem.bomb:
                        CrystalController.Instance.GrabBombCrystal(this);
                        _handAnimations.SwitchHandState(HandAnimationState.HandState.holdingBombCrystal);
                        break;

                    case ItemPoolManager.GrabbableItem.key:
                        CrystalController.Instance.GrabKeyCrystal(this);
                        break;

                    case ItemPoolManager.GrabbableItem.potion:
                        PlayerPotionController.Instance.GrabPotion(GetHand(), _currentGrabbedObj);
                        break;

                    case ItemPoolManager.GrabbableItem.staff:
                        break;

                    case ItemPoolManager.GrabbableItem.bow:
                        break;

                    case ItemPoolManager.GrabbableItem.rune:
                        break;

                    case ItemPoolManager.GrabbableItem.climbable:
                        _climbController.GrabClimbable(false, _currentGrabbedObj.transform);
                        break;

                    case ItemPoolManager.GrabbableItem.jar:
                        break;
                }
            }
        }

        else
        {
            if (!_player.toggleGrip && currentObjectGrabbed != ItemPoolManager.GrabbableItem.nothing)
                ReleaseGrip();
        }
    }

    public void UseTriggerController(bool buttonDown)
    {
        TriggerGrabController(buttonDown);

        if (currentObjectGrabbed != ItemPoolManager.GrabbableItem.nothing)
            return;

        else if (currentObjectGrabbed == ItemPoolManager.GrabbableItem.nothing && _menuRaycast.MenuRayActive())
            _menuRaycast.ShootRaycast();
    }

    private void TriggerGrabController(bool buttonDown)
    {
        //add telekinetic throwing here
        if (buttonDown)
        {
            if (currentObjectGrabbed != ItemPoolManager.GrabbableItem.nothing)
            {
                switch (currentObjectGrabbed)
                {
                    case ItemPoolManager.GrabbableItem.bomb:
                        CrystalController.Instance.IgniteBomb(this);
                        currentObjectGrabbed = ItemPoolManager.GrabbableItem.ignitedBomb;
                        _handAnimations.SwitchHandState(HandAnimationState.HandState.holdingArcaneBomb);
                        break;

                    case ItemPoolManager.GrabbableItem.bowString:
                        if (_hand.IsPrimaryHand())
                        {
                            Conjurer.instance.GetBowController().GetBow().GrabString();
                        }

                        break;

                    case ItemPoolManager.GrabbableItem.climbable:
                        _climbController.GrabClimbable(true, _currentGrabbedObj.transform);
                        break;
                }
            }

            else
            {
                if (GetNearestGrabbable() != null)
                {
                    _currentGrabbedObj = GetNearestGrabbable();
                    if (_currentGrabbedObj == null)
                        return;

                    currentObjectGrabbed = _currentGrabbedObj.GetComponent<PlayerItemGrabbable>().grabbableItem;

                    switch (currentObjectGrabbed)
                    {
                        case ItemPoolManager.GrabbableItem.bowString:
                            if (_hand.IsPrimaryHand())
                            {
                                Conjurer.instance.GetBowController().GetBow().GrabString();
                            }
                            break;

                        case ItemPoolManager.GrabbableItem.climbable:
                            _climbController.GrabClimbable(true, _currentGrabbedObj.transform);
                            break;
                    }
                }
            }
        }

        else
        {
            if (_currentGrabbedObj == null)
                return;

            switch (currentObjectGrabbed)
            {
                case ItemPoolManager.GrabbableItem.bomb:
                    CrystalController.Instance.ThrowBomb(GetHand());
                    break;

                case ItemPoolManager.GrabbableItem.bowString:
                    if (_hand.IsPrimaryHand())
                        Conjurer.instance.GetBowController().GetBow().GrabString();
                    break;

                case ItemPoolManager.GrabbableItem.climbable:
                    _climbController.GrabClimbable(true, _currentGrabbedObj.transform);
                    break;
            }
        }
    }

    public void ReleaseGrip()
    {
        if (currentObjectGrabbed != ItemPoolManager.GrabbableItem.nothing)
        {
            switch (currentObjectGrabbed)
            {
                case ItemPoolManager.GrabbableItem.map:
                    MapController.Instance.ResetMap(this);
                    break;

                case ItemPoolManager.GrabbableItem.wallet:
                    WalletController.Instance.ResetWallet(this);
                    break;

                case ItemPoolManager.GrabbableItem.bomb:
                    CrystalController.Instance.DropBombCrystal();
                    break;

                case ItemPoolManager.GrabbableItem.ignitedBomb:
                    CrystalController.Instance.ThrowBomb(GetHand());
                    break;

                case ItemPoolManager.GrabbableItem.key:
                    CrystalController.Instance.SpawnKeyCrystalOnHand();
                    break;

                case ItemPoolManager.GrabbableItem.potion:
                    break;

                case ItemPoolManager.GrabbableItem.staff:
                    break;

                case ItemPoolManager.GrabbableItem.bow:
                    break;

                case ItemPoolManager.GrabbableItem.rune:
                    break;

                case ItemPoolManager.GrabbableItem.climbable:
                    break;

                case ItemPoolManager.GrabbableItem.jar:
                    break;

                case ItemPoolManager.GrabbableItem.classCard:
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
            PlayerItemGrabbable newItem;

            if (grabbableObject.TryGetComponent(out newItem))
            {
                // Ignore Objects
                bool ignoreBomb = newItem.grabbableItem == ItemPoolManager.GrabbableItem.bomb && !GetHand().IsPrimaryHand() ? true : false;
                bool ignoreKey = newItem.grabbableItem == ItemPoolManager.GrabbableItem.key && GetHand().IsPrimaryHand() ? true : false;

                if (!ignoreBomb || !ignoreKey)
                {
                    distance = Vector3.Distance(grabbableObject.transform.position, _hand.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearest = grabbableObject.gameObject;
                    }
                }
            }
        }

        if (nearest != null)
            nearest.GetComponent<PlayerItemGrabbable>().currentHand = _hand;

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
        currentObjectGrabbed = ItemPoolManager.GrabbableItem.nothing;

        if (_currentGrabbedObj != null)
            _currentGrabbedObj = null;

        _handAnimations.SwitchHandState(HandAnimationState.HandState.idle);

        _climbController.ClimbingReset();
    }

    public bool CheckIfHoldingAnything()
    {
        bool holdingItem = currentObjectGrabbed != ItemPoolManager.GrabbableItem.nothing ? true : false;

        return holdingItem;
    }

    public VRPlayerHand GetHand() { return _hand; }

    public ClimbingController GetClimbingController() { return _climbController; }

    public GrabController GetOppositeGrabController() { return GetHand().GetOppositeHand().GetGrabController(); }
}
