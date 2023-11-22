using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBossAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab of the projectile to be fired
    public Transform firePoint; // Point where the projectile will be spawned
    public float projectileForce = 10f; // Force applied to the projectile
    public float projectileWeight = 2f; // Weight of the projectile affecting its gravity
    public float projectileLifetime = 2f; // Time before the projectile is destroyed
    public float fireRate = 1f; // Number of shots per second
    public LayerMask playerLayer;
    public float detectionRange = 10f;

    private Transform target; // Reference to the target's transform
    private float nextFireTime = 0f; // Time of the next allowed shot
    private float lastPlayerMovementTime = Mathf.NegativeInfinity;

    void Start()
    {
       
    }

    void Update()
    {
        FindNearestPlayer();


        
    }
    void FIxedUpdate()
    {
        if (Time.time >= nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + 1f / fireRate; // Calculate next allowed shot time
        }



    }

    public void ShootAtPlayer()
    {
        if (target != null)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Calculate direction towards the player
                Vector3 direction = (target.position - firePoint.position).normalized;

                // Apply force to the projectile towards the player
                rb.AddForce(direction * projectileForce, ForceMode.Impulse);

                // Apply weight to the projectile
                rb.mass = projectileWeight;

                // Enable gravity for the projectile
                rb.useGravity = true;

                // Destroy the projectile after projectileLifetime seconds
                Destroy(newProjectile, projectileLifetime);
            }
            else
            {
                Debug.LogError("Projectile prefab is missing Rigidbody component.");
            }
        }
    }

    void FindNearestPlayer()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, detectionRange, playerLayer);
        float closestDistance = Mathf.Infinity;
        Transform nearestPlayer = null;

        foreach (Collider playerCollider in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerCollider.transform.position);
            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                nearestPlayer = playerCollider.transform;
            }
        }

        if (nearestPlayer != null)
        {
            SetTargetPlayer(nearestPlayer);

        }
    }

    void SetTargetPlayer(Transform player)
    {
        target = player;
        lastPlayerMovementTime = Time.time;
    }
}
