using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBossAttackMelee : MonoBehaviour
{
    enemyBoss enemyBoss;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyBoss = GetComponentInParent<enemyBoss>();
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();



        if (other.tag == "Player")
        {

            if (playerHealth != null)
            {
                // Apply damage to the enemy
                playerHealth.TakeDamage(1);
                Debug.Log("Hit");

            }

        }

    }
}
