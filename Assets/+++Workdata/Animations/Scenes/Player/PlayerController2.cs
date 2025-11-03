using System;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed = 2f;
    
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementDirection * movementSpeed;
    }
}
