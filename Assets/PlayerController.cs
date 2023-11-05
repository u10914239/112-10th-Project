using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float horizontalValue;
    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;

    public bool facingRight;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

    }

    void Update()
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

        horizontalValue = x*speed*Time.deltaTime;


        Flip();

    }

    void Flip()
    {
        if ((horizontalValue < 0 && facingRight) || (horizontalValue > 0 && !facingRight))
        {

            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));

        }

    }

}

   
