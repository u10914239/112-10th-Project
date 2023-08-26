using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyCharactor : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    float stopSpeed = 0f;
    public SpriteRenderer Knight;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 0.1f;

    }
    void Update()
    {
        PlayerMovement();
        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical") * speed);
        animator.SetFloat("Speed", Mathf.Abs(stopSpeed));
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
