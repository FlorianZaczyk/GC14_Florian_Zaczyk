using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerAA : MonoBehaviour
{
    public static readonly int Hash_MovementValues = Animator.StringToHash("MovementValues");
    public static readonly int Hash_GroundValue = Animator.StringToHash("isGrounded");

    
    #region Insepctor Variables 
// Alle Variabeln die im Inspektor zu sehen sind. 
    [Header("Movement")]
    [SerializeField] private float _walkingSpeed =5f;
    [SerializeField] private float _runningSpeed = 7f;
    [SerializeField] private float _jumpPower = 3f;


    [Header("GroundCheck")]
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 boxOffset;
    [SerializeField] private LayerMask _groundLayer;
    
    
    #endregion

    #region Private Variables
    private Vector2 _moveInput;
    
    private Rigidbody2D _rb;

    private float _moveSpeed = 5f;
    private SpriteRenderer _sr;
    private Animator _anim;

    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isJumping;
    private bool isRolling;
    private bool isAttacking;
    private bool canJump;
    
    #endregion
    
    
    #region Input Actions

    public InputSystem_Actions _inputActions;

    #region Private InputActions
    
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _crouchAction;
    private InputAction _sprintAction;
    
    #endregion

    
    
    #endregion

    #region Unity Event Funktion
    
    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim =  GetComponent<Animator>();
        
         _moveSpeed = _walkingSpeed;
         
        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.Move;
        _jumpAction = _inputActions.Player.Jump;
        _attackAction = _inputActions.Player.Attack;
        _crouchAction = _inputActions.Player.Crouch;
        _sprintAction = _inputActions.Player.Sprint;
        
       
    }

    private void OnEnable()
    { 
        _inputActions.Enable();
        
        _moveAction.performed += Move;
        _moveAction.canceled += Move; 
        
    }

    private void FixedUpdate()
    {
        CheckGround();
        
        _rb.linearVelocityX = _moveInput.x * _walkingSpeed;

        UpdateAnimator();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;
        
    }
    #endregion
    
    #region Physics

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapBox(boxOffset + (Vector2)transform.position + boxSize, 0, _groundLayer);
    }

    #endregion
        
    #region Animations Methods
    void UpdateAnimator()
    {
        _anim.SetFloat(Hash_MovementValues, Mathf.Abs(_rb.linearVelocity.x));
        _anim.SetBool(Hash_GroundValue, isGrounded);
    }
        
    #endregion


    #endregion

    #region Input
    
    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        
        Gizmos.DrawWireCube(boxOffset + (Vector2)transform.position + boxSize);
    }
    private void Move(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
        _sr = GetComponent<SpriteRenderer>();
        
        if (_moveInput.x > 0)
        {
            _sr.flipX = false;
            isFacingRight = true;
        }
        else if (_moveInput.x < 0)
        {
            _sr.flipX = true;
            isFacingRight = false;
        }
        
        
    }

    #endregion
    
    
    
   
}