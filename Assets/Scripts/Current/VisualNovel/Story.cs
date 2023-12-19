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
    void Start()
    {
        Cue = 0;
        ScriptsVersion = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Cue < Storys.Length-1 && Input.GetMouseButtonDown(0))
        {
            Cue = Cue += 1;
        }else if(Cue == Storys.Length-1 && Input.GetMouseButtonDown(0))
        {
            VisualNovel.VisualNovel.SetActive(false);
        }

        ShowWord.text = Storys[Cue];

        if(ScriptsVersion == 1)
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
    void Scripts()
    {
        Storys[0] = " 「史萊姆剛剛攻擊了我的村莊」 ";
        Storys[1] = " 「我的Coin Master村莊」 ";
        Storys[2] = " 「拜託有哪位勇者看到這個委託」 ";
        Storys[3] = " 「幫忙我剷除史萊姆的話我會給報酬」 ";
        Storys[4] = "看來我們第一個任務就是史萊姆了";
    }
    void Scripts2()
    {
        Storys[0] = " 「太感謝你們的幫忙」 ";
        Storys[1] = " 「因為有你們的協助」 ";
        Storys[2] = "「一天又平安的過去了，感謝勇者們的努力！」";
        Storys[3] = " 「這是我答應給你們的報酬...」 ";
        Storys[4] = "萬歲!!!";
    }
}
