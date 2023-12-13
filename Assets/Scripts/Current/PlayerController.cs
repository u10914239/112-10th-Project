using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float stopSpeed = 0f;
    public float dodgeForce = 10f;
    public float dodgeDuration = 0.5f;
    public float rollSpeed = 10f;
    public float jumpForce;
    public float groundDist;
    public float raycastDistance = 0.5f;
    public LayerMask terrainLayer;
    public Animator anim;
    public static float powerTime;
    public bool isTransformed;
    public GameObject Sync;

    bool canMove , isMoving, isDodging;
    private Camera mainCamera;
    
    private float minWorldX, maxWorldX, minWorldY, maxWorldY;
    public float boundaryPadding = 0.1f;
    private Vector2 moveInput;
    private CapsuleCollider playerCollider;

    PickUp_Joystick pickUp;
    PlayerHealth playerHealth;
    PlayerCombat playerCombat;
    Collider col;
    Rigidbody rb;

    [SerializeField] private SimpleFlash flashEffect;

   
    private void Awake()
    {
        pickUp = GameObject.Find("Player 2").GetComponent<PickUp_Joystick>();
        playerHealth = GetComponent<PlayerHealth>();
        playerCombat = GetComponentInChildren<PlayerCombat>();
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
    }
    void Start()
    {
        
        isTransformed = false;
        canMove = true;
        
        
        mainCamera = Camera.main;
        
        UpdateBoundaries();
    }

    void Update()
    {
        
        TurnIntoWeapon();
       

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDodging && isMoving && !isTransformed)
        {

            RollForward();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);*/
            rb.velocity += new Vector3(0, jumpForce, 0);
        }

        moveInput.x= Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        
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



        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical") * speed);
        anim.SetFloat("Speed", Mathf.Abs(stopSpeed));




    }
    
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveInput.x * speed, rb.velocity.y, moveInput.y * speed);
        PlayerBoundCamera();
        

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
         UpdateBoundaries();
    }

    private void UpdateBoundaries()
    {
        float distance = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

        Vector3 lowerLeft = mainCamera.ScreenToWorldPoint(new Vector3(0 + boundaryPadding, 0 + boundaryPadding, distance));
        Vector3 upperRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width - boundaryPadding, Screen.height - boundaryPadding, distance));

        minWorldX = lowerLeft.x;
        maxWorldX = upperRight.x;
        minWorldY = lowerLeft.y;
        maxWorldY = upperRight.y;
    }

    private void PlayerBoundCamera()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();

        Vector3 newPosition = transform.position + moveInput * speed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, minWorldX, maxWorldX);
        newPosition.y = Mathf.Clamp(newPosition.y, minWorldY, maxWorldY);

        // Check if the new position is within boundaries before updating the player's position
        if (IsWithinBoundaries(newPosition))
        {
            rb.MovePosition(newPosition);
        }
    }

    private bool IsWithinBoundaries(Vector3 position)
    {
        return position.x >= minWorldX && position.x <= maxWorldX
            && position.y >= minWorldY && position.y <= maxWorldY;
    }
    /*private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, 0, y);
        rb.velocity = moveDir * speed * Time.deltaTime;

        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical") * speed);
        anim.SetFloat("Speed", Mathf.Abs(stopSpeed));


        

       


    }*/

    /*private void UpdateBoundaries()
    {
        float distance = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

        Vector3 lowerLeft = mainCamera.ScreenToWorldPoint(new Vector3(0 + boundaryPadding, 0 + boundaryPadding, distance));
        Vector3 upperRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width - boundaryPadding, Screen.height - boundaryPadding, distance));

        minWorldX = lowerLeft.x;
        maxWorldX = upperRight.x;
        minWorldY = lowerLeft.y;
        maxWorldY = upperRight.y;
    }

    private void PlayerBoundCamera()
    {
        Vector3 newPosition = transform.position + rb.velocity;

        newPosition.x = Mathf.Clamp(newPosition.x, minWorldX, maxWorldX);
        newPosition.y = Mathf.Clamp(newPosition.y, minWorldY, maxWorldY);

        transform.position = newPosition;


    }*/


    void RollForward()
    {
        anim.SetTrigger("isDodging");
        Vector3 rollDirection = transform.forward * rollSpeed;
        rb.velocity = rollDirection;
    }



    void TurnIntoWeapon()
    {
        if (pickUp.isHeld == false && isDodging == false && isTransformed == false && Input.GetKeyDown(KeyCode.E))
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

   
