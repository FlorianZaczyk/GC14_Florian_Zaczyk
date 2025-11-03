using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class _2PlayerController : MonoBehaviour
{
    #region Insepctor Variables

    [SerializeField] private float walkingSpeed;
    #endregion

    #region Private Variables

    private Rigidbody2D _rb;
    private InputSystem_Actions _inputActions;
    private InputAction _moveAction;
    private InputAction _jumpAction;

    public Vector2 _moveInput;


    #endregion

    #region Unity Event Functions

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        
        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.Move;
        _jumpAction = _inputActions.Player.Jump;
    }

    private void OnEnable()
    {
        _inputActions.Enable();

        _moveAction.performed += Move;
        _moveAction.canceled += Move;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocityX = _moveInput.x * walkingSpeed;
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

        if (_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
    }

    #endregion
    
    
}