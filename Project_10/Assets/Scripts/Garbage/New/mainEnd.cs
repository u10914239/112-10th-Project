using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainEnd : MonoBehaviour
{
    public static bool ReachEnd;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider Hit)
    {
        if(Hit.gameObject.tag == "End")
        {
            ReachEnd = true;
        }
    }
    void OnTriggerExit(Collider Hit)
    {
        if(Hit.gameObject.tag == "End")
        {
            ReachEnd = false;
        }
    }
}
