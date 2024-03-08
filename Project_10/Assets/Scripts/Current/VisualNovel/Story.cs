using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    [SerializeField] private Text ShowWord;
    [SerializeField] private int Cue;
    [SerializeField] string[] Storys = new string[5];
    [SerializeField] StoryUI VisualNovel;
    public static int ScriptsVersion;
    public static bool ResetStory;
    public bool firstStory;
    void Start()
    {
        Cue = 0;
        ScriptsVersion = 0;
        firstStory = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(firstStory)
        {
            if(Cue < 1 && Input.GetMouseButtonDown(0) || Cue < 1 && Input.GetKeyDown(KeyCode.Joystick2Button3)|| Cue < 1 && Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                Cue = Cue += 1;
            }else if(Cue == 1 && Input.GetMouseButtonDown(0)|| Cue == 1 && Input.GetKeyDown(KeyCode.Joystick2Button3)|| Cue == 1 && Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                firstStory = false;
                ScriptsVersion = 1;
                Cue = 0;
                VisualNovel.VisualNovel.SetActive(false);
            }
        }else if(!firstStory)
        {
            print("Notfirst");
            if(Cue < Storys.Length-1 && Input.GetMouseButtonDown(0)|| Cue < Storys.Length-1 && Input.GetKeyDown(KeyCode.Joystick2Button3)|| Cue < Storys.Length-1 && Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
             Cue = Cue += 1;
            }else if(Cue == Storys.Length-1 && Input.GetMouseButtonDown(0)|| Cue == Storys.Length-1 && Input.GetKeyDown(KeyCode.Joystick2Button3)|| Cue == Storys.Length-1 && Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                VisualNovel.VisualNovel.SetActive(false);
            }
        }

        

        ShowWord.text = Storys[Cue];

        if(ScriptsVersion ==0)
        {
            Scripts0();
        }else if(ScriptsVersion == 1)
        {
            Scripts();
        }else if(ScriptsVersion == 2)
        {
            Scripts2();
        }

        if(ResetStory)
        {
            Cue = 0;
            ResetStory = false;
        }
    }
    void Scripts0()
    {
        Storys[0] = "「才剛離開王宮來到這座小鎮，就已經有魔物的味道了」";
        Storys[1] = "「前面人，問問看看這裡發生什麼事」 ";
    }
    void Scripts()
    {
        Storys[0] = "「史萊姆剛剛又接近了我們的村莊口」 ";
        Storys[1] = "「拜託勇者們再次去擊退那些怪物吧」」 ";
        Storys[2] = "「村莊口在紅色箭頭指網的方向」 ";
        Storys[3] = "「幫忙剷除史萊姆的話我們會給你一點報酬......」 ";
        Storys[4] = "看來我們第一個任務就是史萊姆了";
    }
    void Scripts2()
    {
        Storys[0] = "「太感謝你們的幫忙」 ";
        Storys[1] = "「因為有你們的協助」 ";
        Storys[2] = "「一天又平安的過去了，感謝勇者們的努力！」";
        Storys[3] = "「其實聽說藏有神器的地窖就是村莊口附近的那個......」 ";
        Storys[4] = "「真的嗎!」";
    }
    void Scripts3()
    {
        Storys[0] = " 「史萊姆剛剛攻擊了我的村莊」 ";
        Storys[1] = " 「我的Money Master村莊」 ";
        Storys[2] = " 「拜託有哪位勇者看到這個委託」 ";
        Storys[3] = " 「幫忙我剷除史萊姆的話我會給報酬」 ";
        Storys[4] = "看來我們第一個任務就是史萊姆了";
        Storys[3] = " 「這是我答應給你們的報酬...」 ";
        Storys[4] = "萬歲!!!";
    }
}
