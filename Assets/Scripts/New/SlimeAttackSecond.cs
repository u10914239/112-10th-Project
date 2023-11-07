using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackSecond : MonoBehaviour
{
    public bool Target;
    public float TimeInRange; //! COLD DOWN
    void Start()
    {
        
    }
    void Update()
    {
        if(Target && TimeInRange <= 0.3)
        {
            secondCharactor.GetAttacked = true;
        }else if(TimeInRange >=2)
        {
            TimeInRange = 0;
            secondCharactor.unHurt = false;
        }
        
        if(Target)
        {
            TimeInRange += Time.deltaTime;
        }else if(!Target)
        {
            TimeInRange = 0;
        }
    }

    public void OnTriggerStay(Collider Hit)
    {
        if(Hit.gameObject.tag == "Second")
        {
            Target = true;
        }
    }
    public void OnTriggerExit(Collider Hit)
    {
        Target = false;
    }
}