using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Transform weaponHolder;
    public Rigidbody rb;
    public bool isHeld;
    
    PlayerController player;


    void Awake()
    {
        player = gameObject.GetComponent<PlayerController>();
        weaponHolder = GameObject.Find("Weapon Holder 2").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();

    }
    void Start()
    {
        isHeld = false;
        
    }

   
    void FixedUpdate()
    {
        
        if(player.isTransformed == false)
        {
            rb.isKinematic = false;
            this.transform.SetParent(null);
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            


        }


    }

    private void OnTriggerEnter(Collider collision)
    {


        if (collision.gameObject.tag == "Player" && player.isTransformed == true)
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
