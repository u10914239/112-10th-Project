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
    
    
    
    PlayerController movement;


    void Start()
    {
        movement = GetComponentInParent<PlayerController>();

    }

    void Update()
    {
      
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && movement.isTransformed == false)
        {

            Attack();
            isAttacking = true;
                
            Invoke(nameof(ResetAttack), attackRate);

        }
          
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        movement.enabled = false;
        isAttacking = true;
        
    }
    void ResetAttack()
    {
        movement.enabled = true;
        isAttacking = false;
       
    }
   

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyType1" && isAttacking)
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damageAmountType1);
                isAttacking = false;
            }

        }

        if (other.tag == "EnemyType2" && isAttacking)
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damageAmountType2);
                isAttacking = false;
            }

        }
    }
   
   

    
}
