using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowEnemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public float range;
    public Transform centrePoint;

    

    public GameObject player;

    public SpriteRenderer Slime;

    public Animator slime;

    public LayerMask whatIsPlayer;
    public float lookRadius, attackRadius;
    public bool playerInSightRange, playerInAttackRange;
    Vector3 scale;

    public int findRange;
    bool isAttacking;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        

    }

    private void Update()
    {
         scale = transform.localScale;

        playerInSightRange = Physics.CheckSphere(transform.position, lookRadius, whatIsPlayer);
        playerInAttackRange= Physics.CheckSphere(transform.position, attackRadius, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange)Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();

       

        if (enemy.velocity.x > 0|| player.transform.position.x - transform.position.x >= 0)
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
        if (enemy.remainingDistance <= enemy.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                enemy.SetDestination(point);

            }
        }
        

    }

    private void Chasing()
    {
        
        enemy.SetDestination(player.transform.position);
        
        
    }

    private void Attacking()
    {
        enemy.SetDestination(transform.position);

        isAttacking = true;



    }

    public void SetTarget(Vector3 point)
    {
        if (isAttacking)
        {
            enemy.stoppingDistance = findRange;
        }
        else
        {
            enemy.stoppingDistance = 0;
        }


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
