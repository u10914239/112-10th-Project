using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat_Joystick_Wizard : MonoBehaviour
{

    public LayerMask enemyLayerMask;
    public float detectionRange = 10f; // Range within which the player should face the enemy
    

    public Transform firePoint;
    public GameObject fireballPrefab;
    public float fireballSpeed = 10f;
    public float fireRate = 1f;

    private float nextFireTime = 0f;
    private Transform playerTransform;
    private SpriteRenderer playerSpriteRenderer;
    private Transform currentTarget;

    void Start()
    {
        playerTransform = transform;
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (currentTarget == null || !currentTarget.gameObject.activeSelf)
        {
            currentTarget = GetNearestEnemy();
        }
        else
        {

            float distanceToTarget = Vector3.Distance(playerTransform.position, currentTarget.position);
            if (distanceToTarget < detectionRange)
            {
                // Determine the direction from player to current target enemy
                Vector3 direction = currentTarget.position - playerTransform.position;

                // Flip the player's sprite based on the direction
                if (direction.x > 0) // Enemy is to the right of the player
                {
                    playerTransform.localScale = new Vector3(1f, 1f, 1f); // No flip (original scale)
                }
                else if (direction.x < 0) // Enemy is to the left of the player
                {
                    playerTransform.localScale = new Vector3(-1f, 1f, 1f); // Flip the sprite horizontally
                }


            }
                

            // Check if the current target is beyond detection range
            
            if (distanceToTarget > detectionRange)
            {
                currentTarget = null; // Set the target to null if it's out of range
            }
        }

        if (currentTarget != null)
        {
            // Attack - Shoot projectile
            if (Input.GetKeyDown(KeyCode.Joystick1Button5) && Time.time >= nextFireTime)
            {
                ShootFireball((currentTarget.position - firePoint.position).normalized);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    // Function to find the nearest active enemy
    private Transform GetNearestEnemy()
    {
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        Collider[] hitEnemies = Physics.OverlapSphere(playerTransform.position, detectionRange, enemyLayerMask);

        foreach (Collider enemyCollider in hitEnemies)
        {
            Transform enemy = enemyCollider.transform;

            float distanceToEnemy = Vector3.Distance(playerTransform.position, enemy.position);

            // Check if the enemy is closer than the current nearest enemy
            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }




    void ShootFireball(Vector3 direction)
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.velocity = direction * fireballSpeed;

        Destroy(fireball, 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    
}
