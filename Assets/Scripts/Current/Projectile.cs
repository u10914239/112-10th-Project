using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public int damageAmountType1 = 1; // Set your desired damage amount here
    public int damageAmountType2 = 10;
    public float fireballSpeed;
    public int multiplier = 2;
    

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
        if (other.CompareTag("EnemyType1"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damageAmountType1);
            }

            
            if (enemyHealth != null && pickUp.isHeld && pickUp != null)
            {
                
                Debug.Log("is multiply ");
                enemyHealth.TakeDamage(damageAmountType1 * multiplier);
                

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
