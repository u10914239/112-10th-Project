using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{


    public Animator animator;

    //public Transform attackPoint;
    //public Vector3 halfExtents;

    public int baseDamage = 10;
    public float attackRange = 10f;

    public LayerMask enemyLayer;

    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private Collider nearestEnemyCollider;
    PlayerController movement;


    void Start()
    {
        movement = GetComponent<PlayerController>();

    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && movement.isTransformed == false)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;

            }
        }

        
    }

    void Attack()
    {
        
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        float closestDistance = Mathf.Infinity;

        foreach (Collider enemyCollider in hitEnemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemyCollider.transform.position);

            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                nearestEnemyCollider = enemyCollider;
            }
        }

        // If a nearest enemy is found, attack it
        if (nearestEnemyCollider != null)
        {
            EnemyHealth enemyHealth1 = nearestEnemyCollider.GetComponent<EnemyHealth>();
           

            if (enemyHealth1 != null)
            {
                
                enemyHealth1.TakeDamage(baseDamage);
                Debug.Log("Enemy Type 1 hit!");
            }
            
        }

    }
   
}
