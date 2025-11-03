using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Insepctor Variables 
// Alle Variabeln die im Inspektor zu sehen sind. 
    [SerializeField] private float _walkingSpeed =5f;
    [SerializeField] private float _runningSpeed = 7f;

    #endregion

    #region Private Variables
    private Vector2 _moveInput;
    
    private Rigidbody2D _rb;

    private float _moveSpeed = 5f;
    private SpriteRenderer _sr;

    private bool isFacingRight = true;
    
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
        _rb.linearVelocityX = _moveInput.x * _walkingSpeed;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move; 

    }

    #endregion

    #region Input
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