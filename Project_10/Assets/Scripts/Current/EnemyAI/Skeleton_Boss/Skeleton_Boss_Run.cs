using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton_Boss_Run : StateMachineBehaviour
{
    
    Rigidbody rb;
    NavMeshAgent agent;
    Skeleton_Boss boss;
    Skeleton_Boss_Attack bossAttack;
    EnemyHealth enemyHealth; 

    public float chaseCooldown = 1.0f;
    public float attackRange = 3f;
    //private float lastPlayerMovementTime = Mathf.NegativeInfinity;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        
        rb = animator.GetComponent<Rigidbody>();
        agent = animator.GetComponent<NavMeshAgent>();
        boss = animator.GetComponent<Skeleton_Boss>();
        bossAttack = animator.GetComponent<Skeleton_Boss_Attack>();
        enemyHealth = animator.GetComponent<EnemyHealth>();

        
        
           
        boss.FindNearestPlayer();
        boss.LookAtPlayer();


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceToTarget = Vector3.Distance(rb.position, boss.target.position);
        boss.LookAtPlayer();


        if (Time.time - boss.lastPlayerMovementTime > chaseCooldown)
        {
            
            boss.FindNearestPlayer();
            
            if(distanceToTarget < boss.attackRange) 
            {
                agent.SetDestination(boss.target.position);
            }

        }

        


        if (distanceToTarget < attackRange && bossAttack.attackCount <= 4)
        {
            animator.SetTrigger("Attack");

        }
        else if (bossAttack.attackCount > 4) 
        {
            animator.SetTrigger("Big Attack");
            //bossAttack.attackCount = 0;
        }
        //If health below half change to fast attack
        if (enemyHealth.currentHealth < 200) 
        {

            if (distanceToTarget < attackRange && bossAttack.attackCount <= 4)
            {
                animator.SetTrigger("Fast Attack");

            }
            else if (bossAttack.attackCount > 2) 
            {
                animator.SetTrigger("Big Attack");
                //bossAttack.attackCount = 0;
            }
            
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }


}
