using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Boss_Attack: MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public float timeBetweenAttacks;
    public int attackCount = 0;
    public LayerMask attackMask;

    bool alreadyAttacked;


    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider[] colInfo = Physics.OverlapSphere(pos, attackRange, attackMask);

        foreach (Collider col in colInfo)
        {
            if (!alreadyAttacked)
            {
                col.gameObject.GetComponent<PlayerHealthBar>().TakeDamage(attackDamage);
                


            }

        }


    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
        attackCount++;


    }

    public void ResetAttackCount()
    {
        attackCount = 0;
    }


    public void EnragedAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(enragedAttackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
