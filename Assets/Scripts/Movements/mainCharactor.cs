using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//!
//*
//?
////
//todo
public class mainCharactor : MonoBehaviour
{
    //?【主角設定】
    Rigidbody rb; //*玩家剛體
    [SerializeField] public static bool isGrounded; //*是否在地上
    public SpriteRenderer KnightForm;
    //?【主角素質】
    public float jumpPower = 100f;
    public float runSpeed = 2f;
    float stopSpeed = 0f;
    
    //?【主角狀態】
    public static int magicNumber;
    public Text showMagicNumber;
    public Sprite Knight, Sword;
    public static bool isTransformed;
    public static bool isHolding;
    bool facingRight = true;
    Vector3 movement;
    public static int playerHealth;
    public int playerHealthSide;
    public static bool GetAttacked;
    public static bool unHurt;
    public float dashCoolDown;

    public GameObject bar1,bar2,bar3,bar4,bar5;
    //?【環境狀態】
    float MagicTime;
    float PowerTime;
    //?【武器狀態】
    public SpriteRenderer SowrdObj;
    public Transform SwordPos;

    public Animator anim;
    //public Animator attackEffect;


    public Transform attackPoint;
    public float attackRange=0.5f;
    public LayerMask enemyLayers;
    public bool inRange;

    public GameObject AmazingAnimation;
    public AudioSource Punch;
    public AudioSource Epic;
    public AudioSource Reload;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); //*設定玩家剛體
        runSpeed = 5f;
        playerHealth = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
        Magic();
        Power();
        GotAttacked();
        Health();

        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal") * runSpeed) + Mathf.Abs(Input.GetAxisRaw("Vertical") * runSpeed);
        anim.SetFloat("Speed", Mathf.Abs(stopSpeed));

        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        playerHealthSide = playerHealth;
        
    }
    void FixedUpdate()
    {
        Movement();
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
    void Movement()
    {
        rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
        
        if(dashCoolDown ==0 && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftControl))
        {
            rb.AddForce(20,0,0,ForceMode.Impulse);
            dashCoolDown = 2;
        }
        if(dashCoolDown ==0 && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftControl))
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
            SwordPos.transform.rotation = Quaternion.Euler(0,0,-30);
            //SwordPos.transform.localPosition = new Vector3 (1.18f,SwordPos.transform.localPosition.y,SwordPos.transform.localPosition.z);
        }
        else if (movement.x < 0&&facingRight)
        {
            Flip();
            SwordPos.transform.rotation = Quaternion.Euler(0,0,30);
            //SwordPos.transform.localPosition = new Vector3 (-1.18f,SwordPos.transform.localPosition.y,SwordPos.transform.localPosition.z);
        }

        //*《玩家跳躍》
        if(isGrounded && Input.GetKey(KeyCode.Space))
        {
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
        //MagicTime += Time.deltaTime;
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
        if(magicNumber == 10 && Input.GetKeyDown(KeyCode.T))
        {
            KnightForm.sprite = Sword;
            isTransformed = true;
            magicNumber = 0;
        }
        if(isTransformed == true)
        {
            PowerTime += Time.deltaTime;
            if(PowerTime > 10)
            {
                KnightForm.sprite = Knight;
                KnightForm.transform.localScale = new Vector3 (0.06f,0.06f,0.06f);
                isTransformed = false;
                PowerTime = 0;
            }
        }
        if(!isHolding && inRange && secondCharactor.isTransformed && Input.GetKeyDown(KeyCode.E))
        {
            Reload.Play();
        }
        if(inRange && secondCharactor.isTransformed && Input.GetKeyDown(KeyCode.E))
            {
                isHolding = true;
            }
            if(!secondCharactor.isTransformed)
            {
                isHolding = false;
            }
        if(isHolding)
        {
            SowrdObj.enabled = true;
            if(secondCharactor.PowerTime <9 && Input.GetKeyDown(KeyCode.F))
            {
                AmazingAnimation.SetActive(true);
                FollowEnemy.smallWaveActivate = true;
                secondCharactor.PowerTime = 9f;
                Epic.Play();
            }
        }else
        {
            AmazingAnimation.SetActive(false);
            SowrdObj.enabled = false;
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
        
            if(Hit.gameObject.tag == "Second")
        {
            inRange = true;
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
