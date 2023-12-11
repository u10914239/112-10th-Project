using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat_Joystick_Wizard : MonoBehaviour
{
    


    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 10f;
    public float fireRate = 1f;
    public float detectionRadius = 5f;
    public LayerMask enemyLayerMask;
    

    private float nextFireTime = 0f;
   
    private Transform nearestEnemy;

    void Start()
    {
        
    }

    void Update()
    {
        

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayerMask);

        if (colliders.Length > 0)
        {
            nearestEnemy = FindNearestEnemy(colliders);
            FlipSpriteTowardsEnemy();

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
    void FlipSpriteTowardsEnemy()
    {
        if (nearestEnemy != null)
        {
            Vector3 charactorScale = transform.localScale;

            if (nearestEnemy.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
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
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
