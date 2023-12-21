using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordUltimate : MonoBehaviour
{
    public int damageAmount = 1;
    public float destroyDelay = 1f;
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

        GameObject parentObject = transform.parent.gameObject;
        Destroy(parentObject, destroyDelay);
    }

    /*private void FlipSprite()
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
            }



            Destroy(gameObject);
        }
        if (other.CompareTag("Boss"))
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

    public void Destroy()
    {
        Destroy(gameObject);

    }
}
