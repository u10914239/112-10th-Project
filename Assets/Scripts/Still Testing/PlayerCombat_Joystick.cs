using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat_Joystick : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public Vector3 halfExtents;

    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;

            }


        }

        
    }

    void Attack()
    {
        animator.SetTrigger("Attack");


        Collider[] hitEnemies = Physics.OverlapBox(attackPoint.position, halfExtents, Quaternion.identity, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {

            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;



        Gizmos.DrawWireCube(attackPoint.position, halfExtents);
    }
}
