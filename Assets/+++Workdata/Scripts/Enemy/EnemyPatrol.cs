using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPatrol : MonoBehaviour
{
    #region Variables

    public Transform pointA;
    public Transform pointB;
    private Rigidbody2D _rb;
    private Animator _anim;
    private Transform currentPoint;
    public float speed;
    public float damage;

    #endregion
    #region Methods
    private void Start()
    {
        _rb= GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        currentPoint = pointB;
        _anim.SetBool("isWalking", true);

    }
    void FixedUpdate()
    {

    }

    void Update()
    {
        if (currentPoint == pointB)
            _rb.velocity = new Vector2(speed, 0);
        else
            _rb.velocity = new Vector2(-speed, 0);

        // Prüfen, ob Enemy den Punkt erreicht hat
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
    }

    private void flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().health -= damage;
        }
    }

    #endregion
}