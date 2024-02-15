using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainAttack : MonoBehaviour
{
    
    public Animator anim;
    public AudioSource Punch;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
            
            
        }
        
    }
    void Attack()
    {
       
        anim.SetTrigger("Attack");
        Punch.Play();

    }

    
}
