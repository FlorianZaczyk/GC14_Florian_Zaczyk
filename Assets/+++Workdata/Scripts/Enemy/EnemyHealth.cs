using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    [SerializeField] public float health;
    [SerializeField] private float maxHealth;

    public float speed;
    private float _distance;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public GameObject player;
    public Slider healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthBar.maxValue = maxHealth;
        anim.SetBool(IsRunning, true);
    }

    void Update()
    {
        _distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if (_distance <= 5f)
        {
            transform.position =
                Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        Kill();
        HealthBar();
    }

    private void HealthBar()
    {
        healthBar.value = health;
    }


    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

    public void Kill()
    {
        if(health <= 0)
        {
            Debug.Log("Dead");
            Destroy(gameObject);
        }
    }
}