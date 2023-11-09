using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Transform weaponHolder;
    
    public bool isHeld;
    
    PlayerController player1;
    PlayerController_Joystick player2;

    void Awake()
    {
        player1 = gameObject.GetComponent<PlayerController>();
        player2 = GameObject.Find("Player 2").GetComponent<PlayerController_Joystick>();
        weaponHolder = GameObject.Find("Weapon Holder 2").GetComponent<Transform>();
        

    }
    void Start()
    {
        isHeld = false;
        
    }

   
    void FixedUpdate()
    {

        if (player1.isTransformed == false)
        {
            
            this.transform.SetParent(null);
            isHeld = false;



        }
        if (player1.isTransformed == true)
        {
            if (player2.facingRight == true)
            {
                player1.facingRight = true;
            }
            else
            {
                player1.facingRight = false;
            }
        }


    }

    private void OnTriggerEnter(Collider collision)
    {


        if (collision.gameObject.tag == "Player" && player1.isTransformed == true)
        {
            
            this.transform.SetParent(weaponHolder);
            transform.localPosition = Vector3.zero;
            isHeld = true;

        }
        

    }

    
}
