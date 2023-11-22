using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossBall : MonoBehaviour
{
    public int damageAmount = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // If the collider is the player, apply damage
            PlayerController playerHealth = other.GetComponent<PlayerController>();

            if (playerHealth != null)
            {
                // Apply damage to the player
                playerHealth.TakeDamage(damageAmount);
            }

            // Destroy the projectile after hitting the player
            Destroy(gameObject);
        }
    }
}
