using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float stopSpeed = 0f;
    public float horizontalValue;
    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public Animator anim;

    public bool facingRight;

    

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
        facingRight = true;
    }

    void Update()
    {
        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical") * speed);
        anim.SetFloat("Speed", Mathf.Abs(stopSpeed));


    }
    void FixedUpdate()
    {

        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;

        

        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;

            }
            
        }
        float x = Input.GetAxis("Horizontal") ;
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        rb.velocity = moveDir * speed;

        if (x > 0 && !facingRight)
        {
            Flip();
            
        }
        else if (x < 0 && facingRight)
        {
            Flip();
            
        }


       

    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

}

   
