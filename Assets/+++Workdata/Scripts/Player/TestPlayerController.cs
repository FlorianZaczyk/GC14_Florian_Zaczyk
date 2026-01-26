using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerController : MonoBehaviour
{
    public enum PlayerMovementState { Idle, Move }
    public PlayerMovementState playerMovementState;    
    
    public enum PlayerDirectionState { Left, Right }
    public PlayerDirectionState playerDirectionState; 
    
    public enum PlayerActionState { Default, Attack, Jumping, Dashing, Climbing, JumpAttack }
    public PlayerActionState playerActionState;
    
    #region Insepctor Variables
    [Header("Movement Setup")] 
    [SerializeField] private float walkingSpeed;
        
    [Header("Action Setup")] 
    [SerializeField] private float jumpForce = 5f;
    
    [Header("Ground Setup")] 
    [SerializeField] private Vector2 groundBoxPos;
    [SerializeField] private Vector2 groundBoxSize;
    [SerializeField] private LayerMask groundLayer;
    #endregion

    #region Private Variables

    private Animator _animator;
    private Rigidbody2D _rb;
    private PlayerPlatformHandler _playerPlatformHandler;
    private PlayerInteractions _playerInteractions;
    
    private InputSystem_Actions _inputActions;
    private InputAction _moveAction;
    private InputAction _attackAction;
    private InputAction _jumpAction;
    private InputAction _interactAction;

    //private OneWayChecker _oneWayChecker;

    private Vector2 _moveInput;
    
    public bool _isGrounded;
    private bool _isRunning;
    private float currentSpeed;
    private float runningSpeed;
    
    #endregion

    #region Unity Event Functions
    
    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerPlatformHandler = GetComponent<PlayerPlatformHandler>();
        _playerInteractions = GetComponent<PlayerInteractions>();
        //_oneWayChecker = GetComponentInChildren<OneWayChecker>();
        
        SetInputs();
        SetDirection(playerDirectionState);

        playerActionState = PlayerActionState.Default;
    }
    
    private void OnEnable()
    {
        _inputActions.Enable();

        _moveAction.performed += Move;
        _moveAction.canceled += Move;

        _attackAction.performed += Attack;

        _jumpAction.performed += Jump;

        _interactAction.performed += Interact;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocityX = _moveInput.x * walkingSpeed;
        
        UpdateAnimations();
        CheckIsGrounded();
    }

    private void OnDisable()
    {
        _inputActions.Disable();

        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;
        
        _attackAction.performed -= Attack;
        
        _jumpAction.performed -= Jump;
        
        _interactAction.performed -= Interact;
    }

    #endregion

    #region Input

    private void SetInputs()
    {
        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.Move;
        _attackAction = _inputActions.Player.Attack;
        _jumpAction = _inputActions.Player.Jump;
        _interactAction = _inputActions.Player.Interact;
    }
    
    private void Move(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();

        if (_moveInput.x > 0)
        {
            SetDirection(PlayerDirectionState.Right);
        }
        else if(_moveInput.x < 0)
        {
            SetDirection(PlayerDirectionState.Left);
        }

        if (_moveInput.y < 0)
        {
           _playerPlatformHandler.TryDisableOneWayEffector();
        }

        if (_moveInput.x == 0)
        {
            playerMovementState = PlayerMovementState.Idle;
        }
        else
        {
            playerMovementState = PlayerMovementState.Move;
        }
        
        currentSpeed = _isRunning ? runningSpeed : walkingSpeed;
    }

    private void Attack(InputAction.CallbackContext ctx)
    {
        if (playerActionState == PlayerActionState.Attack) {return;}

        playerActionState = PlayerActionState.Attack;
        AnimationSetAction(10);

    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (!_isGrounded || playerActionState == PlayerActionState.Jumping) return;
            
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        
        //SetActionState(PlayerActionState.Jumping);
        //AnimationSetActionId(1);
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        if(_playerInteractions == null)
        {
            Debug.LogWarning("No PlayerInteraction component to the player attached");
            return;
        }
        
        _playerInteractions.TryInteract();
    }
    
    #endregion

    #region Animations

    private void AnimationSetAction(int id)
    {
        _animator.SetTrigger("ActionTrigger");
        _animator.SetInteger("ActionId", id);
    }
    
    private void UpdateAnimations()
    {
        _animator.SetFloat("MovementValue", Mathf.Abs(_rb.linearVelocityX));
    }

    public void AnimationEndAttack()
    {
        playerActionState = PlayerActionState.Default;
    }

    #endregion

    #region Utility

    private void SetDirection(PlayerDirectionState newPlayerDirectionState)
    {
        playerDirectionState = newPlayerDirectionState;
        
        if (playerDirectionState == PlayerDirectionState.Left)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    #endregion

    #region Physics

    private void CheckIsGrounded()
    {
        _isGrounded = Physics2D.OverlapBox((Vector2)transform.position + groundBoxPos,  groundBoxSize, 0, groundLayer);
    }

    #endregion
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + groundBoxPos, groundBoxSize);
    }
}