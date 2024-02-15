using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffUltimate : MonoBehaviour
{

    public int damageAmount = 1;

    SpriteRenderer spriteRenderer;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

   
   

    void Update()
    {
        //FlipSprite();
    }

   /* private void FlipSprite()
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
    }*/

    void OnTriggerStay(Collider other)
    {
        // Check if the arrow collided with an enemy
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damageAmount);
                PlayerCombat.MagicAmount += 5;
            }



            //Destroy(gameObject);
        }
        if (other.CompareTag("Boss"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damageAmount);
                PlayerCombat.MagicAmount += 5;
            }

            // Destroy the arrow upon hitting the enemy
           //Destroy(gameObject);


        }
    }

    public void Destroy()
    {
        Destroy(gameObject);

    }
}
