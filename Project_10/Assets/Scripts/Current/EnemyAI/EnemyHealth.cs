using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using JetBrains.Annotations;

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
    [SerializeField] bool AmIBoss, AmIBoss2;

    [SerializeField] float shieldHealth;
    [SerializeField] public bool Round2;
    [SerializeField] public static int shieldKind;
    [SerializeField] GameObject NoSword,NoMagic;
    public static bool SlimeBossDead;

    public GameObject StoryBox;
    
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
        if(AmIBoss2)
        {
            SecondRound2();
            SliderValueShield.value = shieldHealth;
        }
        if(AmIBoss)
        {
            SecondRound();
            SliderValueShield.value = shieldHealth;
        }

        SliderValue.value = currentHealth;
        
       if(currentHealth <=0 && AmIBoss2)
       {
            Die2();
           Story.ResetStory = true;
           Story.ScriptsVersion = 2;
       }
       //if(shieldKind == 1 && shieldHealth<=0 || shieldKind == 2 && shieldHealth<=0)
       //{
       // shieldKind = 0;
       //}
    }

    public void TakeDamage(int damage)
    {
        //flashEffect.Flash();


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
            shieldKind = 0;
        }


        //currentHealth -= damage;

        Debug.Log("Enemy Health: " + currentHealth);
        anim.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            
            Die();
            GameManager.PlayerKillCount++;
            
            if(AmIBoss)
            {
                Die1();
                Story.ResetStory = true;
                Story.ScriptsVersion = 2;
                StoryBox.SetActive(true);
            }
            if(AmIBoss2)
            {
                Die1();
                Story.ResetStory = true;
                Story.ScriptsVersion = 2;
            
            }
        }
        if (currentHealth <= 0 && AmIBoss2)
        {
            
            GameManager.PlayerKillCount++;
            if(AmIBoss)
            {
                Story.ResetStory = true;
                Story.ScriptsVersion = 2;
            }
        }



        
    }
    void SecondRound() //Slime Boss
    {
        if(currentHealth <= maxHealth/2 && !Round2)
        {
            shieldKind = UnityEngine.Random.Range(1,3);
            Round2 = true;
            shieldHealth = 100;
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

        if(shieldKind ==0)
        {
            //print("HAAAAAAAAAAAAAAAAAA");
        }
    }

    public GameObject BonePrefeb;
    void SecondRound2() //Bone Boss
    {

        if(currentHealth <= maxHealth/2 && !Round2)
        {
            //for(int i = 0; i < 3; i++)
            //{
            //    Instantiate(BonePrefeb, new Vector3(transform.position.x + UnityEngine.Random.Range(-5,5),transform.position.y,transform.position.z + UnityEngine.Random.Range(-5,5)), Quaternion.identity);
            //}
            shieldKind = UnityEngine.Random.Range(1,3);
            Round2 = true;
            shieldHealth = 100;
            print("ShieldKind is" + shieldKind);
            if(shieldKind == 1)
            {
                NoMagic.SetActive(true);
            }else if(shieldKind ==2)
            {
                NoSword.SetActive(true);
            }

            
        }
        if(AmIBoss2 && shieldKind == 0)
        {
            NoSword.SetActive(false);
            NoMagic.SetActive(false);
        }
    }

    public void Die()
    {
        
        anim.SetTrigger("isDead");
        Debug.Log("Enemy Died");
        //DisableAllColliders(transform);
        //StartCoroutine(Disable());
        Destroy(this.gameObject,2f);
    }
    public void Die1()
    {
        SlimeBossDead = true;
        anim.SetTrigger("isDead");
        Debug.Log("Enemy Died");
        DisableAllColliders(transform);
        //StartCoroutine(Disable());
        Destroy(this.gameObject,2f);
    }
    public void Die2()
    {
        
        DisableAllColliders(transform);
        //StartCoroutine(Disable());
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

    /*IEnumerator Disable()
    {

        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }*/

    void OnDestroy()
    {
        OnDestroyed?.Invoke(); // Invoke the event when the enemy is destroyed
    }

}
