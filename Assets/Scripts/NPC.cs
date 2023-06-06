using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Rigidbody rb;
    public SpriteRenderer Knight;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.J))
        {
            rb.AddForce(-0.2f,0,0,ForceMode.Impulse);
            Knight.flipX = true;
        }
        if(Input.GetKey(KeyCode.K))
        {
            rb.AddForce(0,0,-0.2f,ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.L))
        {
            rb.AddForce(0.2f,0,0,ForceMode.Impulse);
            Knight.flipX = false;
        }
        if(Input.GetKey(KeyCode.I))
        {
            rb.AddForce(0,0,0.2f,ForceMode.Impulse);
        }
    }
}
