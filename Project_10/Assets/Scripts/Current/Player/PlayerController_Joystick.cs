using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController_Joystick : MonoBehaviour
{
    public float speed;
    float stopSpeed = 0f;

    public float dodgeForce = 10f;
    public float dodgeDuration = 0.5f;
    public float rollSpeed = 10f;
    public float jumpForce;
    public float groundDist;
    public float raycastDistance = 0.5f;
    public GameObject Sync;
    public LayerMask terrainLayer;
    public Animator anim;
    public static float powerTime;
    public bool isTransformed;
    public bool facingRight;
    public bool isHoldingGod;


    public GameObject WeaponHolder;
    public Sprite GodWeaponSprite;
    public SpriteRenderer NormalWeaponSprite;

    
    private NavMeshAgent enemyAgent;
    private float originalSpeed;
    private Rigidbody enemyRb;
    

    private Sprite originalSprite;

    public Quaternion initialGlobalRotation;

    private Vector2 moveInput;
    PickUp pickUp1;
    PlayerCombat_Joystick_Wizard playerCombat;
    Collider col;
    Rigidbody rb;


    private bool isGrounded;
    bool canMove, isMoving;
    bool currentFacing;
    public AudioSource Swoosh3;

    public bool RollCoolDown;
    public PlayerHealthBar playerHealthBar;
    public Coroutine recharge;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        pickUp1 = GameObject.Find("Player 1").GetComponent<PickUp>();
       


        playerCombat = GetComponent<PlayerCombat_Joystick_Wizard>();
        col = GetComponent<Collider>();

    }

    void Start()
    {
        

        isTransformed = false;
        
        canMove = true;
        originalSprite = NormalWeaponSprite.sprite;

        
        

    }

    void Update()
    {
        
        TurnIntoWeapon();
        isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastDistance, terrainLayer);
        //if (Input.GetKeyDown(KeyCode.Joystick1Button0) && isMoving)
        //{
//
        //    RollForward();
        //    Swoosh3.Play();
        //}

        if (playerHealthBar.currentStamina>=30 && Input.GetKeyDown(KeyCode.Joystick1Button2) && isMoving && !isTransformed)
        {

            RollForward();
            Swoosh3.Play();
            Invoke("RollCoolDownTimeEnd", 1f);
        }

        if (canMove)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal2");
            moveInput.y = Input.GetAxisRaw("Vertical2");

            moveInput.Normalize();
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {

            rb.velocity += new Vector3(0, jumpForce, 0);
        }

        //�ˬd��W���S������
        if (WeaponHolder != null)
        {
            bool hasChildren = WeaponHolder.transform.childCount > 0;

            if (hasChildren && NormalWeaponSprite != null && GodWeaponSprite != null)
            {
                NormalWeaponSprite.sprite = GodWeaponSprite;
                isHoldingGod = true;
            }
            else
            {
                NormalWeaponSprite.sprite = originalSprite;
                isHoldingGod = false;
            }
        }

        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal2") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical2") * speed);
        anim.SetFloat("Speed", Mathf.Abs(stopSpeed));


    }
    void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = new Vector3(moveInput.x * speed  , rb.velocity.y, moveInput.y * speed );

        }
        properFlip();


        if (stopSpeed > 0.1f)
        {
            isMoving = true;

        }
        else if (stopSpeed < 0.1f)
        {

            isMoving = false;
        }


       
    }



    void properFlip()
    {
        if ((moveInput.x < 0 && facingRight) || (moveInput.x > 0 && !facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
            initialGlobalRotation = transform.rotation;
        }
    }





    void RollForward()
    {
        anim.SetTrigger("isDodging");
        Vector3 rollDirection = transform.forward * rollSpeed;
        rb.velocity = rollDirection;
        speed = 6f;
        anim.SetTrigger("Roll");
        playerHealthBar.currentStamina -=30;
        playerHealthBar.StartRecover = false;
        if(recharge != null) StopAllCoroutines();
        recharge = StartCoroutine(Recover());

        //StopCoroutine(Recover());
        //StartCoroutine(Recover());
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(1.5f);
        playerHealthBar.StaminaRecover();
        //if(playerHealthBar.currentStamina >= playerHealthBar.MaxStamina)
        //yield return new WaitForSeconds(1);
    }

    void RollCoolDownTimeEnd()
    {
        RollCoolDown = false;
        speed=2.5f;
    }




    void TurnIntoWeapon()
    {
        if (PlayerCombat_Joystick_Wizard.MagicAmount >= 50 && pickUp1.isHeld == false  && isTransformed == false && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
            anim.SetBool("Transform", true);
            isTransformed = true;
            Explosion(transform.position, 2f);
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

            if (powerTime >= 3)
            {
                anim.SetBool("Transform", false);
                rb.isKinematic = false;
                rb.interpolation = RigidbodyInterpolation.Interpolate;
                



                powerTime = 0;
                col.isTrigger = false;
                isTransformed = false;
                canMove = true;
                Sync.SetActive(false);

            }

        }

    }
    void Explosion(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearyby in hitColliders )
        {
            if(nearyby.gameObject.tag == "Enemy")
            {
                enemyRb = nearyby.GetComponent<Rigidbody>();
                enemyAgent = nearyby.GetComponent<NavMeshAgent>();

                if (enemyRb != null && enemyAgent !=null)
                {
                    StartCoroutine(TemporarilyStopAgentAfterExplosion());
                   



                }
            }
            
        }
    }

    IEnumerator TemporarilyStopAgentAfterExplosion()
    {
        originalSpeed = enemyAgent.speed;

        enemyAgent.speed = 0f;

        enemyAgent.velocity = Vector3.zero;
               
        enemyAgent.isStopped = true;

        Vector3 knockbackDirection = enemyAgent.transform.position -transform.position;
        knockbackDirection.Normalize();
        enemyRb.AddForce(knockbackDirection * 10f, ForceMode.Impulse);

        yield return new WaitForSeconds(1f);

        enemyAgent.speed = originalSpeed;

        enemyRb.velocity = Vector3.zero;

        enemyAgent.isStopped= false;
      
        

    }

}
