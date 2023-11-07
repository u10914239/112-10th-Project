using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Transform weaponHolder;


    void Start()
    {
        weaponHolder = GameObject.Find("Weapon Holder").GetComponent<Transform>();
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.SetParent(weaponHolder);
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.Euler(0, 0, -60);
        }

        
    }
}
