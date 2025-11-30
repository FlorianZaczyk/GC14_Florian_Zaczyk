using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AmyPlayerController : MonoBehaviour
{
    #region Inspector Variables 
    // Alle Variablen, die im Unity-Inspektor sichtbar sind (zum Anpassen im Editor)
    
    [SerializeField] private float _walkingSpeed = 5f;         // Laufgeschwindigkeit beim normalen Gehen
    [SerializeField] private float _runningSpeed = 7f;         // Laufgeschwindigkeit beim Rennen
    [SerializeField] private float _jumpForce = 10f;           // Sprungkraft des Spielers
    [SerializeField] private Transform _groundCheck;           // Transform-Referenz für Bodenprüfung
    [SerializeField] private float _groundCheckRadius = 0.2f;  // Radius der Bodenprüfung
    [SerializeField] private LayerMask _groundLayer;           // Layer, der den Boden definiert
    
    #endregion

    #region Private Variables
    // Interne Variablen, die nicht im Inspektor sichtbar sind

    private Vector2 _moveInput;        // Bewegungsrichtung des Spielers (von Input)
    private Rigidbody2D _rb;           // Referenz auf das Rigidbody2D des Spielers
    private float _moveSpeed = 5f;     // Aktuelle Bewegungsgeschwindigkeit (kann zwischen Walk/Run wechseln)
    private SpriteRenderer _sr;        // Referenz auf SpriteRenderer (zum Drehen der Figur)
    private bool _isGrounded;          // Gibt an, ob der Spieler auf dem Boden ist
    private bool isFacingRight = true; // Gibt an, ob der Spieler nach rechts schaut
    
    #endregion

    #region Input Actions
    // Eingabeaktionen (Unity Input System)

    public InputSystem_Actions _inputActions;  // Instanz der generierten InputActions-Klasse

    #region Private InputActions
    // Einzelne Aktionen, die vom Input System ausgelesen werden

    private InputAction _moveAction;    // Bewegungseingabe
    private InputAction _jumpAction;    // Sprungaktion
    private InputAction _attackAction;  // Angriffsaktion
    private InputAction _crouchAction;  // Ducken
    private InputAction _sprintAction;  // Rennen

    #endregion

    #endregion

    #region Unity Event Funktionen
    // Standard Unity-Funktionen für Lebenszyklus & Physik

    private void Awake()
    {
        // Referenzen holen
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();

        // Standardgeschwindigkeit setzen
        _moveSpeed = _walkingSpeed;
        
        // Input-System initialisieren und Aktionen zuweisen
        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.Move;
        _jumpAction = _inputActions.Player.Jump;
        _attackAction = _inputActions.Player.Attack;
        _crouchAction = _inputActions.Player.Crouch;
        _sprintAction = _inputActions.Player.Sprint;
    }
    
    private void OnEnable()
    { 
        // Eingaben aktivieren
        _inputActions.Enable();

        // Listener registrieren
        _moveAction.performed += Move;
        _moveAction.canceled += Move; 
        _jumpAction.started += Jump;
    }


    private void FixedUpdate()
    {
        // Bewegung des Spielers (x-Richtung)
        _rb.linearVelocity = new Vector2(_moveInput.x * _moveSpeed, _rb.linearVelocity.y);

        // Bodenprüfung per OverlapCircle
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
        // Eingaben deaktivieren und Listener entfernen
        _inputActions.Disable();
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move; 
        _jumpAction.started -= Jump;
    }

    #endregion

    #region Input
    // Funktionen, die auf Spieler-Eingaben reagieren

    private void Move(InputAction.CallbackContext ctx)
    {
        // Bewegungseingabe auslesen (x/y)
        _moveInput = ctx.ReadValue<Vector2>();
        
        // Spieler drehen je nach Richtung
        if (_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFacingRight = true;
        }
        else if (_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            isFacingRight = false;
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        // Springen nur, wenn Spieler auf dem Boden steht
        if (_isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
        }
    }

    #endregion
}