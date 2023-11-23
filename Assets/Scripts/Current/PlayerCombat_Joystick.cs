using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat_Joystick : MonoBehaviour
{
    public Animator animator;

    public GameObject arrowPrefab;
    public Transform attackPoint;
    public Vector3 halfExtents;

    public LayerMask enemyLayer;

    public float shootingDistance = 10f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    PlayerController_Joystick movement;

    void Start()
    {
        movement = GetComponent<PlayerController_Joystick>();

    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button5) && movement.isTransformed==false)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;

            }


        }

        
    }

    void Attack()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootingDistance, enemyLayer);

        float nearestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (Collider collider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = collider.gameObject;
            }
        }

        if (nearestEnemy != null)
        {
            // Calculate direction towards the nearest enemy
            Vector3 direction = nearestEnemy.transform.position - transform.position;

            // Instantiate arrowPrefab and set its initial position and rotation
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.LookRotation(direction));
            Projectile arrowScript = arrow.GetComponent<Projectile>();

            if (arrowScript != null)
            {
                arrowScript.ShootTowards(nearestEnemy.transform.position);
            }
            // You'll need to have a script (e.g., ArrowScript) on your arrow prefab to handle its behavior.
        }
        

        

        
        

        
    }
}
