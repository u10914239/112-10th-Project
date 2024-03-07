using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject DemoEnd;
    public static int PlayerKillCount;
    public PlayerHealthBar player1HealthBar;
    public PlayerHealthBar player2HealthBar;
    public GameObject ExitToLevel2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mission();
        GameOver();
        LevelChange();
    }

    void Mission()
    {
        if(PlayerKillCount >= 5)
        {
            print("Boss Spawn");
            BossSpawn.BossTrigger = true;
        }
    }
    void GameOver()
    {
        if(player1HealthBar.currentHealth <= 0 && player2HealthBar.currentHealth <= 0)
        {
            GameOverScreen.SetActive(true);
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
    void LevelChange()
    {
        if(EnemyHealth.SlimeBossDead)
        {
            ExitToLevel2.SetActive(true);
        }
    }
}
