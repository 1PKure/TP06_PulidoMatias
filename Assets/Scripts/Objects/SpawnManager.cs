using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager objectPoolManager;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnRate = 2f;                 
    [SerializeField] private float spawnDistanceAhead = 10f;
    [SerializeField] private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            float randomChance = Random.Range(0f, 1f);

            if (randomChance > 0.8f)
            {
                SpawnPowerUp();
            }
            else
            {
                SpawnObstacle();
            }

            nextSpawnTime = Time.time + spawnRate + Random.Range(0.5f, 1.5f);
        }
    }

    void SpawnObstacle()
    {
        float randomY = Random.Range(-1f, 0.5f);
        Vector3 spawnPosition = new Vector3(player.position.x + spawnDistanceAhead, randomY, 0);

        objectPoolManager.SpawnFromPool("Obstacle", spawnPosition);
    }

    void SpawnPowerUp()
    {
        float randomY = Random.Range(-1f, 1f);
        Vector3 spawnPosition = new Vector3(player.position.x + spawnDistanceAhead, randomY, 0);

        objectPoolManager.SpawnFromPool("PowerUp", spawnPosition);
    }
}

