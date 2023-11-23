using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float arrowSpeed = 10f;
    public int damageAmount = 10; // Set your desired damage amount here
    public float arrowLifetime = 5f; // Adjust the lifetime of the arrow

    private Vector3 targetPosition;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * arrowSpeed;
        Destroy(gameObject, arrowLifetime); // Destroy the arrow after a specified time
    }

    public void ShootTowards(Vector3 target)
    {
        targetPosition = target;
        // Rotate the arrow towards the target
        transform.LookAt(targetPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the arrow collided with an enemy
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damageAmount);
            }

            // Destroy the arrow upon hitting the enemy
            Destroy(gameObject);
        }
    }
}
