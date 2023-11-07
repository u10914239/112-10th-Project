using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Transform weaponHolder;
    public Rigidbody rb;

    void Start()
    {
        weaponHolder = GameObject.Find("Weapon Holder").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();

    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
            transform.SetParent(weaponHolder);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(0, 0, 120);
        }

        
    }
}
