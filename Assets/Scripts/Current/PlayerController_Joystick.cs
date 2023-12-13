using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Joystick : MonoBehaviour
{
    public float speed;
    float stopSpeed = 0f;
    public float dodgeForce = 10f;
    public float dodgeDuration = 0.5f;
    public float rollSpeed = 10f;
    public float jumpForce;
    public float groundDist;
    public LayerMask terrainLayer;
    public Rigidbody rb;
    public Animator anim;
    public static float powerTime;
    public bool isTransformed;
    public SpriteRenderer Knight;


    private Vector2 moveInput;
    PickUp pickUp;
    PlayerHealth playerHealth;
    PlayerCombat_Joystick_Wizard playerCombat;
    Collider col;

    public GameObject Sync;


    bool canMove, isMoving, isDodging;
   
    

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        pickUp = GameObject.Find("Player 1").GetComponent<PickUp>();
        playerHealth = GetComponent<PlayerHealth>();
        playerCombat = GetComponent<PlayerCombat_Joystick_Wizard>();
        col = GetComponent<Collider>();

    }

    void Start()
    {
        

        isTransformed = false;
        
        canMove = true;
        

    }

    void Update()
    {
        
        TurnIntoWeapon();

        if (Input.GetKeyDown(KeyCode.Joystick1Button0) && !isDodging && isMoving)
        {

            RollForward();
        }

        if (canMove)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal2");
            moveInput.y = Input.GetAxisRaw("Vertical2");
            moveInput.Normalize();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {

            rb.velocity += new Vector3(0, jumpForce, 0);
        }

        Vector3 charactorScale = transform.localScale;

        if (moveInput.x > 0)
        {
            charactorScale.x = 1;


        }
        if (moveInput.x < 0)
        {
            charactorScale.x = -1;
        }

        transform.localScale = charactorScale;



        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal2") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical2") * speed);
        anim.SetFloat("Speed", Mathf.Abs(stopSpeed));


    }
    void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = new Vector3(moveInput.x * speed, rb.velocity.y, moveInput.y * speed);
        }



        if (stopSpeed > 0.1f)
        {
            isMoving = true;

        }
        else if (stopSpeed < 0.1f)
        {

            isMoving = false;
        }


       
    }

   

    private void LateUpdate()
    {
        // Call UpdateBoundaries in LateUpdate to ensure it runs after the camera has moved
        
    }

    
    
   

    

    void RollForward()
    {
        anim.SetTrigger("isDodging");
        Vector3 rollDirection = transform.forward * rollSpeed;
        rb.velocity = rollDirection;
    }


    void TurnIntoWeapon()
    {
        if (pickUp.isHeld == false && isDodging == false && isTransformed == false && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
            anim.SetBool("Transform", true);
            isTransformed = true;
            
            canMove = false;
            col.isTrigger = true;


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


        }
       
        if (isTransformed)
        {
            Sync.SetActive(true);
            powerTime += Time.deltaTime;

            if (powerTime >= 10)
            {
                rb.isKinematic = false;
                rb.interpolation = RigidbodyInterpolation.Interpolate;

                anim.SetBool("Transform", false);

                powerTime = 0;


                col.isTrigger = false;
                isTransformed = false;
                canMove = true;
                Sync.SetActive(false);

            }

        }

    }

}
