using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { SpeedBoost, Health }
    public PowerUpType powerUpType;
    [SerializeField] private float duration = 5f;
    [SerializeField] private AudioSource powerUpSound;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                powerUpSound.Play();
                ApplyPowerUp(player);
                gameObject.SetActive(false);
            }
        }
    }

    void ApplyPowerUp(PlayerController player)
    {
        switch (powerUpType)
        {
            case PowerUpType.SpeedBoost:
                StartCoroutine(ApplySpeedBoost(player));
                break;
            case PowerUpType.Health:
                ApplyHealth(player);
                break;
        }
    }

    IEnumerator ApplySpeedBoost(PlayerController player)
    {
        float originalSpeed = player.playerData.moveSpeed;
        player.playerData.moveSpeed *= 1.5f;
        yield return new WaitForSeconds(duration);
        player.playerData.moveSpeed = originalSpeed;
    }

    void ApplyHealth(PlayerController player)
    {
        float additionalHealth = 20f;
        player.playerData.maxHealth += additionalHealth;
        player.playerData.currentHealth = Mathf.Min(player.playerData.maxHealth, player.playerData.currentHealth + additionalHealth);
        player.AddHealth(additionalHealth);
    }
}

