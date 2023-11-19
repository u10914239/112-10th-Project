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
    private Transform targetPlayer;




    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        FindNearestPlayer();
        if (targetPlayer != null)
        {
            Chasing();
        }

    }

    void FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                targetPlayer = player.transform;
            }
        }
    }

    private void Chasing()
    {

        agent.SetDestination(targetPlayer.position);
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
