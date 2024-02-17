using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossBall : MonoBehaviour
{
    public int damageAmount = 10;
    public float ballLifetime = 2f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();


            if (playerHealth.enabled == true)
            {
                
                playerHealth.TakeDamage(damageAmount);

                Destroy(gameObject);
            }

            
            
        }
        else
        {
            Destroy(gameObject, ballLifetime);


        } 
    }
}
