using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyType enemyType;

    // Start is called before the first frame update
    void Start()
    {
        enemyType = GetComponentInParent<EnemyType>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
                
        if (other.tag == "Player")
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Apply damage to the enemy
                playerHealth.TakeDamage(1);

            }

        }
                
    }
}
