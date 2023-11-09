using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Joystick : MonoBehaviour
{
    private Transform weaponHolder;
    public Rigidbody rb;
    public bool isHeld;

    PlayerController_Joystick player2;


    void Awake()
    {
        player2 = gameObject.GetComponent<PlayerController_Joystick>();
        weaponHolder = GameObject.Find("Weapon Holder 1").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();

    }
    void Start()
    {
        isHeld = false;

    }


    void FixedUpdate()
    {

        if (player2.isTransformed == false)
        {
            rb.isKinematic = false;
            this.transform.SetParent(null);
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            //transform.Rotate(new Vector3(0, 180, 0));


        }


    }

    private void OnTriggerEnter(Collider collision)
    {


        if (collision.gameObject.tag == "Player" && player2.isTransformed == true)
        {
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
            this.transform.SetParent(weaponHolder);
            transform.localPosition = Vector3.zero;
            isHeld = true;

        }
        else
        {
            isHeld = false;
        }


    }

    


}
