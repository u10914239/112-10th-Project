using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator anim;
    public Transform attackPoint;
    public GameObject laserPrefab;
    public int damageAmount;
    public float attackRange = 10f;
    public float knockbackForce = 10f;
   
    public bool isAttacking;
    public LayerMask enemyLayer;
    public float radius;
    public float attackRate = 2f;
    


    private float currentSpeed;
   

    PickUp_Joystick pickUpJoy;
    PlayerController movement;

    //public int multiplier = 2;

    void Start()
    {
        movement = GetComponentInParent<PlayerController>();
        pickUpJoy = GameObject.Find("Player 2").GetComponent<PickUp_Joystick>();
        currentSpeed = movement.speed;
    }

    void Update()
    {
      
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && movement.isTransformed == false && !isAttacking && !movement.isHoldingGod)
        {
            
            Attack();
            Invoke(nameof(ResetAttack), attackRate);

           
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && movement.isTransformed == false && !isAttacking && movement.isHoldingGod)
        {
            GodAttack();
            Invoke(nameof(ResetAttack), attackRate);

        }
        
    }
    void FixedUpdate()
    {
        
        
    }

    void Attack()
    {
        
        
        anim.SetTrigger("Attack");
        movement.speed = movement.speed * 0.5f;
        isAttacking = true;
        
        
        
        
        
    }

    void GodAttack()
    {

        anim.SetTrigger("GodAttack");
        movement.speed = movement.speed * 0.5f;
        isAttacking = true;
    }

    void ResetAttack()
    {
        movement.speed = currentSpeed;
        isAttacking = false;
       
    }
    void ShootLaser()
    {
        GameObject laser = Instantiate(laserPrefab, attackPoint.position, transform.rotation);

    }



    public void DealDamage()
    {

        Collider[] enemy = Physics.OverlapSphere(attackPoint.transform.position, radius, enemyLayer);

        foreach (Collider enemyGameObject in enemy)
        {

            Rigidbody rb = enemyGameObject.GetComponent<Rigidbody>();
            EnemyHealth enemyHealth = enemyGameObject.GetComponent<EnemyHealth>();

            if (enemyGameObject.CompareTag("Enemy"))
            {
                

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageAmount);

                    if (rb != null)
                    {
                        EnemyType enemyAI = enemyGameObject.GetComponent<EnemyType>();
                        Vector3 knockBack = (enemyGameObject.transform.position - transform.position).normalized;
                        rb.AddForce(knockBack * knockbackForce, ForceMode.Impulse);
                        StartCoroutine(enemyAI.KnockBack());
                    }
                }

            }

            if (enemyGameObject.CompareTag("Boss"))
            {


                if (enemyHealth != null)
                {
                    if(EnemyHealth.shieldKind ==0 || EnemyHealth.shieldKind ==1)
                    {
                        enemyHealth.TakeDamage(damageAmount);
                    }

                    

                    if (rb != null)
                    {
                        EnemyBoss bossAI = enemyGameObject.GetComponent<EnemyBoss>();
                        Vector3 knockBack = (enemyGameObject.transform.position - transform.position).normalized;
                        rb.AddForce(knockBack * knockbackForce, ForceMode.Impulse);
                        StartCoroutine(bossAI.KnockBack());
                    }
                }

            }





        }
    }

    private void OnDrawGizmos()
    {


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
