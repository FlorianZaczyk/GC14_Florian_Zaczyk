using System;
using UnityEngine;

public class DamagePush : MonoBehaviour
{
    private PlayerController _player;
    
    private void Awake()
    {
        _player.GetComponent<Rigidbody2D>();
    }

    public void ForcePush(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>();
            

        }
    }
    
    
}
