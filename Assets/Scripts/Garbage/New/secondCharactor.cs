using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//!
//*
//?
////
//todo
public class secondCharactor : MonoBehaviour
{
    //?【主角設定】
    Rigidbody rb; //*玩家剛體
    [SerializeField] public static bool isGrounded; //*是否在地上
    public SpriteRenderer KnightForm;
    CapsuleCollider CapsuleCollider;
    //?【主角素質】
    public float jumpPower = 100f;
    public float runSpeed = 2f;
    public float moveSpeed = 0.5f;
    float stopSpeed_02 = 0f;
    //?【主角狀態】
    public static int magicNumber;
    public Text showMagicNumber;
    public Sprite Knight, Bow;
    public static bool isTransformed;
    public static bool isHolding;
    bool facingRight = true;
    Vector3 movement;
    public SpriteRenderer showTransformTime;
    public GameObject showTransformTimeObject;
    public Sprite T3,T2,T1;
    public static int playerHealth;
    public int playerHealthSide;
    public static bool GetAttacked;
    public static bool unHurt;
    public float dashCoolDown;

    public GameObject bar1,bar2,bar3,bar4,bar5;

    //?【環境狀態】
    float MagicTime;
    public static float PowerTime;
    public Transform Main;

    public Transform attackPoint;
    public float attackRange=0.5f;
    public LayerMask enemyLayers;

    public Animator anim_02;

    //todo
    public SpriteRenderer InvisibleWhenTransform;
    public GameObject TransformObj;
    public AudioSource Punch;
    public AudioSource Pop;
    
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); //*設定玩家剛體
        PowerTime = 0;
        showTransformTimeObject.SetActive(false);
        CapsuleCollider = GetComponent<CapsuleCollider>();
        runSpeed = 5f;
        playerHealth = 5;

    }

    // Update is called once per frame
    void Update()
    {
       Magic();
       Power();
       Health();
       if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            
        }
        GotAttacked();
        stopSpeed_02 = Mathf.Abs(Input.GetAxisRaw("Horizontal2") * runSpeed) + Mathf.Abs(Input.GetAxisRaw("Vertical2") * runSpeed);
        anim_02.SetFloat("Speed_02", Mathf.Abs(stopSpeed_02));



        movement.x = Input.GetAxisRaw("Horizontal2");
        movement.z = Input.GetAxisRaw("Vertical2");

        playerHealthSide = playerHealth;
    }
    void FixedUpdate()
    {  
        Movement_02();
    }
    void Health()
    {
        if(playerHealth>=5)
        {
            bar1.SetActive(true);
            bar2.SetActive(true);
            bar3.SetActive(true);
            bar4.SetActive(true);
            bar5.SetActive(true);
        }else if(playerHealth == 4)
        {
            bar1.SetActive(true);
            bar2.SetActive(true);
            bar3.SetActive(true);
            bar4.SetActive(true);
            bar5.SetActive(false);
        }
        else if(playerHealth == 3)
        {
            bar1.SetActive(true);
            bar2.SetActive(true);
            bar3.SetActive(true);
            bar4.SetActive(false);
            bar5.SetActive(false);
        }
        else if(playerHealth == 2)
        {
            bar1.SetActive(true);
            bar2.SetActive(true);
            bar3.SetActive(false);
            bar4.SetActive(false);
            bar5.SetActive(false);
        }
        else if(playerHealth == 1)
        {
            bar1.SetActive(true);
            bar2.SetActive(false);
            bar3.SetActive(false);
            bar4.SetActive(false);
            bar5.SetActive(false);
        }
    }

    void Movement_02()
    {
        rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
        
        if(dashCoolDown ==0 && movement.x>0 && Input.GetKey(KeyCode.LeftControl))
        {
            rb.AddForce(20,0,0,ForceMode.Impulse);
            dashCoolDown = 2;
        }
        if(dashCoolDown ==0 && movement.x<0 && Input.GetKey(KeyCode.Joystick1Button12))
        {
            rb.AddForce(-20,0,0,ForceMode.Impulse);
            dashCoolDown = 2;
        }

        if(dashCoolDown>1)
        {
            dashCoolDown -= Time.deltaTime;
        }else if(dashCoolDown<1)
        {
            dashCoolDown = 0;
        }

        if (movement.x > 0&&!facingRight)
        {
            Flip();
        }
        else if (movement.x < 0&& facingRight)
        {
            Flip();
        }

        //*《玩家跳躍》
        if(isGrounded && Input.GetKey(KeyCode.Joystick1Button0))
        {
            
            print("fff");
            rb.AddForce(0,jumpPower,0,ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            runSpeed = 10f;
        }else
        {
            runSpeed = 5f;
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    void Magic()
    {
        MagicTime += Time.deltaTime;
        if(magicNumber >=10)
        {
            magicNumber = 10;
        }
        if(MagicTime >=1 && magicNumber < 10)
        {
            magicNumber += 1;
            MagicTime = 0;
        }
        showMagicNumber.text = magicNumber.ToString();
    }
    void Power()
    {
        if(magicNumber == 10 && Input.GetKeyDown(KeyCode.Joystick1Button1) || magicNumber == 10 && Input.GetKeyDown(KeyCode.P))
        {
            //KnightForm.sprite = Bow;
            InvisibleWhenTransform.enabled=false;
            TransformObj.SetActive(true);
            isTransformed = true;
            magicNumber = 0;
            FollowEnemy.smallWaveActivate = true;
            Pop.Play();
        }
        if(isTransformed == true)
        {
            PowerTime += Time.deltaTime;

            if(PowerTime == 0)
            {
                showTransformTimeObject.SetActive(false);
            }else if(PowerTime <=1)
            {
                showTransformTimeObject.SetActive(true);
                showTransformTime.sprite = T3;
                
            }else if(PowerTime <=4)
            {
                showTransformTime.sprite = T2;
                
                FollowEnemy.smallWaveActivate = false;
            }else if(PowerTime >= 7)
            {
                showTransformTime.sprite = T1;
            }


            if(PowerTime > 10 || isTransformed == false)
            {
                //KnightForm.sprite = Knight;
                //KnightForm.transform.localScale = new Vector3 (0.06f,0.06f,0.06f);
                InvisibleWhenTransform.enabled = true;
                TransformObj.SetActive(false);
                isTransformed = false;
                PowerTime = 0;
                showTransformTimeObject.SetActive(false);
                Pop.Play();
            }
        }
        if(mainCharactor.isHolding)
        {
            KnightForm.color = new Color32 (0,0,0,0);
            TransformObj.SetActive(false);
            this.transform.position = Main.transform.position;
            Debug.Log("fhdiufiuhdshfuidshuishdfui");
            CapsuleCollider.enabled = false;
        }else if(!mainCharactor.isHolding)
        {
            KnightForm.transform.localScale = new Vector3 (3f,3f,3f);
            KnightForm.color = new Color32 (255,255,255,255);
            CapsuleCollider.enabled = true;
        }

    }
    public void GotAttacked()
    {
        if(GetAttacked)
        {
            if(!unHurt)
            {
                KnightForm.color = Color.red;
                playerHealth -= 1;
                Punch.Play();
            }
            unHurt = true;
            
            Invoke("colorwhite", 0.3f);
            GetAttacked = false;
        }
        if(playerHealth<=0)
        {

        }
    }
    public void colorwhite()
    {
        KnightForm.color = Color.white;
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void OnTriggerEnter(Collider Hit)
    {   

    }
    void OnTriggerStay(Collider Hit)
    {
        
        if(mainCharactor.isTransformed)
        {
            if(Hit.gameObject.tag == "Second")
        {
            if(secondCharactor.isTransformed && Input.GetKeyDown(KeyCode.E))
            {
                isHolding = true;
            }
            if(!secondCharactor.isTransformed)
            {
                isHolding = false;
            }
        }
        }
        
    }
    void OnTriggerExit(Collider Hit)
    {

       if (Hit.gameObject.tag == "Ground")
       {
           isGrounded = false;
       }
    }
}
