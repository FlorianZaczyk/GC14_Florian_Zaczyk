using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControllerNew1 : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField] 
    private float walkingSpeed;

    #endregion

    #region Private Variables

    private InputSystem_Actions _inputActions;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private Rigidbody2D _Player;

    public Vector2 _moveInput;
    
    
    #endregion

    #region Unity_Event_Functions

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
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
        float walkingSpeed = 2f;
        _moveInput = ctx.ReadValue<Vector2>(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _Player.linearVelocity = _moveInput * walkingSpeed;
    }

    #endregion
}