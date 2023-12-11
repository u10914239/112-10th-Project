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
    private Transform detectedEnemy;
   

    void Start()
    {
        
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayerMask);

        if (colliders.Length > 0)
        {
            detectedEnemy = colliders[0].transform;
            FlipSpriteTowardsEnemy();

            if (Input.GetKeyDown(KeyCode.Joystick1Button5) && Time.time >= nextFireTime)
            {
                ShootFireball((detectedEnemy.position - firePoint.position).normalized);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else
        {
            detectedEnemy = null;

            /*if (Input.GetKeyDown(KeyCode.Joystick1Button5) && Time.time >= nextFireTime)
            {
                ShootFireball(transform.right);
                ShootFireball(-transform.right);
                nextFireTime = Time.time + 1f / fireRate;
            }*/
        }
    }

    void FlipSpriteTowardsEnemy()
    {
        if (detectedEnemy != null)
        {
            Vector3 charactorScale = transform.localScale;

            if (detectedEnemy.position.x > transform.position.x)
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
