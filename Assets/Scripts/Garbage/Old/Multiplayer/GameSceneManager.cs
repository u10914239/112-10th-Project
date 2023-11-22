using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.CurrentRoom == null)
        {
            SceneManager.LoadScene("Lobby");
        }else
        {
            InitGame();
        }
    }
    public void InitGame()
    {
        print("ddddddddddddddddddddddddddd");
        float spawnPointX = Random.Range(-41,-35);
        float spawnPointY = 2f;
        PhotonNetwork.Instantiate("Charactor" , new Vector3(spawnPointX, spawnPointY, 90f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
