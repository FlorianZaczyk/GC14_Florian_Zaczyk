using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class TestPlayerController : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField] 
    private float walkingSpeed = 2f;

    #endregion

    #region Private Variables

    private InputSystem_Actions _inputActions;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private Rigidbody2D rb;
    
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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //throw new NotImplementedException();
        _moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    }
    private void FixedUpdate()
    {
        rb.linearVelocity = _moveInput * walkingSpeed;
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
    }

    #endregion
}


