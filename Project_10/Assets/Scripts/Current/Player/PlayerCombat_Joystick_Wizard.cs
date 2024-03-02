using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat_Joystick_Wizard : MonoBehaviour
{

    //public LayerMask enemyLayerMask;
    //public float detectionRange = 10f; // Range within which the player should face the enemy

    public Animator anim;
    public Transform firePoint;
    public GameObject fireballPrefab;
    public GameObject laserPrefab;
    public GameObject swordgasPrefab;
    public float fireballSpeed = 10f;
    public float fireRate = 1f;

    private float nextFireTime = 0f;

    PlayerController_Joystick movement;
    public Text MagicShow;
    public static float MagicAmount;
    public AudioSource Pop;
    public AudioSource Swoosh2;
    public AudioSource Punch;
    public PlayerHealthBar playerHealthBar;
    public Coroutine recharge;
    void Start()
    {
        movement = GetComponentInParent<PlayerController_Joystick>();
        MagicAmount = 100;

    }

    private void Update()
    {


        if (playerHealthBar.currentStamina>=10 && Input.GetKeyDown(KeyCode.Joystick1Button5) && Time.time >= nextFireTime && !movement.isHoldingGod)
        {
            
            anim.SetTrigger("Attack");
            nextFireTime = Time.time + 1f / fireRate;
            Pop.Play();
            
            playerHealthBar.StartRecover = false;
        playerHealthBar.currentStamina -=10;
        if(recharge != null) StopAllCoroutines();
        recharge = StartCoroutine(Recover());

        //StopCoroutine(Recover());
        //StartCoroutine(Recover());

        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button5) && Time.time >= nextFireTime && movement.isHoldingGod)
        {
            //GodAttack();

            anim.SetTrigger("GodAttack");
        }

        Magic();
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(1.5f);
        playerHealthBar.StaminaRecover();
        //if(playerHealthBar.currentStamina >= playerHealthBar.MaxStamina)
        //yield return new WaitForSeconds(1);
    }
    void GodAttack()
    {

        
        //movement.speed = movement.speed * 0.5f;
        //isAttacking = true;
    }

    void ShootSwordGas()
    {
        GameObject swordGas = Instantiate(swordgasPrefab, firePoint.position, transform.rotation);
        Rigidbody rb = swordGas.GetComponent<Rigidbody>();
        rb.velocity = transform.right * fireballSpeed;
        Swoosh2.Play();
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
    void Magic()
    {
        MagicShow.text = MagicAmount.ToString();
        if(MagicAmount >= 100)
        {
            MagicAmount = 100;
        }
    }

}
