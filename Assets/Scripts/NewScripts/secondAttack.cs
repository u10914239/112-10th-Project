using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Attack2;
    public bool inRange2;
    void Start()
    {
        Attack2 = false;
    }
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Attack2 = true;
        }else
        {
            Attack2 = false;
        }
        if(inRange2 && Attack2)
        {
            FollowEnemy.isAttacked = true;
        }
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
