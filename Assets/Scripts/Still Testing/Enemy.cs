using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private SimpleFlash flashEffect;
    public Animator animator;

    public int maxHealth;
    int currentHealth;
    public LayerMask playerLayer;
    
    public float detectionRange = 10f;
    public float patrolRadius = 10f; 
    private int currentPatrolIndex = 0;
    public float chaseCooldown = 1.0f;

    private NavMeshAgent agent;
    private Transform targetPlayer;
    private Vector3 patrolPoint;

    private bool isPatrolling = true;
    private bool isFacingRight = false; // Flag to track the enemy's facing direction
    private float lastPlayerMovementTime = Mathf.NegativeInfinity;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0f;
        SetRandomPatrolPoint();
    }

    void Update()
    {

        
        if (isPatrolling)
        {
            Patrol();
        }
        else
        {
            if (Time.time - lastPlayerMovementTime > chaseCooldown)
            {
                FindNearestPlayer();
                ChasePlayer();
            }
        }

        if (targetPlayer == null)
        {
            // If there's no player, resume patrolling to a new random point
            isPatrolling = true;
        }
    }

    

    void Patrol()
    {
        // Move the enemy towards the patrol point
        agent.SetDestination(patrolPoint);

        // If the enemy reaches the patrol point, set a new random point
        if (Vector3.Distance(transform.position, patrolPoint) < 0.5f)
        {
            SetRandomPatrolPoint();
        }

        // Check for nearby players within the detection range
        FindNearestPlayer();
        if (targetPlayer != null)
        {
            isPatrolling = false; // Stop patrolling when a player is detected
        }
    }

    void SetRandomPatrolPoint()
    {
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);
        patrolPoint = transform.position + new Vector3(randomX, 0f, randomZ);
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
            if (nearestPlayer.position.x > transform.position.x && !isFacingRight)
            {
                Flip();
            }
            else if (nearestPlayer.position.x < transform.position.x && isFacingRight)
            {
                Flip();
            }
        }
    }

    void SetTargetPlayer(Transform player)
    {
        targetPlayer = player;
        lastPlayerMovementTime = Time.time;
    }

    void ChasePlayer()
    {
        if (targetPlayer != null)
        {
            agent.stoppingDistance = 0.5f; // Set a stopping distance for chasing
            agent.SetDestination(targetPlayer.position);
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }

    public void TakeDamage(int damage)
    {
        flashEffect.Flash();
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        if (currentHealth <= 0)
        {

            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy Died");

    }
}
