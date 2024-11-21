using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private float particleLifetime = 2f;
    private float currentHealth;
    public void Set(EnemyData enemyData)
    {
        this.enemyData = enemyData;
    }
    void Start()
    {
        currentHealth = enemyData.maxHealth;
        UpdateHealthBar();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, enemyData.maxHealth);
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / enemyData.maxHealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(enemyData.damage);
            }
        }
    }
    void Die()
    {

        if (healthBar != null)
        {
            Destroy(healthBar);
        }

        if (deathEffect != null)
        {
            GameObject particleObject = Instantiate(deathEffect.gameObject, transform.position, Quaternion.identity);
            ParticleSystem particles = particleObject.GetComponent<ParticleSystem>();

            if (particles != null)
            {
                Destroy(particleObject, particleLifetime);
            }
        }
        Destroy(gameObject);
    }
}

