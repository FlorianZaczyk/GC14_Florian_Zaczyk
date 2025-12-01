using System;
using UnityEngine;

public class OneWayChecker : MonoBehaviour
{
    private Collider2D _oneWayPlatform;
    
    public string tagName = "OneWay";
    public float enableTimer = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.name);
        if (other.CompareTag(tagName))
        {
            _oneWayPlatform = other;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(tagName))
        {
           // _oneWayPlatform = null;
        }
    }

    public void DisableOneWayCollider()
    {
        if (!_oneWayPlatform) return;

        _oneWayPlatform.enabled = false;
        
        _oneWayPlatform.GetComponent<ColliderReset>().EnableCollider();
    }

    private void EnableOneWayCollider()
    {
        _oneWayPlatform.enabled = true;
        _oneWayPlatform = null;
    }
}