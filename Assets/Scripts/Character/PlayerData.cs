using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed = 5f;
    public float maxSpeed = 10f;
    public float jumpForce = 15f;
    public float maxHealth = 200;
    public float currentHealth = 100;
    public float damage = 5;

    public void ResetValue()
    {
        moveSpeed = 100f;
        maxSpeed = 200f;
        jumpForce = 15f;
        maxHealth = 200;
        currentHealth = 100;
        damage = 5;
    }
}
