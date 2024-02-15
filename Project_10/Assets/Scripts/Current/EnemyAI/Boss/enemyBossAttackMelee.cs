using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBossAttackMelee : MonoBehaviour
{
    public int damageAmount;
    EnemyBoss enemyBoss;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyBoss = GetComponentInParent<EnemyBoss>();
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealthBar playerHealth = other.GetComponent<PlayerHealthBar>();



        if (other.tag == "Player 1")
        {

            if (playerHealth != null)
            {
                // Apply damage to the enemy
                playerHealth.TakeDamage(damageAmount);
                

            }

        }

        if (other.tag == "Player 2")
        {

            if (playerHealth != null)
            {
                // Apply damage to the enemy
                playerHealth.TakeDamage(damageAmount);


            }

        }

    }
}
