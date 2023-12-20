using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat_Joystick_Wizard : MonoBehaviour
{

    //public LayerMask enemyLayerMask;
    //public float detectionRange = 10f; // Range within which the player should face the enemy

    public Animator anim;
    public Transform firePoint;
    public GameObject fireballPrefab;
    public GameObject laserPrefab;
    public float fireballSpeed = 10f;
    public float fireRate = 1f;

    private float nextFireTime = 0f;
   
    

    void Start()
    {
       
        
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Joystick1Button5) && Time.time >= nextFireTime)
        {
            
            anim.SetTrigger("Attack");
            nextFireTime = Time.time + 1f / fireRate;

        }


    }

    void ShootFireball()
    {
        // Create a fireball at the fire point position and rotation
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

        // Set fireball's initial velocity along the X-axis
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.velocity = transform.right * fireballSpeed; // fireballSpeed is a variable determining fireball speed

        


    }

    void ShootLaser()
    {
        GameObject laser = Instantiate(laserPrefab, firePoint.position, transform.rotation);
       
    }

}
