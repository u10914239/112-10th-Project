using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider SliderValue;

    public int maxHealth = 500;
    [SerializeField] int currentHealth;
    [SerializeField] private SimpleFlash flashEffect;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        SliderValue.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        flashEffect.Flash();
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);
        if (currentHealth <= 0)
        {

            Die();
        }
    }
    public void Die()
    {
        Debug.Log("I Died");
        gameObject.SetActive(false);
    }
}
