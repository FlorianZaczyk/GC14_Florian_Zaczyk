using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyEnviromentChecker : MonoBehaviour
{
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform groundCheck;
    
    [SerializeField] private float wallCheckDistance = 0.2f;
    [SerializeField] private float groundCheckDistance = 0.3f;
    
    [SerializeField] private LayerMask groundAndWallMask;


    #region Private Variables

    private EnemyPatrolMovement _enemyPatrolMovement;

    #endregion

    private void Awake()
    {
        _enemyPatrolMovement = GetComponent<EnemyPatrolMovement>();
    }

    private void FixedUpdate()
    {
        if (_enemyPatrolMovement.enemyMovementState != EnemyPatrolMovement.EnemyMovementState.Movement) return;

        if (CheckForWalls() || !CheckForGround())
        {
            _enemyPatrolMovement.ChangeDirection();
        }
    }

    bool CheckForWalls()
    {
        Vector2 direction = Vector2.right * _enemyPatrolMovement.FacingDirection;

        RaycastHit2D hit = Physics2D.Raycast(
            wallCheck.position,
            direction,
            wallCheckDistance,
            groundAndWallMask);
        
        return hit.collider ;
    }
    
    bool CheckForGround()
    {
        Vector2 direction = Vector2.right * _enemyPatrolMovement.FacingDirection;

        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            groundCheckDistance,
            groundAndWallMask);
        
        return hit.collider ;
    }
}
