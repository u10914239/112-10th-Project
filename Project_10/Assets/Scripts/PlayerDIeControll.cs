using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDIeControll : MonoBehaviour
{
    public GameObject player1, player2;
    PlayerHealthBar playerHealth;

    void Start()
    {



    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            playerHealth = player1.GetComponent<PlayerHealthBar>();
            playerHealth.Die();

        }


    }
}
