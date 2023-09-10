using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowEnemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public Animator slime;

    
    private void Update()
    {
        enemy.SetDestination(player.position);
        slime.SetFloat("Speed", 1);

    }
    
}
