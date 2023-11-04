using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public Rigidbody rb;
    public float runSpeed;
    float stopSpeed = 0f;
    Vector3 movement;
    bool facingRight;
    public Animator animator;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {

        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal") * runSpeed) + Mathf.Abs(Input.GetAxisRaw("Vertical") * runSpeed);
        animator.SetFloat("Speed", Mathf.Abs(stopSpeed));

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        if (movement.x > 0 && facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && !facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);


    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;

    }
}
