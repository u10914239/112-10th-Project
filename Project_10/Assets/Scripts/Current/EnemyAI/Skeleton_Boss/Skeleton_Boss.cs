using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton_Boss : MonoBehaviour
{
    //public Transform player;
    public float detectionRange;

    public LayerMask playerLayer;
    public bool isFlipped = false;
    //public bool isFacingRight;

    public Transform target;
    public float lastPlayerMovementTime  = Mathf.NegativeInfinity;

    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > target.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < target.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    /* public void Flip()
     {
         isFacingRight = !isFacingRight;
         transform.Rotate(0f, 180f, 0f);
         Vector3 newScale = transform.localScale;
         newScale.x *= -1;
         transform.localScale = newScale;
         EnemyHealthBar.FlipR = true;
     }*/

    public void FindNearestPlayer()
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
}
