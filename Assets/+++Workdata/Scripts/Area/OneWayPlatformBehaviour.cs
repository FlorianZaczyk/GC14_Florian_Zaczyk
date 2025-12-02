using UnityEngine;

public class OneWayPlatformBehaviour : MonoBehaviour
{
    private Collider2D _collider2D;
    public float enableTimer = 1;
    
    public void EnableCollider()
    {
        Invoke("EnableOneWayCollider", enableTimer);
    }
    
    private void EnableOneWayCollider()
    {
        _collider2D.enabled = true;
    }
}
