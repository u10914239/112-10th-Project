using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyType : MonoBehaviour
{

   
    
    public float detectionRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public LayerMask playerLayer;
    
    public float chaseCooldown = 1.0f;
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    


    private Animator anim;
    private Transform target;
    private NavMeshAgent agent;
    
    private float lastPlayerMovementTime = Mathf.NegativeInfinity;
    private bool isFacingRight = false;
    
    private EnemyHealth enemyHealth;
    private SphereCollider sphereCollider;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponentInChildren<Animator>();
        sphereCollider = GetComponentInChildren<SphereCollider>();
    }

    void Update()
    {
        if (Time.time - lastPlayerMovementTime > chaseCooldown)
        {
            FindNearestPlayer();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        }
        

        playerInSightRange = Physics.CheckSphere(transform.position, detectionRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        //if (!playerInSightRange && !playerInAttackRange) Patroling();
        
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        float velocity = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", Mathf.Abs(velocity));
        StopMoving();

    }

    void FindNearestPlayer()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, detectionRange, playerLayer);
        float closestDistance = Mathf.Infinity;
        Transform nearestPlayer = null;

        foreach (Collider playerCollider in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerCollider.transform.position);
            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                nearestPlayer = playerCollider.transform;
            }
        }

        if (nearestPlayer != null)
        {
            SetTargetPlayer(nearestPlayer);

        }
    }

    void SetTargetPlayer(Transform player)
    {
        target = player;
        lastPlayerMovementTime = Time.time;
    }

    void ChasePlayer()
    {
        if (target != null)
        {
            
            agent.SetDestination(target.position);
            


            Vector3 direction = target.position - transform.position;
            if (direction.x > 0 && !isFacingRight)
            {
                
                Flip();
            }
            else if (direction.x < 0 && isFacingRight)
            {
               
                Flip();
            }
        }
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        if (target != null && !alreadyAttacked)
        {




            alreadyAttacked = true;
            anim.SetBool("isAttacking", true);

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }



    }

    void ResetAttack()
    {
        
        alreadyAttacked = false;
        anim.SetBool("isAttacking", false);
    }

    

    




    void StopMoving()
    {
        if(enemyHealth.currentHealth <= 0)
        {

            agent.speed = 0;

        }


    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
