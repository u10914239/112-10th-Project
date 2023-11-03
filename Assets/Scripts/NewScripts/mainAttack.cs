using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public bool dealtDamage;
    public static bool inRange;

    public Animator anim;
    public AudioSource Punch;
    void Start()
    {
        dealtDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
            Punch.Play();
            if (inRange && dealtDamage)
            {
                FollowEnemy.isAttacked = true;
                mainCharactor.magicNumber +=1;
                dealtDamage = false;
            }
        }
        
    }
    void Attack()
    {
        dealtDamage = true;
        anim.SetTrigger("Attack");

       
    }

    void OnTriggerStay(Collider Hit)
    {
        if(Hit.gameObject.tag == ("Enemy"))
        {
            inRange = true;
        }
    }
    void OnTriggerExit(Collider Hit)
    {
        if(Hit.gameObject.tag == ("Enemy"))
        {
            inRange = false;
        }
    }
}
