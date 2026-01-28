using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum PlayerMovementState { Idle, Move }
    public PlayerMovementState playerMovementState;

    public enum PlayerActionState { Default, Attack, SecondAttack, Jumping, Dashing, Climbing, JumpAttack }
    public PlayerActionState playerActionState;
    
    public enum PlayerDirectionState{Right, Left}
    public PlayerDirectionState playerDirectionState;
    
    public static readonly int Hash_MovementValue = Animator.StringToHash("MovementValue");
    public static readonly int Hash_GroundValue = Animator.StringToHash("isGrounded");
    public static readonly int Hash_JumpValue = Animator.StringToHash("isJumping");
    private static readonly int Hash_Actionid = Animator.StringToHash("ActionID");
    private static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    private static readonly int Hash_HoldingMouse = Animator.StringToHash("HoldingMouse");
    
    
    #region Insepctor Variables

// Alle Variabeln die im Inspektor zu sehen sind. 
    [SerializeField] private float _walkingSpeed = 5f;
    [SerializeField] private float _runningSpeed = 7f;
    
    [SerializeField] private float _dashSpeed = 14f;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    
    public bool _isSprinting;
    public float _sprintSpeed;
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 boxOffset;
    private Vector2 boxsize;

    #endregion

    #region Private Variables

    private Vector2 _moveInput;
    private Transform _player;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private float _moveSpeed = 5f;
    private float _bounceForce = 15f;

    private bool _isDashing = false;
    private bool _canDash = true;
    private bool _isGrounded;
    private bool _isHoldingMouse;

     //private OneWayChecker _oneWayChecker;
    
    //private bool isFacingRight = true;
    private Animator _anim;
    private PlayerPlatformHandler _playerPlatformHandler;
    private DamagePush _damagePush;
    private PlayerInteractions _playerInteractions;
    private InputAction _interactAction;
    private EnemyInformation _enemyInformation;

    #endregion


    #region Input Actions

    public InputSystem_Actions _inputActions;

    #region Private InputActions

    private InputAction _moveAction;
    private InputAction _dashAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _secondAttackAction;
    private InputAction _crouchAction;
    private InputAction _sprintAction;


    #endregion



    #endregion

    #region Unity Event Funktion

    //Debug.Log("Mouse clicked");

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _player = GetComponent<Transform>();
        _anim = GetComponent<Animator>();
        _playerInteractions = GetComponent<PlayerInteractions>();
        _playerPlatformHandler = GetComponent<PlayerPlatformHandler>();
        _damagePush = GetComponent<DamagePush>();
        _enemyInformation = GetComponent<EnemyInformation>();
        //_oneWayChecker = GetComponentInChildren<OneWayChecker>();


        _moveSpeed = _walkingSpeed;

        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.Move;
        _jumpAction = _inputActions.Player.Jump;
        _attackAction = _inputActions.Player.Attack;
        _secondAttackAction = _inputActions.Player.SecondAttack;
        _crouchAction = _inputActions.Player.Crouch;
        _sprintAction = _inputActions.Player.Sprint;
        _dashAction = _inputActions.Player.Dash;
        _interactAction = _inputActions.Player.Interact;

        
        playerActionState = PlayerActionState.Default;

        if (playerDirectionState == PlayerDirectionState.Right)
        {
            _player.rotation = Quaternion.Euler(0, 0, 0); 
        }
        else if (playerDirectionState == PlayerDirectionState.Left)
        {
            _player.rotation = Quaternion.Euler(0, 180, 0);  
        }
    }
    
    
    private void OnEnable()
    {
        _inputActions.Enable();

        _moveAction.performed += Move;
        _moveAction.canceled += Move;
        _jumpAction.performed += Jump;
        _sprintAction.performed += Sprint;
        _sprintAction.canceled += Sprint;
        _attackAction.started += Attack;
        _attackAction.canceled += AttackReleased;
        _secondAttackAction.started += SecondAttack;
        _dashAction.performed += Dash;
        _interactAction.performed += Interact;


    }

    private void FixedUpdate()
    {
       // _rb.linearVelocityX = _moveInput.x * _walkingSpeed;
        //_rb.linearVelocityX = new Vector2(_moveInput.x * _walkingSpeed, _rb.linearVelocity.y);

        if (_groundCheck != null)
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        }

        else
        {
            _isGrounded = false;
        }

        if (!_isDashing)
        {
            _rb.linearVelocityX = _moveInput.x * _walkingSpeed;
        }

        //float _moveSpeed = Mathf.Abs(_moveInput.x); 
        //_anim.SetFloat("MovementValue", _moveSpeed);

        UpdateAnimator(); 
    }


    private void OnDisable()
    {
        _inputActions.Disable();
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;
        _jumpAction.performed -= Jump;
        _attackAction.performed -= Attack;
        _secondAttackAction.canceled -= SecondAttack;
        _dashAction.performed -= Dash;
        _interactAction.performed -= Interact;

    }

    #endregion

    #region Input

    private void Move(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();

        if (_moveInput.x > 0)
        {
            SetDirection(PlayerDirectionState.Right);
        }
        else if (_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            SetDirection(PlayerDirectionState.Left);
        }

        if (_moveInput.y < 0)
        {
            _playerPlatformHandler.TryDisableOneWayEffector();
           // s pressed
        }
        
        
        if (_moveInput.x == 0)
        {
            playerMovementState = PlayerMovementState.Idle;
        }
        else
        {
            playerMovementState = PlayerMovementState.Move;
        }

        if (_moveInput.x > 0)
        {
            playerDirectionState = PlayerDirectionState.Right;
        }
        else
        {
            playerDirectionState = PlayerDirectionState.Left;
        }
    }

    private void Dash(InputAction.CallbackContext ctx)
    {
        if (!_canDash || _isDashing) return;
        
        _anim.SetInteger("ActionID", 3); //gleiche Zahl wie im Animator
        _anim.SetTrigger("ActionTrigger"); //löst die StateMachine aus

        StartCoroutine(PerformDash());
    }

    private System.Collections.IEnumerator PerformDash()
    {
        _isDashing = true;
        _canDash = false;

        playerActionState = PlayerActionState.Dashing;

        float originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0; // Optional: kein Fall während Dash

        float direction = transform.rotation.y == 0 ? 1f : -1f;

        float timer = 0f;
        while (timer < dashDuration)
        {
            _rb.linearVelocity = new Vector2(direction * _dashSpeed, 0);
            timer += Time.deltaTime;
            yield return null;
        }

        // Stop Movement
        _rb.linearVelocity = Vector2.zero;

        _rb.gravityScale = originalGravity;

        _isDashing = false;

        // Nachdem Dash vorbei ist, wieder normaler Zustand
        if (_isGrounded)
            playerActionState = PlayerActionState.Default;

        yield return new WaitForSeconds(dashCooldown);

        _canDash = true;
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (_isGrounded)
        {
            //_rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _anim.SetTrigger(Hash_JumpValue);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ForcePush"))
        {
            _rb.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
        }
        //_damagePush.ForcePush();
    }

    private void Sprint(InputAction.CallbackContext ctx)
    {
        _isSprinting = !_isSprinting;
        _moveSpeed = _isSprinting ? _sprintSpeed : _walkingSpeed;

    }
    
    void Attack(InputAction.CallbackContext ctx)
    {
        if (playerActionState == PlayerActionState.Attack) return;
        playerActionState = PlayerActionState.Attack;
        AnimationSetActionId(10);

        _moveSpeed = 0f;
        _walkingSpeed = 0f;
        _isHoldingMouse = true;
        _anim.SetBool(Hash_HoldingMouse,true);

        _enemyInformation.SetDamage(1);
    }

    void AttackReleased(InputAction.CallbackContext ctx)
    {
        _anim.SetBool(Hash_HoldingMouse, false);
    }

    private void SecondAttack(InputAction.CallbackContext ctx)
    {
        if (playerActionState == PlayerActionState.SecondAttack) return;
        playerActionState = PlayerActionState.SecondAttack;
        AnimationSetActionId(11);
    }
    
    private void EndAttackEvent()
    {
        if (playerActionState != PlayerActionState.Default)
        {
            playerActionState = PlayerActionState.Default;
        }

        
            _moveSpeed = 5f;
            _walkingSpeed = 5f;
        
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        print("pressed E");
        if (_playerInteractions == null)
        {
            Debug.LogWarning("No PlayerInteraction component to the player attached");
            return;
        }

        _playerInteractions.TryInteract(); 
    }
    
    
   /* private void SecondAttack(InputAction.CallbackContext ctx)
    {
        Debug.Log("Mouse clicked");
        _anim.SetTrigger(Hash_ActionTrigger);
        _anim.SetInteger("ActionID", 11);
        
    } */
    
    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(boxOffset + (Vector2)transform.position, boxsize);
    }
    #endregion

    private void AttackEndAnimation()
    {
        playerActionState = PlayerActionState.Default;
    }
    


        #region Animation Methods

        private void AnimationSetActionId (int id)
        {
  
            _anim.SetInteger(Hash_Actionid, id);
            _anim.SetTrigger(Hash_ActionTrigger);
            _anim.SetBool(Hash_HoldingMouse, true);
        }
        void UpdateAnimator()
        {
            _anim.SetFloat(Hash_MovementValue, Mathf.Abs(_rb.linearVelocity.x));
            _anim.SetBool(Hash_GroundValue, _isGrounded);
        }

        #endregion

        #region Utility

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


        #endregion
    }
