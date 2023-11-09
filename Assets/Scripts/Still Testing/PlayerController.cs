using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float stopSpeed = 0f;
    
    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public Animator anim;

    public bool facingRight;
    
    float powerTime;
    public bool isTransformed;


    //PickUp pickUp;
    PickUp_Joystick pickUp;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        pickUp = GameObject.Find("Player 2").GetComponent<PickUp_Joystick>();
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

        transform.Rotate(new Vector3(0, 180, 0));
        facingRight = !facingRight;

        

        
    }

    

    void TurnIntoWeapon()
    {
        if (pickUp.isHeld == false && isTransformed == false && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("Transform", true);
            isTransformed = true;
            facingRight = true;

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

   
