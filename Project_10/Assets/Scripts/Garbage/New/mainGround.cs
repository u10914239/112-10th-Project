using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainGround : MonoBehaviour
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
            mainCharactor.isGrounded = true;
        }
    }
}