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
    bool canMove;

    //PickUp pickUp;
    PickUp_Joystick pickUp;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        pickUp = GameObject.Find("Player 2").GetComponent<PickUp_Joystick>();
        isTransformed = false;
        facingRight = true;
        canMove = true;
    }

    void Update()
    {
        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical") * speed);
        anim.SetFloat("Speed", Mathf.Abs(stopSpeed));
        TurnIntoWeapon();


    }
    void FixedUpdate()
    {
        

        if (canMove)
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
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        
        
        Vector3 moveDir = new Vector3(x, 0, y);
        rb.velocity = moveDir * speed;


        Vector3 charactorScale = transform.localScale;
        if (x > 0)
        {
            charactorScale.x = 1;


        }
        if (x < 0)
        {
            charactorScale.x = -1;
        }
        transform.localScale = charactorScale;


    }

    

   



    void TurnIntoWeapon()
    {
        if (pickUp.isHeld == false && isTransformed == false && Input.GetKeyDown(KeyCode.E))
        {
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
            anim.SetBool("Transform", true);
            isTransformed = true;
            facingRight = true;
            canMove = false;
            
        }
        if (isTransformed)
        {
            powerTime += Time.deltaTime;
            if (powerTime >= 10)
            {
                rb.isKinematic = false;
                rb.interpolation = RigidbodyInterpolation.Interpolate;

                anim.SetBool("Transform", false);

                

                isTransformed = false;
                powerTime = 0;
                canMove = true;

               
            }

        }

    }


}

   
