using UnityEngine;

public class EnemyInformation : MonoBehaviour
{
    [SerializeField] private int enemyMaxLifePoints;

    public float _currentLifePoints;

    private ColorSpriteSetter _colorSpriteSetter;
    private Collider2D _coll;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _currentLifePoints = enemyMaxLifePoints;

        _colorSpriteSetter = GetComponent<ColorSpriteSetter>();
        _coll = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetDamage(int dmg)
    {
        _currentLifePoints -= dmg;

        if (_currentLifePoints < 1)
        {
            _coll.enabled = false;
            _rb.bodyType = RigidbodyType2D.Kinematic;
            GetComponentInChildren<EnemyAnimation>().AnimationEnemyDeath();
            EnemyPatrolMovement enemyPatrol = GetComponentInChildren<EnemyPatrolMovement>();
            GetComponentInChildren<EnemyPatrolMovement>().SetActionState(2);
            enemyPatrol.enabled = false;
        }

        _colorSpriteSetter.ColorObject();
    }
}
