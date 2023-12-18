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
    void Start()
    {
        Cue = 0;
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

        Scripts();
    }
    void Scripts()
    {
        Storys[0] = "第一段";
        Storys[1] = "第二段";
        Storys[2] = "第三段";
        Storys[3] = "第四段";
        Storys[4] = "第五段";
    }
}
