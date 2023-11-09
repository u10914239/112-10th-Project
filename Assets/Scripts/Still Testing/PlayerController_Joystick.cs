using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Joystick : MonoBehaviour
{
    public float speed;
    float stopSpeed = 0f;

    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public Animator anim;

    public bool facingRight;

    public float powerTime;
    public bool isTransformed;
    bool canMove;

    //PickUp_Joystick pickUp;
    PickUp pickUp;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        pickUp = GameObject.Find("Player 1").GetComponent<PickUp>();
        isTransformed = false;
        facingRight = true;
        canMove = true;
    }

    void Update()
    {
        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal2") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical2") * speed);
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

        float x = Input.GetAxisRaw("Horizontal2");
        float y = Input.GetAxisRaw("Vertical2");
       
        
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

       
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
    }



    void TurnIntoWeapon()
    {
        if (pickUp.isHeld == false && isTransformed == false && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
            anim.SetBool("Transform", true);
            isTransformed = true;
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
