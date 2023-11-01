using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    
        void OnTriggerStay(Collider Hit)
    {
        if(Hit.gameObject.tag == "Ground")
        {
            secondCharactor.isGrounded = true;
        }
        
    }
    void OnTriggerExit(Collider Hit)
    {

       if (Hit.gameObject.tag == "Ground")
       {
           secondCharactor.isGrounded = false;
       }
    }
}
