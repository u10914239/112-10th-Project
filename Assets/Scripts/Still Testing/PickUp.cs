using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Transform weaponHolder;
    public Rigidbody rb;

    OnlyTransform onlyTransform;


    void Awake()
    {
        onlyTransform = gameObject.GetComponent<OnlyTransform>();
        weaponHolder = GameObject.Find("Weapon Holder").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();

    }
    void Start()
    {
        
        
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {


        if (collision.gameObject.tag == "Player" && onlyTransform.isTransformed == true)
        {
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
            transform.SetParent(weaponHolder);
            transform.localPosition = Vector3.zero;
            transform.localRotation= Quaternion.Euler (0, 0, 100);
        }

        
    }
}
