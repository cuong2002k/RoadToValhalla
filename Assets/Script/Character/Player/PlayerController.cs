using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerState playerState { get; private set; }
    public PlayerMachine playerMachine { get; private set; }
    public PlayerData playerData;
    [SerializeField] private CameraController cameraObject;

    #region Component
    public InputManager InputHandler { get; private set; }
    public Animator PlayerAmin { get; private set; }
    public PlayerHudManger PlayerHudManager { get; private set; }
    public PlayerStatsManager PlayerStats { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    private Rigidbody _playerRB;
    private CharacterController _controller;


    public PlayerWeaponEquipment PlayerWeapon { get; set; }
    #endregion

    #region Player State

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerSprintingState SprintState { get; private set; }
    public PlayerCroundState CroundState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }
    public PlayerHurtState HurtState { get; private set; }

    #endregion

    #region other variable
    public Vector3 CurrentVelocity { get; private set; }
    private Vector3 _moveDirection;
    private float _rotationSpeed = 5f;
    [SerializeField] private Vector3 _yVelocity = Vector3.zero;

    #endregion

    #region check Variable
    private bool _hasBeenFalling = false;

    #endregion

    #region gravity
    private float _gravity = -9.81f;
    [SerializeField] private float _groundedGravity = -0.05f;
    private float _gravityForce = -9.81f;
    private float _fallStartVelocity = -5f;

    #endregion


    #region Unity Callback

    private void Awake()
    {
        playerMachine = new PlayerMachine();
        IdleState = new PlayerIdleState(this, playerMachine, playerData, "Idle");
        MoveState = new PlayerMoveState(this, playerMachine, playerData, "Move");
        SprintState = new PlayerSprintingState(this, playerMachine, playerData, "Move");
        CroundState = new PlayerCroundState(this, playerMachine, playerData, "Cround");
        LandState = new PlayerLandState(this, playerMachine, playerData, "Land");
        JumpState = new PlayerJumpState(this, playerMachine, playerData, "Jump");
        InAirState = new PlayerInAirState(this, playerMachine, playerData, "InAir");
        AttackState = new PlayerAttackState(this, playerMachine, playerData, "Attack");
        DeathState = new PlayerDeathState(this, playerMachine, playerData, "Death");
        HurtState = new PlayerHurtState(this, playerMachine, playerData, "Hurt");
    }

    private void Start()
    {
        InputHandler = InputManager.Instance;
        cameraObject = GetComponentInChildren<CameraController>();
        _playerRB = GetComponent<Rigidbody>();
        PlayerAmin = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        PlayerStats = GetComponent<PlayerStatsManager>();
        PlayerWeapon = GetComponent<PlayerWeaponEquipment>();
        PlayerHudManager = PlayerUIManager.Instance.PlayerHubManager;

        playerMachine.Inittialize(IdleState);
        _yVelocity.y = _groundedGravity;
    }

    private void Update()
    {
        playerMachine.CurrentState.LogicUpdate();
        CurrentVelocity = _yVelocity;
        PlayerRotation();
        HandlerGrayvity();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void FixedUpdate()
    {
        playerMachine.CurrentState.PhysicUpdate();


    }

    #endregion

    #region set Velocity
    /// <summary>
    /// set velocity movement
    /// </summary>
    /// <param name="desiredVelocity"></param>
    public void SetMovementVelocity(float moveSpeed)
    {
        _moveDirection = cameraObject.transform.forward * InputHandler.YInput;
        _moveDirection += cameraObject.transform.right * InputHandler.XInput;
        _moveDirection.Normalize();
        _moveDirection.y = 0;
        _controller.Move(_moveDirection * moveSpeed * Time.deltaTime);

        if (InputHandler.XInput != 0 || InputHandler.YInput != 0 && OnSlope())
        {
            _controller.Move(Vector3.down * _controller.height / 2 * 3f * Time.deltaTime);
        }

    }

    private float AdjustVelocityToSlope()
    {
        float moveY = 0;
        Ray ray = new Ray(this.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit outhit, 0.2f))
        {
            if (outhit.normal.normalized.y < 1) moveY = outhit.normal.normalized.y;
            else moveY = 0;
        }
        return moveY;
    }

    /// <summary>
    /// Set y velocity when press jumping
    /// </summary>
    public void SetJumpVelocity()
    {
        // caculator jumpspeed sqrt(-2gh) => g: gravity , h: jumpheight
        float jumpSpeed = Mathf.Sqrt(-2f * _gravityForce * playerData.jumpHeight);
        _yVelocity.y += jumpSpeed;
        _controller.Move(_yVelocity * Time.deltaTime);
    }


    private void PlayerRotation()
    {
        if (PlayerManager.Instance.IsDead) return;
        Quaternion lookDirection = Quaternion.Euler(cameraObject.transform.eulerAngles);
        transform.rotation = Quaternion.Slerp(this.transform.rotation, lookDirection, Time.deltaTime * _rotationSpeed);

    }

    #endregion

    #region check function
    public bool CheckIfGrounded()
    {
        //create sphere to check ground
        // return Physics.CheckSphere(this.transform.position, playerData.groundCheckRadius, playerData.whatIsGrounded);
        return _controller.isGrounded;

    }

    public bool OnSlope()
    {
        if (InputHandler.JumpInput) return false;
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, Vector3.down, out raycastHit, _controller.height / 2 * 1.5f))
        {
            if (raycastHit.normal != Vector3.up) return true;
        }
        return false;
    }
    #endregion

    private void HandlerGrayvity()
    {
        if (CheckIfGrounded())
        {
            if (_yVelocity.y < 0f)
            {
                _hasBeenFalling = true;
                _yVelocity.y = _groundedGravity;
            }
        }
        else
        {
            if (!_hasBeenFalling && !InputHandler.JumpInput)
            {
                _yVelocity.y = _fallStartVelocity;
                _hasBeenFalling = false;
            }

            _yVelocity.y += _gravity * Time.deltaTime;
            _controller.Move(_yVelocity * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position, playerData.groundCheckRadius);
    }

    public void Interact()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 5f))
        {
            IInteractable objectInteract = hit.transform.gameObject.GetComponent<IInteractable>();
            if (objectInteract != null)
            {
                objectInteract.Interact();
            }
        }
    }

}
