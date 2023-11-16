using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private SimpleFlash flashEffect;
    public Animator animator;
    public int maxHealth;
    int currentHealth;
    

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        flashEffect.Flash();
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        if (currentHealth <= 0)
        {

            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy Died");

    }
}
