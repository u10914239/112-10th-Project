using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{


    public Animator anim;

    public Transform attackPoint;
    
    public int baseDamage = 10;
    public int attackDamage = 40;
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
            enemy.GetComponent<EnemyHealth>().TakeDamage(baseDamage);
        }

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;

    }
}
