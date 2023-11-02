using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public bool dealtDamage;
    public bool inRange;

    public Animator anim;

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
            if (inRange && dealtDamage)
            {
                FollowEnemy.isAttacked = true;
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
