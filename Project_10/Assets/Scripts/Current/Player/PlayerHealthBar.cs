using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthBar : MonoBehaviour
{

    public int maxHealth = 500;
    public bool isInvulernable = false;

    [SerializeField] private Slider SliderValue;
    [SerializeField] int currentHealth;
    [SerializeField] private SimpleFlash flashEffect;
    public GameObject EndCover,Slider1,Slider2;
    public AudioSource Punch;

    [SerializeField] private Slider Slider2Value;
    [SerializeField] public float currentStamina;
    [SerializeField] public float MaxStamina = 100;
    [SerializeField] public bool StartRecover;
    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        SliderValue.value = currentHealth;
        Slider2Value.value = currentStamina;

        if(StartRecover && currentStamina < MaxStamina)
        {
            currentStamina += Time.deltaTime*20;
        }else if(currentStamina >= MaxStamina)
        {
            StartRecover = false;
        }
        
        
    }

    public void TakeDamage(int damage)
    {
        Punch.Play();
        if (isInvulernable)
            return;
        flashEffect.Flash();
        currentHealth -= damage;
        //Debug.Log("Player Health: " + currentHealth);
        if (currentHealth <= 0)
        {

            Die();
            Invoke("GameOver",2f );
        }
    }
    public void Stamina()
    {

    }

    public void Die()
    {
        Debug.Log("I Died");
        gameObject.SetActive(false);
    }
    public void GameOver()
    {
        EndCover.SetActive(true);
        Slider1.SetActive(false);
        Slider2.SetActive(false);

        //SceneManager.LoadScene("EndCover");
    }
    public void StaminaRecover()
    {
            
            StartRecover = true;
    }

}
