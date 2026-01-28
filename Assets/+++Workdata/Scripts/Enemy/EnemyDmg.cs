using UnityEngine;

public class EnemyDmg : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().health  -= damage;
        }
    }
}
