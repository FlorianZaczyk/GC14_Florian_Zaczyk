using UnityEngine;

public class PlayerDmg : MonoBehaviour
{
    [SerializeField] private int damage;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyInformation>()._currentLifePoints  -= damage;
            }
        }
}
