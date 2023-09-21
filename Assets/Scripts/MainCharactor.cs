using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    //!
    //*
    //?
    ////
    //todo

public class MainCharactor : MonoBehaviour
{
    //?【主角設定】
    public Rigidbody rb; //* (角色剛體)
    public float runSpeed;
    public float jumpPower;
    float stopSpeed = 0f;
    public Animator animator;

    public Transform attackPoint;
    public float attackRange=0.5f;
    public LayerMask enemyLayers;

    public int PosionType;
    public int cType;
    public Text PosionTypeShow;
    public GameObject Charactor0;
    public GameObject Charactor1;
    public GameObject Charactor2;
    public GameObject Charactor3;
    public Sprite C0,C1,C2,C3;
    public SpriteRenderer Knight;
    Vector3 movement;
    //?【NPC設定】
    SpriteRenderer NPCPic; //* (NPC圖片)
    public bool Carrying = false; //*(正在被搬運)
    //?【互動設定】
    public GameObject sign; //*(告示牌)
    public Text MissionComplete;
    //?【怪物設定】
    public static int KillCount;
    public Text KillGoal;
    public bool isGrounded;


   

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); //* (尋找剛體)
        
        PosionType = 1;
        isGrounded = true;
        cType = 0;
    }

    void Update()
    {
        Jump();
        Posion();
        Mission();
        stopSpeed = Mathf.Abs(Input.GetAxisRaw("Horizontal") * runSpeed)+ Mathf.Abs(Input.GetAxisRaw("Vertical") * runSpeed);
        

        if(cType ==0)
        {
            animator.SetFloat("Speed", Mathf.Abs(stopSpeed));
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();

        }

        if (movement.x > 0)
        {
            Knight.flipX = false;
        }
        else if (movement.x < 0)
        {
            Knight.flipX = true;
        }
        
        CharactorChange();

        Debug.Log(cType);
        //Debug.Log(speed);
        //Charactor1.transform.position = Charactor0.transform.position;
        //Charactor2.transform.position = Charactor0.transform.position;
        //Charactor3.transform.position = Charactor0.transform.position;
    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement * runSpeed * Time.fixedDeltaTime);
        
      
    }
    void Jump() //? 【玩家移動】
    {
        
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            
            animator.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.y, jumpPower);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
       
    }

    public void Attack()
    {
        if(cType ==0)
        {
            animator.SetTrigger("Attack");
        }
        

        Collider[] hitEnemies=Physics.OverlapSphere(attackPoint.position,attackRange,enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<FollowEnemy>().TakeDamage(1);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    void OnTriggerEnter(Collider Ground)
    {

        if (Ground.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        //animator.SetBool("isJumping", false);
    }

    void OnTriggerExit(Collider Ground)
    {

       if (Ground.gameObject.tag == "Ground")
       {
           isGrounded = false;
       }
       //animator.SetBool("isJumping", false);

    }

    void OnTriggerStay(Collider NPC) //? 【偵測NPC】
    {


        NPCPic = NPC.GetComponent<SpriteRenderer>(); //* (抓取圖形渲染)

        if(NPC.gameObject.tag == "NPC" && Input.GetKey(KeyCode.F)) //* (如果碰到NPC而且按下F鍵)
        {
            NPCPic.color = new Color32 (0,0,0,0); //* (圖片變透明)
            Carrying = true; //* (正在搬運)
            runSpeed = 0.15f;
            CharactorChange();
        }
        if(Carrying == true && NPC.gameObject.tag == "NPC" && Input.GetKey(KeyCode.LeftShift)) //* (如果碰到NPC而且按下F鍵)
        {
            NPCPic.color = new Color32 (255,255,255,255); //* (圖片變透明)
            Carrying = false; //* (已經卸下)
            Charactor1.SetActive(false);
            Charactor2.SetActive(false);
            Charactor3.SetActive(false);
            runSpeed = 0.2f;
        }
        if(Carrying == true && NPC.gameObject.tag == "NPC")
        {
            NPC.transform.position = transform.position;
        }
        if(NPC.gameObject.tag == "Sign" && Input.GetKey(KeyCode.F))
        {
            sign.SetActive(true);
        }
        if(NPC.gameObject.tag == "Sign" && Input.GetKey(KeyCode.Escape))
        {
            sign.SetActive(false);
        }
    }
    void Posion()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PosionType = PosionType + 1;
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            PosionType = PosionType - 1;
        }
        if(PosionType<=0)
        {
            PosionType = 3;
        }
        if(PosionType>=4)
        {
            PosionType = 1;
        }
        PosionTypeShow.text = PosionType.ToString();

        
    }
    void CharactorChange()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            cType+=1;

            if(cType==1)
        {
            Knight.sprite = C1;
        }else if(cType==2)
        {
            Knight.sprite = C2;
        }else if(cType==3)
        {
            Knight.sprite = C3;
        }else if(cType==0)
        {
            Knight.sprite = C0;
        }
        }

        
        if(cType >=4)
        {
            cType = 0;
        }
    }
    void FailCharactorChange()
    {
        if(PosionType == 1)
        {
            Charactor1.SetActive(true);
            Charactor2.SetActive(false);
            Charactor3.SetActive(false);
        }else if(PosionType == 2)
        {
            Charactor2.SetActive(true);
            Charactor1.SetActive(false);
            Charactor3.SetActive(false);
        }else if(PosionType == 3)
        {
            Charactor3.SetActive(true);
            Charactor2.SetActive(false);
            Charactor1.SetActive(false);
        }
    }
    void Mission()
    {
        if(KillCount >= 3)
        {
            KillGoal.text = "任務完成";
            MissionComplete.text = "\n- 公告 - \n\n感謝神秘的勇者消滅了所有占據地盤的怪物";
            Invoke("Reset",3f);
            KillCount = 0;
        }
    }
    void Reset()
    {
        KillGoal.text = "";
    }
}
