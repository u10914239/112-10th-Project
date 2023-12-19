using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject DemoEnd;
    public static int PlayerKillCount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mission();
    }

    void Mission()
    {
        if(PlayerKillCount >= 5)
        {
            print("Boss Spawn");
            BossSpawn.BossTrigger = true;
        }
    }

    void Trash()
    {
        if (mainCharactor.playerHealth <= 0 || secondCharactor.playerHealth <= 0)
        {
            GameOverScreen.SetActive(true);
        }
        if (mainEnd.ReachEnd && secondEnd.ReachEnd)
        {
            DemoEnd.SetActive(true);
        }
        else
        {
            DemoEnd.SetActive(false);
        }
    }
}
