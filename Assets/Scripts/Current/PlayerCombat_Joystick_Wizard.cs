using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat_Joystick_Wizard : MonoBehaviour
{
    


    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 10f;
    public float fireRate = 1f;
    public float detectionRange = 5f;
    public LayerMask enemyLayerMask;
    

    private float nextFireTime = 0f;
    private SpriteRenderer playerSpriteRenderer;
    private Transform nearestEnemy;

    void Start()
    {
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, enemyLayerMask);

        if (colliders.Length > 0)
        {
            nearestEnemy = FindNearestEnemy(colliders);
            FlipWhenFindEnemy();

            if (Input.GetKeyDown(KeyCode.Joystick1Button5) && Time.time >= nextFireTime)
            {
                
                ShootFireball((nearestEnemy.position - firePoint.position).normalized);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else
        {
            nearestEnemy = null;

            if (Input.GetKeyDown(KeyCode.Joystick1Button5) && Time.time >= nextFireTime)
            {
                /*ShootFireball(transform.right);
                ShootFireball(-transform.right);
                nextFireTime = Time.time + 1f / fireRate;*/

                Debug.Log("No enemy");
            }
        }


        
    }

    private void FlipWhenFindEnemy()
    {
        float distanceToEnemy = Vector3.Distance(this.transform.position, nearestEnemy.position);

        // Check if the enemy is within the detection range
        if (distanceToEnemy <= detectionRange)
        {
            // Determine the direction from player to enemy
            Vector3 direction = nearestEnemy.position - this.transform.position;

            // Flip the player's sprite based on the direction
            if (direction.x > 0) // Enemy is to the right of the player
            {
                this.transform.localScale = new Vector3(1f, 1f, 1f); // No flip
            }
            else if (direction.x < 0) // Enemy is to the left of the player
            {
                this.transform.localScale = new Vector3(-1f, 1f, 1f); // Flip the sprite horizontally
            }
        }


    }

    Transform FindNearestEnemy(Collider[] enemies)
    {
        float minDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        Vector3 currentPosition = transform.position;

        foreach (Collider enemyCollider in enemies)
        {
            float distance = Vector3.Distance(currentPosition, enemyCollider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemyCollider.transform;
            }
        }

        return closestEnemy;
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
