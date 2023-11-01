using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Attack;
    public bool inRange;
    void Start()
    {
        Attack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Attack = true;
        }else
        {
            Attack = false;
        }
        if(inRange && Attack)
        {
            FollowEnemy.isAttacked = true;
        }
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
