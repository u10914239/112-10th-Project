using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{


    public Animator anim;

    public Transform attackPoint;

    public int damageAmountType1 = 10;
    public int damageAmountType2 = 1;
    public float attackRange = 10f;
    public bool isAttacking;

    public LayerMask enemyLayer;


    public float attackRate = 2f;
    float nextAttackTime = 0f;

    
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

                StartCoroutine(Attack());
                nextAttackTime = Time.time + 1f / attackRate;
               

            }
        }

        
    }

    
   
   

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);


    }

    IEnumerator Attack()
    {

        isAttacking = true;

        anim.SetTrigger("Attack");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
          

            if (enemy.CompareTag("EnemyType1"))
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    // Apply damage to the enemy
                    enemyHealth.TakeDamage(damageAmountType1);
                }

            }

            if (enemy.CompareTag("EnemyType2"))
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    // Apply damage to the enemy
                    enemyHealth.TakeDamage(damageAmountType2);
                }

            }
        }

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;

    }
}
