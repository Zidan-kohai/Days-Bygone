using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    public int health;
    public int enemyDamage;
    public int coinEnemy;
    public float speed;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public float damage;

    public Player player;
    public PlayerHealth playerHealth;
    public Animator anim;

    private void Update()
    {
        if (health <= 0)
        {
            player.coin += coinEnemy;
            Destroy(gameObject);
        }

        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Fance"))
        {
            if(timeBtwAttack <= 0)
            {
                speed = 0;
                anim.SetTrigger("attack");
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    public void OnEnemyAttack()
    {
        playerHealth.healthAmount -= damage;
        timeBtwAttack = startTimeBtwAttack;
    }

    public void TakeDamage()
    {
        health -= enemyDamage;
    }
}