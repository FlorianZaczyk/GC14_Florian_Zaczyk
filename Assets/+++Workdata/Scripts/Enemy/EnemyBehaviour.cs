using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    #region Variables


    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D _rb;
    private Animator _anim;
    private Transform currentPoint;
    public float speed;

    #endregion
    #region Methods
    private void Start()
    {
        _rb= GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        _anim.SetBool("isWalking", true);
        
    }
    void FixedUpdate()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            _rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            _rb.linearVelocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position,currentPoint.position) <= 0.5f && currentPoint == pointB.transform)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position,currentPoint.position) <= 0.5f && currentPoint == pointA.transform)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            currentPoint = pointB.transform;
        }
    }
    
    #endregion

    #region Functions




    #endregion
    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
       
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
    }
    #endregion
}