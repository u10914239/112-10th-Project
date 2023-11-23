using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class FollowEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range;
    public Transform centrePoint;

    public Animator animator;

    public Transform player, player1, player2;

    public LayerMask whatIsPlayer;
    public float lookRadius, attackRadius;
    public bool playerInSightRange, playerInAttackRange;
    public float timeBetweenAttack;

    public int maxHealth= 3;
    int currentHealth;
    public GameObject spawner;

    public bool FireCoolDown = false;
    public SpriteRenderer color;

    public SpriteRenderer SpriteRenderer;
    public Sprite Blood1,Blood2,Blood3;
    public Rigidbody rb;
    public float Knockback;
    public float Dis1,Dis2;
    Vector3 D ;

    Vector3 scale;

    public bool isHit;
    //public static bool isBigAttacked;
    bool isAttacking;
    

    private PhotonView _pv;

    public static bool smallWaveActivate;
    public float knockBackForce;
    public bool KnockbackInRange;
    public GameObject HitTarget;

    void Start()
    {
        DetectTargetPlayer();
        //player = GameObject.FindWithTag("Player").transform;
        color = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        spawner = GameObject.Find("Spawner");

        currentHealth = maxHealth;
        Knockback = 10f;
        knockBackForce = 5f;
        _pv = this.gameObject.GetComponent<PhotonView>();

    }

    private void Update()
    {
        
        Knockbacks();
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

        D = transform.position - player.transform.position;
        
        if(currentHealth == 3)
        {
            SpriteRenderer.sprite = Blood3;
        }else if(currentHealth == 2)
        {
            SpriteRenderer.sprite = Blood2;
        }else if(currentHealth == 1)
        {
            SpriteRenderer.sprite = Blood1;
        }
        
        
        
        
    }
    void DetectTargetPlayer()
    {
        Dis1 = Vector3.Distance(this.transform.position,player1.transform.position);
        Dis2 = Vector3.Distance(this.transform.position,player2.transform.position);
        print(Dis1.ToString() + "+" + Dis2.ToString());
        if(Dis1<Dis2)
        {
            player = player1;
        }else if(Dis2<Dis1)
        {
            player = player2;
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
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerHitBox" && isHit == false)
        {
            TakeDamage(1);
            isHit = true;
        }
        else
        {
            isHit = false;
        }


    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        rb.AddForce(D*Knockback,ForceMode.Impulse);
        color.color = Color.red;
        Invoke("colorwhite",0.3f);

        if (currentHealth <= 0)
        {
            Die();
            Destroy(this.gameObject);
            //spawner.GetComponent<GenerateEnemies>().enemyCount--;
        }
    }

    void Die()
    {
        
        
        Debug.Log("I died");
        
        Destroy(this);
        
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

    
    public void colorwhite()
    {
        color.color = Color.white;
    }

    public void Knockbacks()
    {

        if(smallWaveActivate && KnockbackInRange)
        {
            Vector3 knockBack = (this.transform.position - HitTarget.transform.position).normalized;
            rb.AddForce(knockBack * knockBackForce, ForceMode.Impulse);
        }
    }

    void OnTriggerStay(Collider Hit)
    {
        if(Hit.gameObject.tag == "smallWave")
        {
             KnockbackInRange = true;
             HitTarget = Hit.gameObject;
        }
    }
    void OnTriggerExit(Collider Hit)
    {
        if(Hit.gameObject.tag == "smallWave")
        {
             KnockbackInRange = false;
        }
    }
    
}