using TMPro;
using UnityEngine;

public class EnemyPatrolMovement : MonoBehaviour
{

    public enum EnemyMovementState{Idle, Movement, Chase}
    public enum EnemyActionState{Default, Attack, Dead}

    #region Inspector

    [Header("States")] 
    public EnemyMovementState enemyMovementState;
    public EnemyActionState enemyActionState;
    
    
    [Header("Movement")]
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private float movementSpeed;
    
    [Header("Attack")]
    [SerializeField] private float attackDistance = 1;
    
    #endregion

    #region Private Variables

    private Rigidbody2D _rb;
    private EnemyAnimation _enemyAnimation;
    private int _facingDirection;
    
    public int FacingDirection => _facingDirection;
    
    public Transform _chaseTarget;

    #endregion
    
    #region Unity Events

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        
        _facingDirection = isFacingRight ? 1 : -1;
    }

    private void FixedUpdate()
    {
        if (enemyActionState != EnemyActionState.Default) return;

        if (enemyMovementState == EnemyMovementState.Chase)
        {
            if (_chaseTarget == null)
            {
                Debug.LogError("Chase Target is null");
                return;
            }

            if (Vector2.Distance(transform.position, _chaseTarget.position) < attackDistance)
            {
                _enemyAnimation.AnimationSetAttack();
                enemyActionState = EnemyActionState.Attack;
            }

            if ((transform.position.x < _chaseTarget.position.x && _facingDirection == -1) ||
                (transform.position.x > _chaseTarget.position.x && _facingDirection == 1))
            {
                ChangeDirection();
            }
        }
        
        _rb.linearVelocityX = movementSpeed * _facingDirection;
    }

    private void LateUpdate()
    {
        UpdateAnimator();
    }
    
    #endregion
    
    #region States


    public void SetMovementStateToChase(Transform target)
    {
        enemyMovementState = EnemyMovementState.Chase;
        _chaseTarget = target;
    }


    public void SetMovementState(int state)
    {
        enemyMovementState = (EnemyMovementState)state;
        
        if (enemyMovementState != EnemyMovementState.Chase) 
            _chaseTarget = null;
    }
    
    public void SetActionState(int state)
    {
        enemyActionState = (EnemyActionState)state;
    }


    public void SetActionStateToDefault()
    {
        enemyActionState = EnemyActionState.Default;
    }
    
    #endregion
    
    
    #region Movement

    public void ChangeDirection()
    {
        _facingDirection *= -1; //-1 * -1 = 1 * -1 = -1
        
        transform.rotation = Quaternion.Euler(0, _facingDirection == 1 ? 0 : 180, 0);
    }

    #endregion
    
    #region Animaton

    private void UpdateAnimator()
    {
      //  _enemyAnimation.AnimationSetMovementValue(Mathf.Abs(_rb.linearVelocityX));
    }
    
    #endregion
}
