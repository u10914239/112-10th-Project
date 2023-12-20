using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public int damageAmount = 1; // Set your desired damage amount here
   
    public float fireballSpeed;
    
    

    PickUp pickUp;

    void Start()
    {

        
        pickUp = GameObject.Find("Player 1").GetComponent<PickUp>();
        
    }

    void Update()
    {
        
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
                enemyHealth.TakeDamage(damageAmount);
            }

            // Destroy the arrow upon hitting the enemy
            Destroy(gameObject);


        }
    }
}
