using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public int damageAmount = 1; // Set your desired damage amount here
   
    

    SpriteRenderer spriteRenderer;
    Rigidbody rb;
    

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (rb != null && spriteRenderer != null)
        {
            if (rb.velocity.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
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


            
            Destroy(gameObject);
        }
        if (other.CompareTag("Boss"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy

                if(EnemyHealth.shieldKind ==0 || EnemyHealth.shieldKind ==2)
                {
                    enemyHealth.TakeDamage(damageAmount);
                }

                
            }

            // Destroy the arrow upon hitting the enemy
            Destroy(gameObject);


        }
    }
}
