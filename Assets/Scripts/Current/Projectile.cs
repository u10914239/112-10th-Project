using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public int damageAmountType1 = 10; // Set your desired damage amount here
    public int damageAmountType2 = 1;
    public float fireballSpeed;

    private Rigidbody rb;

    void Start()
    {
        /*rb = GetComponent<Rigidbody>();*/

       /* rb.velocity = transform.right * fireballSpeed;*/
    }

    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        // Check if the arrow collided with an enemy
        if (other.CompareTag("EnemyType1"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damageAmountType1);
            }

            // Destroy the arrow upon hitting the enemy
            Destroy(gameObject);
        }
        if (other.CompareTag("EnemyType2"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damageAmountType2);
            }

            // Destroy the arrow upon hitting the enemy
            Destroy(gameObject);


        }
    }
}
