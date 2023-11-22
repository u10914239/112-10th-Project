using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public LayerMask playerLayer;
    public float detectionRange = 10f;

    private NavMeshAgent agent;
    private float lastPlayerMovementTime = Mathf.NegativeInfinity;
    private bool isFacingRight = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        



    }

    void Update()
    {
        FindNearestPlayer();
        //SpriteFlipCheck();


        if (target != null)
        {
            // Set the destination of the enemy to the player's position
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
            else if(direction.x < 0 && isFacingRight)
            {
                // If player is on the left side of the enemy
                // Flip the sprite to face left
                // Replace 'YourSpriteRenderer' with your actual SpriteRenderer component
                Flip();
            }
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
