using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyType : MonoBehaviour
{
    public float chasingRange = 5f;
    public float chaseCooldown = 1.0f;
    public float detectionRange = 10f;
    public LayerMask playerLayer;

    private Transform target;
    private NavMeshAgent agent;
    private float lastPlayerMovementTime = Mathf.NegativeInfinity;
    private bool isFacingRight = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

    }

    void Update()
    {
        if (Time.time - lastPlayerMovementTime > chaseCooldown)
        {
            FindNearestPlayer();
            ChasePlayer();
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
            
            agent.SetDestination(target.position);

            Vector3 direction = target.position - transform.position;
            direction.y = 0; 
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

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
    }
}
