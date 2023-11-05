using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallWave : MonoBehaviour
{
    public static bool activate;
    public float knockBackForce = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider Hit)
    {
        if(activate)
        {
            if(Hit.gameObject.tag == "Enemy")
            {
                Rigidbody rb = Hit.GetComponent<Rigidbody>();
                if(rb!=null)
                {
                    Vector3 knockBack = (Hit.transform.position - transform.position).normalized;
                    //print(knockBack);
                    rb.AddForce(knockBack * knockBackForce, ForceMode.Impulse);
                }
                activate = false;
            }
        }

        
    }
}
