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
    public float speed;
    public int PosionType;
    public Text PosionTypeShow;
    public GameObject Charactor0;
    public GameObject Charactor1;
    public GameObject Charactor2;
    public GameObject Charactor3;
    public SpriteRenderer Knight;
    //?【NPC設定】
    SpriteRenderer NPCPic; //* (NPC圖片)
    public bool Carrying = false; //*(正在被搬運)
    //?【互動設定】
    public GameObject sign; //*(告示牌)
    public Text MissionComplete;
    //?【怪物設定】
    public static int KillCount;
    public Text KillGoal;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); //* (尋找剛體)
        speed = 0.5f;
        PosionType = 1;
    }

    void Update()
    {
        PlayerMovement();
        Posion();
        Mission();

        //Charactor1.transform.position = Charactor0.transform.position;
        //Charactor2.transform.position = Charactor0.transform.position;
        //Charactor3.transform.position = Charactor0.transform.position;
    }
    void PlayerMovement() //? 【玩家移動】
    {
        if(Input.GetAxisRaw("Horizontal") > 0.1f)
        {
            rb.AddForce(speed,0,0,ForceMode.Impulse);
            Knight.flipX = false;
        }else if(Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            rb.AddForce(-speed,0,0,ForceMode.Impulse);
            Knight.flipX = true;
        }
        if(Input.GetAxisRaw("Vertical") > 0.1f)
        {
            rb.AddForce(0,0,speed,ForceMode.Impulse);
        }else if(Input.GetAxisRaw("Vertical") < -0.1f)
        {
            rb.AddForce(0,0,-speed,ForceMode.Impulse);
        }
    }
    void OnTriggerStay(Collider NPC) //? 【偵測NPC】
    {
        NPCPic = NPC.GetComponent<SpriteRenderer>(); //* (抓取圖形渲染)

        if(NPC.gameObject.tag == "NPC" && Input.GetKey(KeyCode.F)) //* (如果碰到NPC而且按下F鍵)
        {
            NPCPic.color = new Color32 (0,0,0,0); //* (圖片變透明)
            Carrying = true; //* (正在搬運)
            speed = 0.15f;
            CharactorChange();
        }
        if(Carrying == true && NPC.gameObject.tag == "NPC" && Input.GetKey(KeyCode.LeftShift)) //* (如果碰到NPC而且按下F鍵)
        {
            NPCPic.color = new Color32 (255,255,255,255); //* (圖片變透明)
            Carrying = false; //* (已經卸下)
            Charactor1.SetActive(false);
            Charactor2.SetActive(false);
            Charactor3.SetActive(false);
            speed = 0.2f;
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
