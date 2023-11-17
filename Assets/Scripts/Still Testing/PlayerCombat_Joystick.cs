using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat_Joystick : MonoBehaviour
{
    public Animator animator;

    public GameObject arrowPrefab;
    public Transform attackPoint;
    public Vector3 halfExtents;

    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    PlayerController_Joystick movement;

    void Start()
    {
        movement = GameObject.Find("Player 2").GetComponent<PlayerController_Joystick>();

    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;

            }


        }

        
    }

    void Attack()
    {
        
        float dir = 0f;
        if (movement.facingRight==true)
        {
            dir = 1f;

        }
        if (movement.facingRight == false)
        {

            dir = -1f;
        }

        Vector2 shootingDirection = new Vector2(dir,0);
        shootingDirection.Normalize();
        GameObject arrow = Instantiate(arrowPrefab, attackPoint.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody>().velocity = shootingDirection*5.0f;
        
        Vector3 origScale = arrow.transform.localScale;
        arrow.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1, origScale.y, origScale.z);
        Destroy(arrow, 2.0f);


       

         
        

        

    }

   
    
}
