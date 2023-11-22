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

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
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
            SlimeBossHealth enemyHealth2 = nearestEnemyCollider.GetComponent<SlimeBossHealth>();

            if (enemyHealth1 != null)
            {
                // Apply damage to EnemyHealth1
                enemyHealth1.TakeDamage(baseDamage);

                // Perform specific actions for Enemy Type 1
                // Example: Enemy Type 1 specific behavior
                Debug.Log("Enemy Type 1 hit!");
            }
            else if (enemyHealth2 != null)
            {
                // Apply damage to EnemyHealth2
                enemyHealth2.TakeDamage(baseDamage);

                // Perform specific actions for Enemy Type 2
                // Example: Enemy Type 2 specific behavior
                Debug.Log("Boss hit!");
            }
        }

    }
   /* void Attack()
    {
        animator.SetTrigger("Attack");

       
        Collider[] hitEnemies = Physics.OverlapBox(attackPoint.position, halfExtents, Quaternion.identity, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.transform.CompareTag("Boss_Slime"))
            {
                enemy.GetComponent<enemyBoss>().TakeDamage(attackDamage);

            }
            else if (enemy.transform.CompareTag("Minion_Slime"))
            {

                enemy.GetComponent<Minion>().TakeDamage(attackDamage);
            }
            
            
        }
    }*/

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        
           
        Gizmos.DrawWireCube(transform.position, attackRange);
    }*/
}
