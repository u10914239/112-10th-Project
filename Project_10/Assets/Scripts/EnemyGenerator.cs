using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab you want to spawn
    public float spawnInterval = 2f; // Time interval between spawns
    public Transform spawnPoint; // The original spawn point for enemies
    public float spawnRadius = 5f; // Radius of the spawn area
    public int maxEnemies = 10; // Maximum number of enemies to spawn

    private float timer = 0f;
    private int currentEnemies = 0;

    void Start()
    {
        //EnemyHealth.OnDestroyed += HandleEnemyDestroyed;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            timer = 0f;
        }
        
    }

    void SpawnEnemy()
    {
        if (spawnPoint == null || enemyPrefab == null)
        {
            Debug.LogError("Spawn point or enemy prefab not set!");
            return;
        }

        Vector2 randomCirclePos = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = spawnPoint.position + new Vector3(randomCirclePos.x, 0f, randomCirclePos.y);

        Instantiate(enemyPrefab, spawnPos, spawnPoint.rotation);
        currentEnemies++;
    }

    void HandleEnemyDestroyed()
    {
        currentEnemies--;
        // Add any additional logic you need when an enemy is destroyed
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }


}
