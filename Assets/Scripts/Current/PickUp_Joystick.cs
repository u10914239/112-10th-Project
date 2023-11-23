using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Joystick : MonoBehaviour
{
    private Transform weaponHolder;
    
    public bool isHeld;

    PlayerController player1;
    PlayerController_Joystick player2;


    void Awake()
    {
        player1 = GameObject.Find("Player 1").GetComponent<PlayerController>();
        player2 = gameObject.GetComponent<PlayerController_Joystick>();
        weaponHolder = GameObject.Find("Weapon Holder 1").GetComponent<Transform>();
        

    }
    void Start()
    {
        isHeld = false;

    }


    void FixedUpdate()
    {

        if (player2.isTransformed == false)
        {
            
            this.transform.SetParent(null);
            isHeld = false;
            
            

        }
        

    }

    

    private void OnTriggerEnter(Collider collision)
    {


        if (collision.gameObject.tag == "Player" && player2.isTransformed == true)
        {
            
            this.transform.SetParent(weaponHolder);
            transform.localPosition = Vector3.zero;
            isHeld = true;

        }
       


    }

    


}