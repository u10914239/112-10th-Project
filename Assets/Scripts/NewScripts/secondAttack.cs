using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public bool dealtDamage;
    public bool inRange2;

    public Animator anim;

    void Start()
    {
        dealtDamage = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Attack();
            if (inRange2 && dealtDamage)
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
            inRange2 = true;
        }
    }
    void OnTriggerExit(Collider Hit)
    {
        if(Hit.gameObject.tag == ("Enemy"))
        {
            inRange2 = false;
        }
    }


}
