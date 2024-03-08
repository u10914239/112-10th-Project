using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public GameObject EnergyBar;
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
    public bool canMove, isMoving;
    bool currentFacing;
    public AudioSource Swoosh3;

    public bool RollCoolDown;
    public PlayerHealthBar playerHealthBar;
    public Coroutine recharge;
    public GameObject MiniGame, Holder;
    public PlayerHealthBar Player1HealthBar;
    public PlayerHealthBar Player2HealthBar;
    public PlayerController playerController;
    public Transform DeadZone1Positon = null,DeadZone2Positon = null,DeadZone3Positon = null,DeadZone4Positon = null,DeadZone5Positon = null, DeadZone3_2Positon = null;

    public GameObject FindPlayerDectector;
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
        WhenDead();
        isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastDistance, terrainLayer);
        //if (Input.GetKeyDown(KeyCode.Joystick1Button0) && isMoving)
        //{
//
        //    RollForward();
        //    Swoosh3.Play();
        //}

        if (playerHealthBar.currentStamina>=30 && Input.GetKeyDown(KeyCode.Joystick1Button2) && isMoving && !isTransformed && PlayerHealthBar.Player2WaitForRescue == false)
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

        if (isGrounded && Input.GetKeyDown(KeyCode.Joystick1Button0) && PlayerHealthBar.Player2WaitForRescue == false)
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

        
        //if(PlayerHealthBar.Player2WaitForRescue)
        //{
         //   canMove = false;
        //}
    }
    void FixedUpdate()
    {
        if (canMove && PlayerHealthBar.Player2WaitForRescue == false)
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
        Invoke("RollCoolDownTimeEnd", 1f);
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
        if (PlayerCombat_Joystick_Wizard.MagicAmount >= 70 && pickUp1.isHeld == false  && isTransformed == false && Input.GetKeyDown(KeyCode.Joystick1Button1) && PlayerHealthBar.Player2WaitForRescue == false)
        {
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
            anim.SetBool("Transform", true);
            isTransformed = true;
            Explosion(transform.position, 2f);
            canMove = false;
            col.isTrigger = true;
            powerTime = 3;
            PlayerCombat_Joystick_Wizard.MagicAmount = PlayerCombat_Joystick_Wizard.MagicAmount - 70;


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
            SyncManager.CallbyMagic = true;
            EnergyBar.SetActive(true);
            powerTime -= Time.deltaTime;
            //print(powerTime);

            if (powerTime <= 0 && Note.ConfirmDestroy)
            {
                anim.SetBool("Transform", false);
                rb.isKinematic = false;
                rb.interpolation = RigidbodyInterpolation.Interpolate;
                


                SyncManager.CallbyMagic = false;
                powerTime = 0;
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

    void WhenDead()
    {
        if(Player2HealthBar.currentHealth <= 0)
        {
            FindPlayerDectector.GetComponent<CapsuleCollider>().enabled = false;
        }else
        {
            FindPlayerDectector.GetComponent<CapsuleCollider>().enabled = true;
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
    public float Rescuing;
    public GameObject RescuingBar;
    public Slider Slider4Value;
    void OnTriggerStay(Collider other)
    {
        if(other.tag =="Player 1" && PlayerHealthBar.WaitForRescue)
        {
            
            if(Input.GetKey(KeyCode.Joystick1Button3))
            {
                //print("Player2"+Rescuing);
                Rescuing += Time.deltaTime;
                RescuingBar.SetActive(true);
                Slider4Value.value = Rescuing;
                if(Rescuing >= 3)
                {
                    
                    //playerHealthBar.Player1Rescued();
                    PlayerHealthBar.Player1WaitForRescue = false;
                    Rescuing = 0;
                    PlayerHealthBar.WaitForRescue = false;
                    Player1HealthBar.currentHealth = Player1HealthBar.maxHealth;
                    RescuingBar.SetActive(false);
                }
            }else if(Input.GetKeyUp(KeyCode.Joystick1Button3))
            {
                Rescuing = 0;
                RescuingBar.SetActive(false);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player 1")
        {
            Rescuing = 0;
            RescuingBar.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DeadZone1")
        {
            print("DeadZone1");
            this.transform.position = DeadZone1Positon.position;
            Player2HealthBar.currentHealth = 0;
            PlayerHealthBar.Player2WaitForRescue = true;
        }
        if(other.tag == "DeadZone2")
        {
            print("DeadZone2");
            this.transform.position = DeadZone2Positon.position;
            Player2HealthBar.currentHealth = 0;
            PlayerHealthBar.Player2WaitForRescue = true;
        }
        if(other.tag == "DeadZone3")
        {
            print("DeadZone3");
            this.transform.position = DeadZone3Positon.position;
            Player2HealthBar.currentHealth = 0;
            PlayerHealthBar.Player2WaitForRescue = true;
        }
        if(other.tag == "DeadZone3-2")
        {
            print("DeadZone3");
            this.transform.position = DeadZone3_2Positon.position;
            Player2HealthBar.currentHealth = 0;
            PlayerHealthBar.Player2WaitForRescue = true;
        }
        if(other.tag == "DeadZone4")
        {
            print("DeadZone4");
            this.transform.position = DeadZone4Positon.position;
            Player2HealthBar.currentHealth = 0;
            PlayerHealthBar.Player2WaitForRescue = true;
        }
        if(other.tag == "DeadZone5")
        {
            print("DeadZone5");
            this.transform.position = DeadZone5Positon.position;
            Player2HealthBar.currentHealth = 0;
            PlayerHealthBar.Player2WaitForRescue = true;
        }
    }

}
