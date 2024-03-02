using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

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
    public static float powerTime;
    public bool facingRight;
    public bool isTransformed;
    public bool isHoldingGod;

    public Animator anim;
    public GameObject Sync;
    public GameObject WeaponHolder;

    private NavMeshAgent enemyAgent;
    private float originalSpeed;
    private Rigidbody enemyRb;

    public Sprite GodWeaponSprite;
    public SpriteRenderer NormalWeaponSprite;
    private Sprite originalSprite;

    public LayerMask terrainLayer;
    public Quaternion initialGlobalRotation;



    private bool isGrounded;
    bool canMove , isMoving , isDodging;
    private Vector2 moveInput;

    PickUp_Joystick pickUp;
   
    PlayerCombat playerCombat;
    Collider col;
    Rigidbody rb;

    [SerializeField] private SimpleFlash flashEffect;
    public AudioSource Swoosh3;

    public bool RollCoolDown;
    public PlayerHealthBar playerHealthBar;

    private Coroutine recharge;
   
    private void Awake()
    {
        pickUp = GameObject.Find("Player 2").GetComponent<PickUp_Joystick>();
        
        playerCombat = GetComponentInChildren<PlayerCombat>();
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        
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

        if (playerHealthBar.currentStamina>=30 && Input.GetKeyDown(KeyCode.Joystick2Button2) && !isDodging && isMoving && !isTransformed)
        {

            RollForward();
            Swoosh3.Play();
            Invoke("RollCoolDownTimeEnd", 0.5f);
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            
            rb.velocity += new Vector3(0, jumpForce, 0);
        }

        if (canMove)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
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



        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal") * speed) + Mathf.Abs(Input.GetAxisRaw("Vertical") * speed);
        anim.SetFloat("Speed", Mathf.Abs(stopSpeed));



    }
    
    private void FixedUpdate()
    {
        if(canMove)
        {
            rb.velocity = new Vector3(moveInput.x * speed, rb.velocity.y, -moveInput.y * speed);
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
        RollCoolDown = true;
        speed = 8f;
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
        speed = 2.5f;
        RollCoolDown = false;
        
    }


    void TurnIntoWeapon()
    {
        if (PlayerCombat.MagicAmount >= 50 &&pickUp.isHeld == false && isDodging == false && isTransformed == false && Input.GetKeyDown(KeyCode.E) ||
            PlayerCombat.MagicAmount >= 50 && pickUp.isHeld == false && isDodging == false && isTransformed == false && Input.GetKeyDown(KeyCode.Joystick2Button1))
        {
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
            anim.SetBool("Transform", true);
            isTransformed = true;
            Explosion(transform.position, 0.5f);
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
    void Explosion(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearyby in hitColliders)
        {
            if (nearyby.gameObject.tag == "Enemy")
            {
                enemyRb = nearyby.GetComponent<Rigidbody>();
                enemyAgent = nearyby.GetComponent<NavMeshAgent>();

                if (enemyRb != null && enemyAgent != null)
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

        Vector3 knockbackDirection = enemyAgent.transform.position - transform.position;
        knockbackDirection.Normalize();
        enemyRb.AddForce(knockbackDirection * 10f, ForceMode.Impulse);

        yield return new WaitForSeconds(1f);

        enemyAgent.speed = originalSpeed;

        enemyRb.velocity = Vector3.zero;

        enemyAgent.isStopped = false;



    }

}

   