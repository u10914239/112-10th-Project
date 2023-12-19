using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealthBar playerHealth = other.gameObject.GetComponent<PlayerHealthBar>();
        if (other.tag == "Player 1")
        {
            
            if (playerHealth != null)
            {
                // Apply damage to the enemy
                playerHealth.TakeDamage(1);

            }

        }


        if (other.tag == "Player 2")
        {
            
            if (playerHealth != null)
            {
                // Apply damage to the enemy
                playerHealth.TakeDamage(1);

            }

        }
    }
}
