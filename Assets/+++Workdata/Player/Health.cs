using System;
using UnityEngine;

public class Health : MonoBehaviour
{
   public float StartingHealth = 100f;

   public float HealthPoints
   {
       get { return _HealthPoints; }
       set
       {
           _HealthPoints = Mathf.Clamp(value, 0f, 100f);

           if (_HealthPoints <= 0f)
           {
               //dead
           }
       }
   }
   
   [SerializeField]
   private float _HealthPoints = 100f;
   
    void Start()
    {
        HealthPoints = StartingHealth;
    }
}
