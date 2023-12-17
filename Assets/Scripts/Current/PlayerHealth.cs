using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//!
//?
//*
////
//todo
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;
    public GameObject HealthBar1,HealthBar2,HealthBar3,HealthBar4,HealthBar5;
    public float Health1,Health2,Health3,Health4,Health5;
    [SerializeField] private SimpleFlash flashEffect;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Health1 = maxHealth*1f;
        Health2 = maxHealth*0.8f;
        Health3 = maxHealth*0.6f;
        Health4 = maxHealth*0.4f;
        Health5 = maxHealth*0.2f;
        HealthBar();

        
    }
    public void TakeDamage(int damage)
    {
        flashEffect.Flash();
        currentHealth -= damage;
        //Debug.Log("Player Health: " + currentHealth);
        if (currentHealth <= 0)
        {

            Die();
        }
    }
    public void HealthBar()
    {
        if(currentHealth>=Health1)
        {
            HealthBar1.SetActive(true);
            HealthBar2.SetActive(true);
            HealthBar3.SetActive(true);
            HealthBar4.SetActive(true);
            HealthBar5.SetActive(true);
        }else if(currentHealth <= Health2)
        {
            HealthBar1.SetActive(true);
            HealthBar2.SetActive(true);
            HealthBar3.SetActive(true);
            HealthBar4.SetActive(true);
            HealthBar5.SetActive(false);
        }else if(currentHealth <= Health3)
        {
            HealthBar1.SetActive(true);
            HealthBar2.SetActive(true);
            HealthBar3.SetActive(true);
            HealthBar4.SetActive(false);
            HealthBar5.SetActive(false);
        }else if(currentHealth <= Health4)
        {
            HealthBar1.SetActive(true);
            HealthBar2.SetActive(true);
            HealthBar3.SetActive(false);
            HealthBar4.SetActive(false);
            HealthBar5.SetActive(false);
        }else if(currentHealth <= Health5)
        {
            HealthBar1.SetActive(true);
            HealthBar2.SetActive(false);
            HealthBar3.SetActive(false);
            HealthBar4.SetActive(false);
            HealthBar5.SetActive(false);
        }
        else if(currentHealth <= 0)
        {
            HealthBar1.SetActive(false);
            HealthBar2.SetActive(false);
            HealthBar3.SetActive(false);
            HealthBar4.SetActive(false);
            HealthBar5.SetActive(false);
        }
    }
    public void Die()
    {
        Debug.Log("I Died");
        gameObject.SetActive(false);
    }
}
