using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Insepctor Variables

// Alle Variabeln die im Inspektor zu sehen sind. 
    [SerializeField] private float _walkingSpeed = 5f;
    [SerializeField] private float _runningSpeed = 7f;
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    #endregion

    #region Private Variables

    private Vector2 _moveInput;
    private Transform _player;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private float _moveSpeed = 5f;
    private bool _isGrounded;
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
        _player = GetComponent<Transform>();


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
        _jumpAction.performed += Jump;

    }

    private void FixedUpdate()
    {
        _rb.linearVelocityX = _moveInput.x * _walkingSpeed;

    if (_groundCheck != null)
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }

    else
    {
        _isGrounded = false;
    }
    }


private void OnDisable()
    {
        _inputActions.Disable();
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move; 
        _jumpAction.performed -= Jump;

    }

    #endregion

    #region Input
    private void Move(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
        
        if (_moveInput.x > 0)
        { 
            _player.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_moveInput.x < 0)
        { 
            _player.rotation = Quaternion.Euler(0, 180, 0);
        }
        
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (_isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
        }
    }
    
    #endregion
  
}
