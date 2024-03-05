using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BoneBoss : MonoBehaviour
{

    public float detectionRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public LayerMask playerLayer;

    public float chaseCooldown = 1.0f;
    public float timeBetweenAttacks;
    public bool alreadyAttacked;


    private Rigidbody rb;
    private Animator anim;
    private Transform target;
    private NavMeshAgent agent;

    private float lastPlayerMovementTime = Mathf.NegativeInfinity;
    private bool isFacingRight = false;
    
    private EnemyHealth enemyHealth;

    [SerializeField] int AttackCount;
    bool AttackFaster = false;
    public int JumpCountLimit = 4;
   
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        
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
            anim.SetBool("isAttacking", false);
            

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
        if(enemyHealth.Round2 && !AttackFaster)
        {
            timeBetweenAttacks = timeBetweenAttacks / 2;
            JumpCountLimit = 2;
            AttackFaster = true;
        }

        if (AttackCount<=JumpCountLimit && target != null && !alreadyAttacked)
        {
            print("Attack!!!!!!!!!");
            AttackCount++;
            alreadyAttacked = true;
            anim.SetBool("isAttacking", true);

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        if(AttackCount>JumpCountLimit && target != null && !alreadyAttacked)
        {
            //Jump Attack
            print("Jump Attack!!!!!!!!!");

            AttackCount = 0;
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

    public IEnumerator KnockBack()
    {
        agent.isStopped = true;

        yield return new WaitForSeconds(0.2f); //Only knock the enemy back for a short time     

        rb.velocity = Vector3.zero;
        agent.isStopped = false;

    }






    void StopMoving()
    {
        if (enemyHealth.currentHealth <= 0)
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
        EnemyHealthBar.FlipR = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


}
