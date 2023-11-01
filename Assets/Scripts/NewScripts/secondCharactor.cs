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
    [SerializeField] bool isGrounded; //*是否在地上
    public SpriteRenderer KnightForm;
    //?【主角素質】
    public float jumpPower = 100f;
    public float moveSpeed = 0.5f;
    //?【主角狀態】
    public int magicNumber;
    public Text showMagicNumber;
    public Sprite Knight, Bow;
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
        if(isGrounded && Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(0,0,moveSpeed,ForceMode.Impulse);
        }
        if(isGrounded && Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-moveSpeed,0,0,ForceMode.Impulse);
            KnightForm.flipX = true;
        }
        if(isGrounded && Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(0,0,-moveSpeed,ForceMode.Impulse);
        }
        if(isGrounded && Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(moveSpeed,0,0,ForceMode.Impulse);
            KnightForm.flipX = false;
        }
        //*《玩家跳躍》
        if(isGrounded && Input.GetKeyDown(KeyCode.RightAlt))
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
        if(magicNumber == 10 && Input.GetKeyDown(KeyCode.P))
        {
            KnightForm.sprite = Bow;
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
        if(mainCharactor.isHolding)
        {
            KnightForm.color = new Color32 (0,0,0,0);
        }else
        {
            KnightForm.color = new Color32 (255,255,255,255);
        }
    }
    void OnTriggerEnter(Collider Hit)
    {   
        if (Hit.gameObject.tag == "Ground")
        {
            isGrounded = true;
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
