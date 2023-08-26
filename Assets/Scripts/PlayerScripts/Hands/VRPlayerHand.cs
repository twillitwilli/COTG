using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class VRPlayerHand : MonoBehaviour
{
    [SerializeField] private bool _isRightHand;

    private GrabController _grabController;
    private ClimbingController _climbingController;
    private SpellCastingForHands _spellCasting;
    
    [SerializeField] private VRPlayerController _player;
    [SerializeField] private VRPlayerHand _oppositeHand;
    [SerializeField] private SkinnedMeshRenderer _handRenderer;
    [SerializeField] private MenuRaycast _menuRaycast;
    [SerializeField] private TelekinesisRaycast _telekinesisController;

    [SerializeField] private Transform _menuSpawnLocation;

    [SerializeField] private HandAnimationState _handAnimator;
    private HandAnimationState.HandState _currentHandState;

    [SerializeField] private GameObject _handModel;

    public Vector3 defaultHandPos, defaultHandRot;
    public Transform itemFixedToHand;

    //hand physics
    private Vector3 _previousHeldPos;
    private Quaternion _previousAngularRot;
    private Vector3 _previousAcceleration;
    private Vector3 _handVel, _handAngVel, _handAccel;

    //Automatically Adjusted Objects
    private bool _isPrimaryHand;
    private Rigidbody _rb;

    private LocalGameManager _gameManager;
    private PlayerStats _playerStats;
    private MagicController _magicController;
    private PlayerComponents _playerComponents;
    private ControllerType _controllerType;

    //chat
    public Transform chatSpawn;
    [HideInInspector] public ChatDisplay chatDisplay;

    private void Awake()
    {
        chatDisplay = chatSpawn.gameObject.GetComponent<ChatDisplay>();

        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
        _rb.useGravity = false;

        _grabController = GetComponent<GrabController>();
        _climbingController = GetComponent<ClimbingController>();
        _spellCasting = GetComponent<SpellCastingForHands>();
    }

    private void Start()
    {
        _gameManager = LocalGameManager.Instance;
        _magicController = _gameManager.GetMagicController();
        _playerComponents = _gameManager.player.GetPlayerComponents();
        _controllerType = _gameManager.GetControllerType();

        CheckHandModelDistance();
        SetPrimaryHand();

        _currentHandState = _handAnimator.GetCurrentHandState();
        Invoke("HandIdleState", 1);
    }

    private void LateUpdate()
    {
        if (!_player.isGhost && !_player.menuSpawned && !_player.playerCalibrationOn && !_player.playerHandAdjusterOn)
        {
            CurrentHandPhysicsMotion();
            if (_climbingController.IsClimbing()) { _climbingController.Climbing(); }
            else
            {
                switch (_magicController.currentClass)
                {
                    case MagicController.ClassType.Sorcerer:
                        if (!_grabController.CheckIfHoldingAnything() && _spellCasting.magicActive && _spellCasting.spellReadyVisual) { _spellCasting.SorcererMagic(); }
                        break;
                }
            }
            if (!_grabController.CheckIfHoldingAnything() && !_menuRaycast.RayActive() && !_telekinesisController.gameObject.activeSelf)
            {
                { 
                    _telekinesisController.gameObject.SetActive(true);
                    _handAnimator.SwitchHandState(HandAnimationState.HandState.fingerPoint);
                }
            }
            PreviousHandPhysicsMotion();
        }
        if (_menuRaycast.RayActive() && _currentHandState != HandAnimationState.HandState.fingerPoint)
        {
            _handAnimator.SwitchHandState(HandAnimationState.HandState.fingerPoint);
        }
        //if (grabbableScript) { Debug.DrawRay(grabbableScript.transform.position, VRPlayerController.instance.head.transform.position - grabbableScript.transform.position, Color.green); }
    }

    public void SetPrimaryHand()
    {
        if (_player.isLeftHanded && !_isRightHand) { _isPrimaryHand = true; }
        else if (_player.isLeftHanded && _isRightHand) { _isPrimaryHand = false; }
        else if (!_player.isLeftHanded && !_isRightHand) { _isPrimaryHand = false; }
        else if (!_player.isLeftHanded && _isRightHand) { _isPrimaryHand = true; }
    }

    // HAND PHYSICS
    private void CurrentHandPhysicsMotion()
    {
        _handAccel = GetAcceleration(GetVelocity(transform.localPosition, _previousHeldPos), _handVel);
        _handVel = GetVelocity(transform.localPosition, _previousHeldPos);
        _handAngVel = GetAngularVelocity(transform.rotation, _previousAngularRot);
    }

    private void PreviousHandPhysicsMotion()
    {
        _previousAcceleration = _handAccel;
        _previousHeldPos = transform.localPosition;
        _previousAngularRot = transform.rotation;
    }

    public Vector3 GetVelocity(Vector3 newPos, Vector3 oldPos)
    {
        Vector3 velocity = (newPos - oldPos) / Time.deltaTime;
        return velocity;
    }

    public Vector3 GetAngularVelocity(Quaternion newRot, Quaternion oldRot)
    {
        Quaternion deltaRotation = newRot * Quaternion.Inverse(oldRot);
        deltaRotation.ToAngleAxis(out var angle, out var axis);
        angle *= Mathf.Deg2Rad;
        Vector3 angularVelocity = (1.0f / Time.deltaTime) * angle * axis;
        return angularVelocity;
    }

    public Vector3 GetAcceleration(Vector3 newVel, Vector3 oldVel)
    {
        Vector3 acceleration = (newVel - oldVel);
        return acceleration;
    }

    public void ApplyHandVelocity(Rigidbody rb, bool isTelekineticThrow)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        if (!isTelekineticThrow)
        {
            rb.velocity = (Quaternion.AngleAxis(_player.transform.localEulerAngles.y, Vector3.up) * _handVel * _playerStats.GetThrowingForce());
            rb.angularVelocity = _handAngVel;
        }
        else
        {
            rb.velocity = (Quaternion.AngleAxis(_player.transform.localEulerAngles.y, Vector3.up)) * _handVel * _playerStats.GetThrowingForce() * 4;
        }
    }

    public void HandIdleState()
    {
        _handAnimator.SwitchHandState(HandAnimationState.HandState.idle);
    }

    // BUTTON CONTROLS

    public void UseGrabController(bool buttonDown)
    {
        _grabController.UseGrabController(buttonDown);
    }

    public void UseTriggerController(bool buttonDown)
    {
        _grabController.UseTriggerController(buttonDown);
    }


    // HAND SETTINGS & DEFAULT SETTINGS

    public void CheckHandModelDistance()
    {
        if (Vector3.Distance(transform.position, _handModel.transform.position) > 0.5f)
        {
            if (!_player.hasCustomHandSettings)
            {
                _controllerType.ResetHandToControllerDefault(this);
            }
            else
            {
                LoadHandPosition();
            }

            _handModel.SetActive(true);
        }
    }

    public void ResetHandAlignment()
    {
        _handModel.transform.SetParent(transform);
        _handModel.transform.localPosition = defaultHandPos;
        _handModel.transform.localEulerAngles = defaultHandRot;
        SaveHandPosition();
    }

    public void SaveHandPosition()
    {
        defaultHandPos = _handModel.transform.localPosition;
        defaultHandRot = _handModel.transform.localEulerAngles;
        if (_isRightHand)
        {
            PlayerPrefs.SetFloat("RightPosX", defaultHandPos.x);
            PlayerPrefs.SetFloat("RightPosY", defaultHandPos.y);
            PlayerPrefs.SetFloat("RightPosZ", defaultHandPos.z);
            PlayerPrefs.SetFloat("RightRotX", defaultHandRot.x);
            PlayerPrefs.SetFloat("RightRotY", defaultHandRot.y);
            PlayerPrefs.SetFloat("RightRotZ", defaultHandRot.z);
        }
        else
        {
            PlayerPrefs.SetFloat("LeftPosX", defaultHandPos.x);
            PlayerPrefs.SetFloat("LeftPosY", defaultHandPos.y);
            PlayerPrefs.SetFloat("LeftPosZ", defaultHandPos.z);
            PlayerPrefs.SetFloat("LeftRotX", defaultHandRot.x);
            PlayerPrefs.SetFloat("LeftRotY", defaultHandRot.y);
            PlayerPrefs.SetFloat("LeftRotZ", defaultHandRot.z);
        }
    }

    public void LoadHandPosition()
    {
        if (PlayerPrefs.HasKey("LeftPosX"))
        {
            if (_isRightHand)
            {
                defaultHandPos = new Vector3(PlayerPrefs.GetFloat("RightPosX"), PlayerPrefs.GetFloat("RightPosY"), PlayerPrefs.GetFloat("RightPosZ"));
                defaultHandRot = new Vector3(PlayerPrefs.GetFloat("RightRotX"), PlayerPrefs.GetFloat("RightRotY"), PlayerPrefs.GetFloat("RightRotZ"));
            }
            else
            {
                defaultHandPos = new Vector3(PlayerPrefs.GetFloat("LeftPosX"), PlayerPrefs.GetFloat("LeftPosY"), PlayerPrefs.GetFloat("LeftPosZ"));
                defaultHandRot = new Vector3(PlayerPrefs.GetFloat("LeftRotX"), PlayerPrefs.GetFloat("LeftRotY"), PlayerPrefs.GetFloat("LeftRotZ"));
            }
            _handModel.transform.localPosition = defaultHandPos;
            _handModel.transform.localEulerAngles = defaultHandRot;
        }
    }

    public void ParentObjectToFixedHandPosition(GameObject obj, Vector3 pos, Vector3 rot, Vector3 scale)
    {
        obj.transform.SetParent(itemFixedToHand);
        obj.transform.localPosition = pos;
        obj.transform.localEulerAngles = rot;
        obj.transform.localScale = scale;
    }

    public void EmptyHand()
    {
        _grabController.ClearAllGrabbableInfo();
        HandIdleState();
    }

    public VRPlayerController GetPlayer() { return _player; }
    public VRPlayerHand GetOppositeHand() { return _oppositeHand; }
    public SkinnedMeshRenderer GetHandRenderer() { return _handRenderer; }
    public bool IsRightHand() { return _isRightHand; }
    public bool IsPrimaryHand()
    {
        if (_player.isLeftHanded && !_isRightHand) { return true; }
        else if (!_player.isLeftHanded && _isRightHand) { return true; }
        else return false;
    }
    public GrabController GetGrabController() { return _grabController; }
    public ClimbingController GetClimbController() { return _climbingController; }
    public HandAnimationState GetHandAnimationState() { return _handAnimator; }
    public GameObject GetHandModel() { return _handModel; }
    public MenuRaycast GetMenuRaycast() { return _menuRaycast; }
    public Vector3 GetHandAcceleration() { return _handAccel; }
    public  Vector3 GetHandVelocity() { return _handVel; }
    public Vector3 GetHandAngularVelocity() { return _handAngVel; }
    public SpellCastingForHands GetSpellCastingForHands() { return _spellCasting; }
    public Transform GetMenuSpawnLocation() { return _menuSpawnLocation; }

    public bool CheckIfHoldingSpecificItem(int holdingItemIdx)
    {
        if (_grabController.CheckIfHoldingSpecificThing(holdingItemIdx)) { return true; }
        else return false;
    }
}