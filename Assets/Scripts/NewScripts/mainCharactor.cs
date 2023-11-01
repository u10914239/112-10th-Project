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
    [SerializeField] bool isGrounded; //*是否在地上
    public SpriteRenderer KnightForm;
    //?【主角素質】
    public float jumpPower = 100f;
    public float moveSpeed = 0.3f;
    //?【主角狀態】
    public int magicNumber;
    public Text showMagicNumber;
    public Sprite Knight, Sword;
    public static bool isTransformed;
    public static bool isHolding;
    //?【環境狀態】
    float MagicTime;
    float PowerTime;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); //*設定玩家剛體
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        Magic();
        Power();
        
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
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0,15,0,ForceMode.Impulse);
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
            if(PowerTime > 5)
            {
                KnightForm.sprite = Knight;
                KnightForm.transform.localScale = new Vector3 (0.06f,0.06f,0.06f);
                isTransformed = false;
                PowerTime = 0;
            }
        }
    }
    void OnTriggerEnter(Collider Hit)
    {   
        if (Hit.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        
    }

    void OnTriggerStay(Collider Hit)
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

    void OnTriggerExit(Collider Hit)
    {

       if (Hit.gameObject.tag == "Ground")
       {
           isGrounded = false;
       }
    }
}
