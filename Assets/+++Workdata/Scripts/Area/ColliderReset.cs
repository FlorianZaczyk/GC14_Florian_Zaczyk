using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class ColliderReset : MonoBehaviour
{
    
    private Collider2D _collider2D;
    public float enableTimer = 1f;

    public void EnableCollider();
    {
        Invoker("EnableOneWayCollider", enableTimer);
    }
    
    private void EnableOneWayCollider()
    {
       _collider2D.enabled = true; ;
    }
}
