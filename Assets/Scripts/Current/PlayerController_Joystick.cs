using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Joystick : MonoBehaviour
{
    public float speed;
    float stopSpeed = 0f;

    public float dodgeForce = 10f;
    public float dodgeDuration = 0.5f;

    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public Animator anim;

    

    public static float powerTime;
    public bool isTransformed;
    public bool facingRight;


    public SpriteRenderer Knight;
    
    PickUp pickUp;
    PlayerHealth playerHealth;
    Collider col;

    public GameObject Sync;


    bool canMove, isMoving, isDodging;
    private Camera mainCamera;
    private float minWorldX, maxWorldX, minWorldY, maxWorldY;
    private float boundaryPadding = 1.0f;
    

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        pickUp = GameObject.Find("Player 1").GetComponent<PickUp>();
        playerHealth = GetComponent<PlayerHealth>();
        col = GetComponent<Collider>();

    }

    void Start()
    {
        

        isTransformed = false;
        facingRight = true;
        canMove = true;
        mainCamera = Camera.main;
        UpdateBoundaries();

    }

    void Update()
    {
        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal2") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical2") * speed);
        anim.SetFloat("Speed", Mathf.Abs(stopSpeed));
        TurnIntoWeapon();


        if (canMove)
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
    void FixedUpdate()
    {
        

        


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
        
        float x = Input.GetAxisRaw("Horizontal2");
        float y = Input.GetAxisRaw("Vertical2");

        Vector3 moveDir = new Vector3(x, 0, y);
        rb.velocity = moveDir * speed * Time.deltaTime;

        Vector3 newPosition = transform.position + rb.velocity;

        newPosition.x = Mathf.Clamp(newPosition.x, minWorldX, maxWorldX);
        newPosition.y = Mathf.Clamp(newPosition.y, minWorldY, maxWorldY);

        transform.position = newPosition;

       

        
        if (x > 0&&!facingRight)
        {
            
            Flip();

        }
        if (x < 0&&facingRight)
        {
           Flip();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0) && !isDodging && isMoving)
        {

            StartCoroutine(StartDodge());
        }


    }

    public void Flip()
    {
        Vector3 charactorScale = transform.localScale;
        charactorScale.x *= -1;
        gameObject.transform.localScale = charactorScale;

        facingRight=!facingRight;
    }

    IEnumerator StartDodge()
    {
        playerHealth.enabled = false;

        isDodging = true;

        canMove = false;

        rb.AddForce(rb.velocity * dodgeForce, ForceMode.Impulse);

        anim.SetTrigger("isDodging");

        yield return new WaitForSeconds(dodgeDuration);

        playerHealth.enabled = true;

        canMove = true;

        isDodging = false;
    }



    
    void TurnIntoWeapon()
    {
        if (pickUp.isHeld == false && isDodging == false && isTransformed == false && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
            anim.SetBool("Transform", true);
            isTransformed = true;
            facingRight = true;
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
        if (pickUp.isHeld == true)
        {
            col.enabled = false;

        }
        else
        {
            col.enabled = true;

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

                col.isTrigger = false;
                isTransformed = false;
                powerTime = 0;
                canMove = true;
                Sync.SetActive(false);
                

            }

        }

    }

}
