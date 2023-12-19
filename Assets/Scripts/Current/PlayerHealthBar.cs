using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    public int maxHealth = 500;
    public bool isInvulernable = false;

    [SerializeField] private Slider SliderValue;
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
        if (isInvulernable)
            return;
        flashEffect.Flash();
        currentHealth -= damage;
        //Debug.Log("Player Health: " + currentHealth);
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
