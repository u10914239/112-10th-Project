using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float stopSpeed = 0f;

    public float dodgeForce = 10f;
    public float dodgeDuration = 0.5f;
    private bool isDodging = false;

    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public Animator anim;

        
    public static float powerTime;
    public bool facingRight;
    public bool isTransformed;
    bool canMove;

    
    PickUp_Joystick pickUp;

    public GameObject Sync;

    private Camera mainCamera;
    private float minWorldX, maxWorldX, minWorldY, maxWorldY;
    private float boundaryPadding = 1.0f;


    public static int playerHealth;
    public int playerHealthSide;
    public static bool GetAttacked;
    public static bool unHurt;
    public GameObject HealthBar1,HealthBar2,HealthBar3,HealthBar4,HealthBar5;
    public SpriteRenderer Knight;

    private void Awake()
    {
        



    }
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        pickUp = GameObject.Find("Player 2").GetComponent<PickUp_Joystick>();

        isTransformed = false;
        facingRight = true;
        canMove = true;

        mainCamera = Camera.main;
        UpdateBoundaries();
        playerHealth = 5;
    }

    void Update()
    {
        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical") * speed);
        anim.SetFloat("Speed", Mathf.Abs(stopSpeed));

        TurnIntoWeapon();

        Health();
        


        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDodging)
        {
            StartCoroutine(StartDodge());
        }


    }
    void FixedUpdate()
    {
        

        if (canMove)
        {
           Movement();

           
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
        rb.velocity = moveDir * speed*Time.deltaTime;

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


    }

    IEnumerator StartDodge()
    {
        isDodging = true;

        canMove = false;

        rb.AddForce(rb.velocity * dodgeForce, ForceMode.Impulse);

        anim.SetTrigger("isDodging");

        yield return new WaitForSeconds(dodgeDuration);

        canMove = true;

        isDodging = false;
    }






    void TurnIntoWeapon()
    {
        if (pickUp.isHeld == false && isDodging == false && isTransformed == false && Input.GetKeyDown(KeyCode.E))
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
            Sync.SetActive(true);
            powerTime += Time.deltaTime;
            if (powerTime >= 10)
            {
                rb.isKinematic = false;
                rb.interpolation = RigidbodyInterpolation.Interpolate;

                anim.SetBool("Transform", false);

                

                isTransformed = false;
                powerTime = 0;
                canMove = true;
                Sync.SetActive(false);
               
            }

        }

    }

    void Health()
    {
        if(playerHealth >= 5)
        {
            HealthBar1.SetActive(true);
            HealthBar2.SetActive(true);
            HealthBar3.SetActive(true);
            HealthBar4.SetActive(true);
            HealthBar5.SetActive(true);
        }else if(playerHealth == 4)
        {
            HealthBar1.SetActive(true);
            HealthBar2.SetActive(true);
            HealthBar3.SetActive(true);
            HealthBar4.SetActive(true);
            HealthBar5.SetActive(false);
        }else if(playerHealth == 3)
        {
            HealthBar1.SetActive(true);
            HealthBar2.SetActive(true);
            HealthBar3.SetActive(true);
            HealthBar4.SetActive(false);
            HealthBar5.SetActive(false);
        }else if(playerHealth == 2)
        {
            HealthBar1.SetActive(true);
            HealthBar2.SetActive(true);
            HealthBar3.SetActive(false);
            HealthBar4.SetActive(false);
            HealthBar5.SetActive(false);
        }else if(playerHealth == 1)
        {
            HealthBar1.SetActive(true);
            HealthBar2.SetActive(false);
            HealthBar3.SetActive(false);
            HealthBar4.SetActive(false);
            HealthBar5.SetActive(false);
        }
        else if(playerHealth == 0)
        {
            HealthBar1.SetActive(false);
            HealthBar2.SetActive(false);
            HealthBar3.SetActive(false);
            HealthBar4.SetActive(false);
            HealthBar5.SetActive(false);
        }

        if(GetAttacked && !unHurt)
        {
            unHurt = true;
            playerHealth -=1;
            Knight.color = Color.red;
            Invoke("colorwhite",0.3f);
        }
    }

    void colorwhite()
    {
        Knight.color = Color.white;
    }


}

   
