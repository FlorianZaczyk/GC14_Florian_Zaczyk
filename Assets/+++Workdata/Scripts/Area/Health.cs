using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
   public float health;
   public float maxHealth;
   public Image healthBar;
   public GameObject gameOverScreenContainer;

   void Start()
   {
      maxHealth = health;
   }
   
   void Update()
   {
      healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0 ,1);

      if (healthBar.fillAmount <= 0)
      {
         Destroy(gameObject);
         OpenGameOverScreen();
         
      }
   }
   
   private void OpenGameOverScreen()
   {
     
      gameOverScreenContainer.SetActive(true);
     
   }
   
}
