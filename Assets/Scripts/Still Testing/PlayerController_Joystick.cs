using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Joystick : MonoBehaviour
{
    public float speed;
    float stopSpeed = 0f;

    private bool isDodging = false;

    public float dodgeForce = 5f;
    public float dodgeDuration = 0.5f;

    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public Animator anim;

    public bool facingRight;

    float powerTime;
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

        if (Input.GetKey(KeyCode.Joystick1Button0) && moveDir != Vector3.zero)
        {

            StartCoroutine(PerformDodgeRoll());

        }

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

    IEnumerator PerformDodgeRoll()
    {
        isDodging = true;

        // Apply a force to the player in the desired direction
        rb.AddForce(rb.velocity * dodgeForce, ForceMode.Impulse);

        // Disable player control during the dodge roll
        canMove = false;
        // You can add animation or other effects here
        anim.SetTrigger("isDodging");
        // to make the dodge roll visually appealing
        yield return new WaitForSeconds(dodgeDuration);

        // Enable player control after the dodge roll
        isDodging = false;
        canMove = true;
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