using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range;
    public Transform centrePoint;

    public Animator animator;

    public Transform player;

    public LayerMask whatIsPlayer;
    public float lookRadius, attackRadius;
    public bool playerInSightRange, playerInAttackRange;
    public float timeBetweenAttack;

    Vector3 scale;

    
    bool isAttacking;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        

    }

    private void Update()
    {
        scale = transform.localScale;

        playerInSightRange = Physics.CheckSphere(transform.position, lookRadius, whatIsPlayer);
        playerInAttackRange= Physics.CheckSphere(transform.position, attackRadius, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange)Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();

       

        if (agent.velocity.x > 0|| player.position.x - transform.position.x >= 0)
        {
           
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

       
        
       


        

        
        
    }

    private void Patrolling()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);

            }
        }
        animator.SetFloat("Speed", 1);
        animator.SetBool("isAttacking", false);
    }

    private void Chasing()
    {
        
        agent.SetDestination(player.position);
        animator.SetFloat("Speed", 1);
        animator.SetBool("isAttacking", false);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);


        if (!isAttacking)
        {
            animator.SetBool("isAttacking",true);
            animator.SetFloat("Speed", 0);

            isAttacking = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
        

    }

    private void ResetAttack()
    {
        isAttacking = false;
        
    }

    
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
