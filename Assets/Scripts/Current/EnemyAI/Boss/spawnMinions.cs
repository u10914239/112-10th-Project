using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnMinions : MonoBehaviour
{
    public GameObject minionPrefab; // Prefab of the minion to be spawned
    public Transform spawnPoint; // Point where the minions will be spawned
    public int numberOfMinions = 10; // Number of minions to spawn at once
    public float minDistanceBetweenMinions = 2f; // Minimum distance between minions

    private bool minionsSpawned = false;
    SlimeBossHealth bossHealth;

    void Start()
    {
       bossHealth = GetComponent<SlimeBossHealth>();
    }

    void FixedUpdate()
    {
        if (bossHealth.currentHealth <= bossHealth.maxHealth / 2 && !minionsSpawned)
        {
            SpawnMinions();
            minionsSpawned = true;
        }


    }


    void SpawnMinions()
    {
        for (int i = 0; i < numberOfMinions; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // Spawn a minion at the calculated spawn point
            GameObject newMinion = Instantiate(minionPrefab, spawnPosition, spawnPoint.rotation);

            // Optionally, if you want to set the spawner as the parent of the minions
            // newMinion.transform.parent = transform;

            // Attach EnemyAI script to the spawned minion
            Minion minionAI = newMinion.GetComponent<Minion>();
            if (minionAI != null)
            {
                minionAI.enabled = true; // Enable the EnemyAI script for navigation
            }
            else
            {
                Debug.LogError("Minion prefab is missing EnemyAI component.");
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition = spawnPoint.position + Random.insideUnitSphere * minDistanceBetweenMinions;
        spawnPosition.y = spawnPoint.position.y; // Keep the same Y position as the spawn point
        return spawnPosition;
    }
}
