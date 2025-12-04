using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour
{
    public Health playerHealth;
    public float healAmount;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().health += healAmount;
        }
    }
}