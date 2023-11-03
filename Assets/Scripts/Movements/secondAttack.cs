using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public bool dealtDamage;
    public static bool inRange2;

    public Animator anim;
    public AudioSource Punch;
    void Start()
    {
        dealtDamage = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Attack();
            Punch.Play();
            if (inRange2 && dealtDamage)
            {
                FollowEnemy.isAttacked = true;
                secondCharactor.magicNumber +=1;
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
