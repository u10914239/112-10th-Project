using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public Animator anim;
    /*public float knockbackForce;*/
    public int maxHealth;
    public int currentHealth;
    public static event Action OnDestroyed;
    
    [SerializeField] private SimpleFlash flashEffect;
    [SerializeField] bool AmIBoss;
    void Awake()
    {
       


    }

    void Start()
    {
        currentHealth = maxHealth;
        
    }

   
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        flashEffect.Flash();
        currentHealth -= damage;

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
