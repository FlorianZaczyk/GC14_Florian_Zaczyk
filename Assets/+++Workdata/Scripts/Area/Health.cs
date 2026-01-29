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
   private PlayerController _playerController;


   private void Awake()
   {
      _playerController = GetComponent<PlayerController>();
   }

   void Start()
   {
      maxHealth = health;
   }
   
   void Update()
   {
      healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0 ,1);

      if (healthBar.fillAmount <= 0)
      {
         _playerController.Death();
         //Destroy(gameObject);
         //OpenGameOverScreen();
         
      }
   }
   
   private void OpenGameOverScreen()
   {
     
      gameOverScreenContainer.SetActive(true);
     
   }
   
}
