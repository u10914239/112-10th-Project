using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public GameObject EnergyBar;
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
    public bool canMove , isMoving , isDodging;
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
    public GameObject MiniGame, Holder;
    public PlayerHealthBar Player2HealthBar;
    public PlayerController_Joystick playerController_Joystick;

    
   
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

        if (playerHealthBar.currentStamina>=30 && Input.GetKeyDown(KeyCode.Joystick2Button2) && !isDodging && isMoving && !isTransformed && PlayerHealthBar.Player1WaitForRescue == false)
        {

            RollForward();
            Swoosh3.Play();
            Invoke("RollCoolDownTimeEnd", 0.5f);
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.Joystick2Button0) && PlayerHealthBar.Player1WaitForRescue == false)
        {
            
            rb.velocity += new Vector3(0, jumpForce, 0);
        }

        if (canMove)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            if(Input.GetKey(KeyCode.A))
            {
                moveInput.x = -1;
            }
            if(Input.GetKey(KeyCode.D))
            {
                moveInput.x = 1;
            }
            if(Input.GetKey(KeyCode.W))
            {
                moveInput.y = -1;
            }
            if(Input.GetKey(KeyCode.S))
            {
                moveInput.y = 1;
            }

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


        //if(PlayerHealthBar.Player1WaitForRescue)
        //{
         //   canMove = false;
        //}
    }
    private void FixedUpdate()
    {
        if(canMove && PlayerHealthBar.Player1WaitForRescue == false)
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
        if (PlayerCombat.MagicAmount >= 50 &&pickUp.isHeld == false && isDodging == false && isTransformed == false && Input.GetKeyDown(KeyCode.E) && PlayerHealthBar.Player1WaitForRescue == false ||
            PlayerCombat.MagicAmount >= 50 && pickUp.isHeld == false && isDodging == false && isTransformed == false && Input.GetKeyDown(KeyCode.Joystick2Button1) && PlayerHealthBar.Player1WaitForRescue == false)
        {
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
            anim.SetBool("Transform", true);
            isTransformed = true;
            Explosion(transform.position, 0.5f);
            canMove = false;
            col.isTrigger = true;
            powerTime = 3;


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
            //Sync.SetActive(true);
            MiniGame.SetActive(true);
            Holder.SetActive(true);
            SyncManager.CallbyKnight = true;
            powerTime -= Time.deltaTime;
            EnergyBar.SetActive(true);
            
            if (powerTime <= 0 && Note.ConfirmDestroy)
            {
                rb.isKinematic = false;
                rb.interpolation = RigidbodyInterpolation.Interpolate;

                anim.SetBool("Transform", false);

                powerTime = 0;

                SyncManager.CallbyKnight = false;
                col.isTrigger = false;
                isTransformed = false;
                canMove = true;
                Sync.SetActive(false);
                MiniGame.SetActive(false);
                Holder.SetActive(false);
                EnergyBar.SetActive(false);
                
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

    public float Rescuing;
    public GameObject RescuingBar;
    public Slider Slider4Value;
    void OnTriggerStay(Collider other)
    {
        if(other.tag =="Player 2" && PlayerHealthBar.WaitForRescue)
        {
            if(Input.GetKey(KeyCode.Joystick2Button3) || Input.GetKey(KeyCode.Y))
            
            {
                //print("Player1"+Rescuing);
                Rescuing += Time.deltaTime;
                RescuingBar.SetActive(true);
                Slider4Value.value = Rescuing;
                if(Rescuing >= 3)
                {
                    
                   //playerHealthBar.Player2Rescued();
                    PlayerHealthBar.Player2WaitForRescue = false;
                    Rescuing = 0;
                    PlayerHealthBar.WaitForRescue = false;
                    Player2HealthBar.currentHealth = Player2HealthBar.maxHealth / 2;
                    RescuingBar.SetActive(false);
                }
            }else if(Input.GetKeyUp(KeyCode.Joystick2Button3) || Input.GetKeyUp(KeyCode.Y))
            {
                Rescuing = 0;
                RescuingBar.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player 2")
        {
            Rescuing = 0;
            RescuingBar.SetActive(false);
        }
    }

}

   
