using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyCharactor : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public SpriteRenderer Knight;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 0.15f;

    }
    void Update()
    {
        PlayerMovement();
    }
    void PlayerMovement()
    {
        if(Input.GetAxisRaw("Horizontal") > 0.1f)
        {
            

            rb.AddForce(speed,0,0,ForceMode.Impulse);
            Knight.flipX = false;
        }else if(Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            
            rb.AddForce(-speed,0,0,ForceMode.Impulse);
            Knight.flipX = true;
        }
        if(Input.GetAxisRaw("Vertical") > 0.1f)
        {
            rb.AddForce(0,0,speed,ForceMode.Impulse);
        }else if(Input.GetAxisRaw("Vertical") < -0.1f)
        {
            rb.AddForce(0,0,-speed,ForceMode.Impulse);
        }
    }
}
