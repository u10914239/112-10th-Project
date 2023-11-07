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
    
    float powerTime;
    public bool isTransformed;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        isTransformed = false;
        facingRight = true;
        
    }

    void Update()
    {
        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical") * speed);
        anim.SetFloat("Speed", Mathf.Abs(stopSpeed));

        TurnIntoWeapon();


    }
    void FixedUpdate()
    {
        


        if (isTransformed==false)
        {
            Movement();

        }
        
    }

    private void Movement()
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
        float x = Input.GetAxis("Horizontal");
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

    

    void TurnIntoWeapon()
    {
        if (isTransformed == false && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("Transform", true);
            isTransformed = true;
            

        }
        if (isTransformed)
        {
            powerTime += Time.deltaTime;
            if (powerTime >= 10)
            {
                anim.SetBool("Transform", false);
                isTransformed = false;
                powerTime = 0;
            }

        }

    }


}

   
