using System;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShawneePlayerController : MonoBehaviour

{
    private static readonly int HashMovementValue = Animator.StringToHash("MovementValue");
    private static readonly int HashGroundValue = Animator.StringToHash("isGrounded");
    private static readonly int Hash_JumpValue = Animator.StringToHash("JumpValue");
    private static readonly int Hash_Actionid = Animator.StringToHash("ActionId");
    private static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    private static readonly int Hash_HoldingMouse = Animator.StringToHash("HoldingMouse");


    #region Insepctor Variables

    [Header("Movement Variables")] [SerializeField]
    private float walkingSpeed;

    public float _jump;
    public float _jumpforce = 3f;
    public bool _isSprinting;
    public float _sprintSpeed;
    private float _currentSpeed;

    [Header("GroundCheck")] [SerializeField]
    private Vector2 boxsize;

    [SerializeField] private Vector2 boxOffset;
    [SerializeField] private LayerMask groundLayer;

    #endregion

    #region Private Variables

    private Rigidbody2D _rb;
    private InputSystem_Actions _inputActions;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _sprintAction;
    private InputAction _attackAction;
    private InputAction _attackDownAction;

    public Animator _anim;
    public Vector2 _moveInput;

    public bool _isGrounded;
    private bool _isJumping;
    private bool _canJump;
    private bool _isAttacking;
    private bool _isHoldingMouse;

    #endregion

    #region Unity Event Functions

    /// <summary>
    /// sr.color = Color.red;
    /// </summary>
    private void Awake()
    {
        _canJump = true;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _currentSpeed = walkingSpeed;

        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.Move;
        _jumpAction = _inputActions.Player.Jump;
        _sprintAction = _inputActions.Player.Sprint;
        _attackAction = _inputActions.Player.Attack;
        _attackDownAction = _inputActions.Player.SecondAttack;
        
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
        _attackDownAction.started += AttackDown;
        _attackDownAction.canceled += AttackDown;


    }



    private void FixedUpdate()
    {
        CheckGround();
        _rb.linearVelocityX = _moveInput.x * _currentSpeed;

        UpdateAnimator();
    }

    private void OnDisable()
    {
        _inputActions.Disable();

        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;
        _jumpAction.performed -= Jump;
        _sprintAction.performed -= Sprint;
        _sprintAction.canceled -= Sprint;
        _attackAction.canceled -= Attack;
        _attackDownAction.canceled -= AttackDown;
        
    }

    #endregion

    #region Input Methode

    private void Jump(InputAction.CallbackContext obj)
    { ;
        if (_isGrounded)
        {
             _rb.AddForce(Vector2.up * _jumpforce, ForceMode2D.Impulse);
                    _anim.SetTrigger(Hash_JumpValue);
        }


       
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
        if (_moveInput.x < 0) //left
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (_moveInput.x > 0) //right
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Sprint(InputAction.CallbackContext ctx)
    {
        _isSprinting = !_isSprinting;
        _currentSpeed = _isSprinting ? _sprintSpeed : walkingSpeed;

    }

    private void Attack(InputAction.CallbackContext ctx)
    {
        Debug.Log("Mouse clicked");
        _anim.SetTrigger(Hash_ActionTrigger);
        _anim.SetInteger(Hash_Actionid, 11);
        _isHoldingMouse = true;
        _anim.SetBool(Hash_HoldingMouse,true);
    }
    private void AttackReleased(InputAction.CallbackContext ctx)
    {
        _anim.SetBool(Hash_HoldingMouse, false);
    }

    private void AttackDown(InputAction.CallbackContext ctx)
    {
        Debug.Log("Mouse clicked");
        _anim.SetTrigger(Hash_ActionTrigger);
        _anim.SetInteger(Hash_Actionid, 12);
        
    }
    #endregion

    #region Physics Functions

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapBox(boxOffset + (Vector2)transform.position, boxsize, 0, groundLayer);
    }

    
    #endregion

    #region Animation Methods
    
    private void AnimationSetActionId (int id)
    {
  
        _anim.SetInteger(Hash_Actionid, id);
        _anim.SetTrigger(Hash_ActionTrigger);
        _anim.SetBool(Hash_HoldingMouse, true);
    }
    private void UpdateAnimator()
    {

        _anim.SetFloat(HashMovementValue, Mathf.Abs(_rb.linearVelocity.x));
        _anim.SetBool(HashGroundValue, _isGrounded);
    }

    

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(boxOffset + (Vector2)transform.position, boxsize);
    }

    #endregion
}