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
    public float moveSpeed = 0.3f;
    //?【主角狀態】
    public int magicNumber;
    public Text showMagicNumber;
    public Sprite Knight, Sword;
    public static bool isTransformed;
    public static bool isHolding;
    Vector3 movement;
    //?【環境狀態】
    float MagicTime;
    float PowerTime;
    //?【武器狀態】
    public SpriteRenderer SowrdObj;
    public Transform SwordPos;




    public Transform attackPoint;
    public float attackRange=0.5f;
    public LayerMask enemyLayers;
    public bool inRange;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); //*設定玩家剛體
        runSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
        Magic();
        Power();

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        
        Movement();
    }
    void FixedUpdate()
    {
        
    }
    void Movement()
    {
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.z = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.W))
        {
            movement.z = 1f;
        }else if(Input.GetKey(KeyCode.S))
        {
            movement.z = -1f;
        }else
        {
            movement.z = 0;
        }

        if(Input.GetKey(KeyCode.A))
        {
            movement.x = -1f;
        }else if(Input.GetKey(KeyCode.D))
        {
            movement.x = 1f;
        }else
        {
            movement.x = 0;
        }

        rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);

        if (movement.x > 0)
        {
            KnightForm.flipX = false;
            SowrdObj.transform.localRotation = Quaternion.Euler(0,0,-30);
            SwordPos.transform.localPosition = new Vector3 (9.3f,SwordPos.transform.localPosition.y,SwordPos.transform.localPosition.z);
        }
        else if (movement.x < 0)
        {
            KnightForm.flipX = true;
            SowrdObj.transform.localRotation = Quaternion.Euler(0,0,30);
            SwordPos.transform.localPosition = new Vector3 (-8.3f,SwordPos.transform.localPosition.y,SwordPos.transform.localPosition.z);
        }

        //*《玩家跳躍》
        if(isGrounded && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(0,5,0,ForceMode.Impulse);
        }
    }
    void playerMovement() //? 【玩家移動】
    {
        //*《玩家移動》
        if(isGrounded && Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0,0,moveSpeed,ForceMode.Impulse);
        }
        if(isGrounded && Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-moveSpeed,0,0,ForceMode.Impulse);
            KnightForm.flipX = true;
        }
        if(isGrounded && Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0,0,-moveSpeed,ForceMode.Impulse);
        }
        if(isGrounded && Input.GetKey(KeyCode.D))
        {
            rb.AddForce(moveSpeed,0,0,ForceMode.Impulse);
            KnightForm.flipX = false;
        }
        //*《玩家跳躍》
        if(isGrounded && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(0,5,0,ForceMode.Impulse);
        }
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
        }else
        {
            SowrdObj.enabled = false;
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
