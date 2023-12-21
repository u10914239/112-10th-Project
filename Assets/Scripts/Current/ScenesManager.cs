using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] bool Scene1,Scene2;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level_0");
    }
    public void Cover()
    {
        SceneManager.LoadScene("Cover");
    }
    public void EndCover()
    {
        SceneManager.LoadScene("EndCover");
    }
    public void FrontStory()
    {
        SceneManager.LoadScene("FrontStory");
    }


    void OnTriggerStay(Collider Player)
    {
        if(Scene1)
        {
            if(Player.gameObject.tag == "Player 1" || Player.gameObject.tag == "Player 2")
            {
                SceneManager.LoadScene("Level_1");
            }
        }
        if(Scene2)
        {
            if(Player.gameObject.tag == "Player 1" || Player.gameObject.tag == "Player 2")
            {
                SceneManager.LoadScene("Level_2");
            }
        }

        
    }
}
