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
    public float knockbackForce = 10f;
    public int multiplier = 2;
    public bool isAttacking;
    public LayerMask enemyLayer;



    public float attackRate = 2f;
    private float attackSpeed = 1f;
    private float currentSpeed;

    PickUp_Joystick pickUpJoy;
    PlayerController movement;


    void Start()
    {
        movement = GetComponentInParent<PlayerController>();
        pickUpJoy = GameObject.Find("Player 2").GetComponent<PickUp_Joystick>();
        currentSpeed = movement.speed;
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
    void FixedUpdate()
    {
        
        if(isAttacking)
        {
            movement.speed = attackSpeed;

        }
        else if (!isAttacking)
        {
            movement.speed = currentSpeed;
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        
        isAttacking = true;
        
    }
    void ResetAttack()
    {
        
        isAttacking = false;
       
    }
   

    void OnTriggerEnter(Collider other)
    {
       
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        EnemyType enemyAI = other.gameObject.GetComponent<EnemyType>();

       


        if (other.tag == "EnemyType1" && isAttacking)
        {
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damageAmountType1);
                isAttacking = false;
            }

            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 knockBack = (other.transform.position - transform.position).normalized;

                rb.AddForce(knockBack * knockbackForce, ForceMode.Impulse);
                StartCoroutine(enemyAI.KnockBack());
            }
        }

        if (other.tag == "EnemyType2" && isAttacking)
        {
            

            if (enemyHealth != null)
            {
                
                enemyHealth.TakeDamage(damageAmountType2);
                isAttacking = false;

                

            }
            if(enemyHealth != null && pickUpJoy.isHeld && pickUpJoy!= null)
            {
                Debug.Log("is multiply ");
                enemyHealth.TakeDamage(damageAmountType2 * multiplier);
                isAttacking = false;

            }

            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 knockBack = (other.transform.position - transform.position).normalized;
                
                rb.AddForce(knockBack * knockbackForce, ForceMode.Impulse);
                StartCoroutine(enemyAI.KnockBack());
            }
            

        }



    }
   
   

    
}
