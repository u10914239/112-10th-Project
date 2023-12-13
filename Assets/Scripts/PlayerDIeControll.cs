using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDIeControll : MonoBehaviour
{
    public GameObject player1, player2;
    PlayerHealth playerHealth;

    void Start()
    {



    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            playerHealth = player2.GetComponent<PlayerHealth>();
            playerHealth.Die();

        }


    }
}
