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

    public float jumpForce;

    public float groundDist;

    public LayerMask terrainLayer;
    
    public Animator anim;

    public static float powerTime;
    
    public bool isTransformed;
    

    public GameObject Sync;
    
    


    bool canMove , isMoving, isDodging;
    private Camera mainCamera;
    private float minWorldX, maxWorldX, minWorldY, maxWorldY;
    private float boundaryPadding = 1.0f;

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
        playerCombat = GetComponent<PlayerCombat>();
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

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
        
        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical") * speed);
        anim.SetFloat("Speed", Mathf.Abs(stopSpeed));


       
        
        if (canMove && !playerCombat.isAttacking)
        {
            Movement();
            
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

    private void LateUpdate()
    {
        // Call UpdateBoundaries in LateUpdate to ensure it runs after the camera has moved
        UpdateBoundaries();
    }

    
    private void Movement()
    {
        

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, 0, y);
        rb.velocity = moveDir * speed * Time.deltaTime;

        Vector3 newPosition = transform.position + rb.velocity;

        newPosition.x = Mathf.Clamp(newPosition.x, minWorldX, maxWorldX);
        newPosition.y = Mathf.Clamp(newPosition.y, minWorldY, maxWorldY);

        transform.position = newPosition;


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

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDodging && isMoving)
        {

            StartCoroutine(StartDodge());
        }

    }

    

    IEnumerator StartDodge()
    {
        
        
        isDodging = true;

        canMove = false;

        playerHealth.enabled = false;

        rb.AddForce(rb.velocity * dodgeForce, ForceMode.Impulse);

        anim.SetTrigger("isDodging");

        yield return new WaitForSeconds(dodgeDuration);

        

        isDodging = false;

        canMove = true;

        playerHealth.enabled = true;
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
        /*if (pickUp.isHeld == true)
        {
            col.enabled = false;

        }
        else
        {
            col.enabled = true;

        }*/

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
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0,jumpForce,0,ForceMode.Impulse);
            
        }
    }

    
}

   
