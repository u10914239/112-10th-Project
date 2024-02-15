using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Joystick : MonoBehaviour
{
    public Transform weaponHolder;
    public bool isHeld;
    
    private SpriteRenderer weaponSprite;
    PlayerController player1;
    PlayerController_Joystick player2;


    void Awake()
    {
        player1 = GameObject.Find("Player 1").GetComponent<PlayerController>();
        player2 = gameObject.GetComponent<PlayerController_Joystick>();
        weaponHolder = GameObject.Find("Weapon Holder 1").GetComponent<Transform>();
        weaponSprite = GetComponentInChildren<SpriteRenderer>();

    }
    void Start()
    {
        isHeld = false;

    }

    void Update()
    {

        
    }

    void FixedUpdate()
    {

        if (player2.isTransformed == false)
        {
            weaponSprite.enabled = true;
            this.transform.SetParent(null);
            isHeld = false;
            player2.transform.rotation = player2.initialGlobalRotation;


        }
        

    }

    

    private void OnTriggerEnter(Collider collision)
    {


        if (collision.gameObject.tag == "Player 1" && player2.isTransformed == true)
        {
            weaponSprite.enabled = false;
            this.transform.SetParent(weaponHolder);
            transform.localPosition = Vector3.zero;
            isHeld = true;

           

        }
       


    }

    


}
