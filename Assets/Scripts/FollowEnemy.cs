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

    public float lookRadius;

    bool isStop;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lookRadius = 10;
        
        
        
        

    }
    
    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position,transform.position);
        if (distance < lookRadius)
        {
            enemy.speed = 2;
            enemy.SetDestination(player.transform.position);
            //slime.SetFloat("Speed", 1);
            if (player.transform.position.x - enemy.transform.position.x < 0)
            {
                Slime.flipX = false;
            }
            else
            {
                Slime.flipX = true;
            }
        }
        slime.SetFloat("Speed", enemy.velocity.magnitude);


       
    }

    
}
