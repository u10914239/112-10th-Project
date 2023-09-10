using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowEnemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public GameObject player;
    public SpriteRenderer Slime;

    public Animator slime;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void Update()
    {
        enemy.SetDestination(player.transform.position);
        slime.SetFloat("Speed", 1);
        if (player.transform.position.x - enemy.transform.position.x < 0)
        {
            Slime.flipX = false;
        }
        else
        {
           Slime.flipX = true;
        }
    }
    
}
