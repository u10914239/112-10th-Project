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

    public NavMeshAgent agent;
    
    

    public Transform player;
   
    
    

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        Chasing();

    }

    
    
    private void Chasing()
    {

        agent.SetDestination(player.position);
        animator.SetFloat("Speed", 1);
       
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
