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

    public int Health;
    public bool FireCoolDown = false;
    public SpriteRenderer color;

    public SpriteRenderer SpriteRenderer;
    public Sprite Blood1,Blood2,Blood3;

    Vector3 scale;

    
    bool isAttacking;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        color = gameObject.GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        scale = transform.localScale;

        playerInSightRange = Physics.CheckSphere(transform.position, lookRadius, whatIsPlayer);
        playerInAttackRange= Physics.CheckSphere(transform.position, attackRadius, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange)Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();

       

        if (agent.velocity.x > 0||player.position.x-transform.position.x>=0)
        {
           
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

       color = gameObject.GetComponent<SpriteRenderer>();
        
       if(Health==0)
        {
            Destroy(gameObject,0f);
            
            MainCharactor.KillCount += 1;
            Health = -1;
        }
        
        if(Health == 3)
        {
            SpriteRenderer.sprite = Blood3;
        }else if(Health == 2)
        {
            SpriteRenderer.sprite = Blood2;
        }else if(Health == 1)
        {
            SpriteRenderer.sprite = Blood1;
        }
        
        
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

    void OnTriggerStay(Collider Player)
    {
        Debug.Log("Detect Player");

        if(FireCoolDown == false && Player.CompareTag("Player") && Input.GetButton("Fire1"))
        {
            Debug.Log("dasdasdasdas");
            Health = Health - 1;
            FireCoolDown = true;
            color.color = Color.red;
            Invoke("colorwhite",0.3f);
        }
        if(!Input.GetButton("Fire1") && FireCoolDown == true)
        {
            FireCoolDown = false;
        }
    }
    public void colorwhite()
    {
        color.color = Color.white;
    }
}
