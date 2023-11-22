using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyBoss : MonoBehaviour
{

    public enemyBossAttack enemyShooting;
    public LayerMask playerLayer;
    
    public float chaseCooldown = 1.0f;
    public float detectionRange = 10f;
    
    

    
    private NavMeshAgent agent;
    private Transform target;
    private float lastPlayerMovementTime = Mathf.NegativeInfinity;
    private bool isFacingRight = false;

    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();

        if (target == null)
        {
            // If the target is not set, try to find the player object
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }


        // Get the EnemyShooting component
        enemyShooting = GetComponent<enemyBossAttack>();

        // Example: Start shooting projectiles after a delay
        InvokeRepeating("StartShooting", 2f, 3f); // Calls StartShooting() every 3 seconds after an initial delay of 2 seconds
    }

    // Update is called once per frame
    void Update()
    {
        //SpriteFlipCheck();

        ChasePlayer();

        if (Time.time - lastPlayerMovementTime > chaseCooldown)
        {
            FindNearestPlayer();
            ChasePlayer();
        }

    }

    void StartShooting()
    {
        if (enemyShooting != null)
        {
            enemyShooting.ShootAtPlayer();
        }
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
            //agent.stoppingDistance = 0.5f; // Set a stopping distance for chasing
            agent.SetDestination(target.position);

            Vector3 direction = target.position - transform.position;
            direction.y = 0; // Ensure direction is on the XZ plane
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

            if (direction.x > 0 && !isFacingRight)
            {
                // If player is on the right side of the enemy
                // Flip the sprite to face right
                // Replace 'YourSpriteRenderer' with your actual SpriteRenderer component
                Flip();
            }
            else if (direction.x < 0 && isFacingRight)
            {
                // If player is on the left side of the enemy
                // Flip the sprite to face left
                // Replace 'YourSpriteRenderer' with your actual SpriteRenderer component
                Flip();
            }
        }
    }

    void SpriteFlipCheck()
    {
        if (agent.velocity.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (agent.velocity.x > 0 && !isFacingRight)
        {
            Flip();
        }


    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

   
}
