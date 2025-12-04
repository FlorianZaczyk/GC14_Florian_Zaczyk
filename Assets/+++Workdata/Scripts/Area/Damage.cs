using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public Health playerHealth;
    public float damage;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().health -= damage;
        }
    }
    
}
