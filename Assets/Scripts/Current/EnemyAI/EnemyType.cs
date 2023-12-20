using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyType : MonoBehaviour
{
    public Transform centrePoint;
    public float patrolRange;

    public float detectionRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public LayerMask playerLayer;
    
    public float chaseCooldown = 1.0f;
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    public bool isFacingRight = false;


    private Rigidbody rb;
    private Animator anim;
    private Transform target;
    private NavMeshAgent agent;
    private EnemyHealth enemyHealth;
    
    private float lastPlayerMovementTime = Mathf.NegativeInfinity;

    private float lastPatrollTIme= Mathf.NegativeInfinity;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        centrePoint = GameObject.Find("Enemy Generator").GetComponent<Transform>();
    }

    void Update()
    {
        

        playerInSightRange = Physics.CheckSphere(transform.position, detectionRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (Time.time - lastPlayerMovementTime > chaseCooldown)
        {
            FindNearestPlayer();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        }
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        float velocity = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", Mathf.Abs(velocity));

        


        StopMoving();

    }

    void FixedUpdate()
    {
        


    }

    public IEnumerator KnockBack()
    {
        agent.isStopped = true;

        yield return new WaitForSeconds(0.2f); //Only knock the enemy back for a short time     

        rb.velocity = Vector3.zero;
        agent.isStopped = false;

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


    private void Patroling()
    {
        anim.SetBool("isAttacking", false);

        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, patrolRange, out point)) //pass in our centre point and radius of area
            {
                if (Time.time - lastPatrollTIme > chaseCooldown)
                {
                   
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                    agent.SetDestination(point);

                }
                    

            }
        }
       
        
        if (agent.velocity.x > 0 && !isFacingRight)
        {

            Flip();
        }
        else if (agent.velocity.x < 0 && isFacingRight)
        {

            Flip();
        }
        
        
    }

   

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        UnityEngine.AI.NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    void ChasePlayer()
    {
        if (enemyHealth.currentHealth <= 0)
            return;

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
