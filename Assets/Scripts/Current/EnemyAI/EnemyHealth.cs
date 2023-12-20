using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    public Animator anim;
    /*public float knockbackForce;*/
    public int maxHealth;
    public float currentHealth;
    public static event Action OnDestroyed;
    [SerializeField] private Slider SliderValue;
    [SerializeField] private Slider SliderValueShield;
    
    [SerializeField] private SimpleFlash flashEffect;
    [SerializeField] bool AmIBoss;

    [SerializeField] float shieldHealth;
    [SerializeField] bool Round2;
    [SerializeField] public static int shieldKind;
    [SerializeField] GameObject NoSword,NoMagic;
    void Awake()
    {
       


    }

    void Start()
    {
        currentHealth = maxHealth;
        shieldHealth = 0f;
        Round2 = false;
    }

   
    void Update()
    {
        SliderValue.value = currentHealth;
        if(AmIBoss)
        {
            SliderValueShield.value = shieldHealth;
        }
        
        if(AmIBoss)
        {
            SecondRound();
        }
        
        
    }

    public void TakeDamage(int damage)
    {
        flashEffect.Flash();

        if (shieldHealth > 0)
        {
            if (damage <= shieldHealth)
            {
                shieldHealth -= damage;
            }
            else
            {
                float remainingDamage = damage - shieldHealth;
                shieldHealth = 0f;
                shieldKind = 0;
                currentHealth -= remainingDamage;
            }
        }
        else
        {
            currentHealth -= damage;
        }


        //currentHealth -= damage;

        Debug.Log("Enemy Health: " + currentHealth);
        anim.SetTrigger("Hit");

        



        if (currentHealth <= 0)
        {
            anim.SetTrigger("isDead");
            Die();
            GameManager.PlayerKillCount +=1;
            if(AmIBoss)
            {
                Story.ResetStory = true;
                Story.ScriptsVersion = 2;
            }
        }
    }
    void SecondRound()
    {
        if(currentHealth <= maxHealth/2 && !Round2)
        {
            shieldKind = UnityEngine.Random.Range(1,3);
            Round2 = true;
            shieldHealth = 30;
            print("ShieldKind is" + shieldKind);
            if(shieldKind == 1)
            {
                NoMagic.SetActive(true);
            }else if(shieldKind ==2)
            {
                NoSword.SetActive(true);
            }
        }
        if(AmIBoss && shieldKind == 0)
        {
            NoSword.SetActive(false);
            NoMagic.SetActive(false);
        }
    }

    private void Die()
    {
        
        
        Debug.Log("Enemy Died");
        DisableAllColliders(transform);
        StartCoroutine(Disable());
        Destroy(this.gameObject,2f);
    }

    void DisableAllColliders(Transform parent)
    {
        Collider[] colliders = parent.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
    }

    IEnumerator Disable()
    {

        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        OnDestroyed?.Invoke(); // Invoke the event when the enemy is destroyed
    }

}
