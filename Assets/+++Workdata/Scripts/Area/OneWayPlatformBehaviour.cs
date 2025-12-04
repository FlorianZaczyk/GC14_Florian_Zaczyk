using System;
using UnityEngine;

public class OneWayPlatformBehaviour : MonoBehaviour
{
    private PlatformEffector2D _platformEffector;
    public float enableTimer = 1;


    private void Awake()
    {
        _platformEffector = GetComponent<PlatformEffector2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerPlatformHandler>()
                .SetOneWayEffector(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerPlatformHandler>()
                .SetOneWayEffector(null);
        }
    }

    public void DisableEffector()
    {
        _platformEffector.surfaceArc = 0;
        Invoke("EnableEffector", enableTimer);
    }

    // Invoke("EnableOneWayCollider", enableTimer);
    
    
    private void EnableEffector()
    {
        _platformEffector.surfaceArc = 180;
    }
}