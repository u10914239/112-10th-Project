using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int attackDamage = 40;
    Enemy em;

    void Start()
    {
        em = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = em.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Enemy")
        {


            em.TakeDamage(attackDamage);



        }


    }
}
