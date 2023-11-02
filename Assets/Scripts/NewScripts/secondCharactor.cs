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
    public int magicNumber;
    public Text showMagicNumber;
    public Sprite Knight, Bow;
    public static bool isTransformed;
    public static bool isHolding;
    bool facingRight = true;
    Vector3 movement;
    public SpriteRenderer showTransformTime;
    public GameObject showTransformTimeObject;
    public Sprite T3,T2,T1;
    //?【環境狀態】
    float MagicTime;
    float PowerTime;
    public Transform Main;

    public Transform attackPoint;
    public float attackRange=0.5f;
    public LayerMask enemyLayers;

    public Animator anim_02;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); //*設定玩家剛體
        PowerTime = 0;
        showTransformTimeObject.SetActive(false);
        CapsuleCollider = GetComponent<CapsuleCollider>();
        runSpeed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
       Magic();
       Power();
       if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Attack();
        }

        stopSpeed_02 = Mathf.Abs(Input.GetAxisRaw("Horizontal2") * runSpeed) + Mathf.Abs(Input.GetAxisRaw("Vertical2") * runSpeed);
        anim_02.SetFloat("Speed_02", Mathf.Abs(stopSpeed_02));



        movement.x = Input.GetAxisRaw("Horizontal2");
        movement.z = Input.GetAxisRaw("Vertical2");
    }
    void FixedUpdate()
    {  
        Movement_02();
    }

    void Movement_02()
    {
        rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
        


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

        if(MagicTime >=1 && magicNumber < 10)
        {
            magicNumber += 1;
            MagicTime = 0;
        }
        showMagicNumber.text = magicNumber.ToString();
    }
    void Power()
    {
        if(magicNumber == 10 && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            KnightForm.sprite = Bow;
            isTransformed = true;
            magicNumber = 0;
            FollowEnemy.smallWaveActivate = true;
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


            if(PowerTime > 10)
            {
                KnightForm.sprite = Knight;
                //KnightForm.transform.localScale = new Vector3 (0.06f,0.06f,0.06f);
                isTransformed = false;
                PowerTime = 0;
                showTransformTimeObject.SetActive(false);
            }
        }
        if(mainCharactor.isHolding)
        {
            KnightForm.color = new Color32 (0,0,0,0);
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
    public void Attack()
    {

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
