using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    public int maxHealth;
    public int currentHealth;

    [SerializeField] private SimpleFlash flashEffect;
    
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
        //animator.SetTrigger("Hit");
        if (currentHealth <= 0)
        {

            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy Died");
        Destroy(this.gameObject);
    }
}
