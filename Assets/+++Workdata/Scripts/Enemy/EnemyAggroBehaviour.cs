using UnityEngine;

public class EnemyAggroBehaviour : MonoBehaviour
{
  private EnemyPatrolMovement _enemyPatrolMovement;

  private void Awake()
  {
    _enemyPatrolMovement = GetComponentInParent<EnemyPatrolMovement>();
  }


  private void OnTriggerEnter2D(Collider2D other)
  {

    if (other.CompareTag("Player"))
    {
      _enemyPatrolMovement.SetMovementStateToChase(other.transform);
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      _enemyPatrolMovement.SetMovementState(1);
    }
  }

}
