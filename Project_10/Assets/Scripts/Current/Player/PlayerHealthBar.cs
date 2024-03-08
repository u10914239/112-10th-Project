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
    [SerializeField] public int currentHealth;
    [SerializeField] private SimpleFlash flashEffect;
    public GameObject EndCover,Slider1,Slider2;
    public AudioSource Punch;

    [SerializeField] private Slider Slider2Value;
    [SerializeField] public float currentStamina;
    [SerializeField] public float MaxStamina = 100;
    [SerializeField] public bool StartRecover;

    public static bool WaitForRescue, Player1WaitForRescue, Player2WaitForRescue;
    public static bool Rescued;

    public bool Player1, Player2;
    public PlayerController playerController;
    public PlayerController_Joystick playerController_Joystick;
    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = MaxStamina;
        Player1WaitForRescue = false;
        Player2WaitForRescue = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        SliderValue.value = currentHealth;
        Slider2Value.value = currentStamina;

        if(StartRecover && currentStamina < MaxStamina)
        {
            currentStamina += Time.deltaTime*50;
        }else if(currentStamina >= MaxStamina)
        {
            StartRecover = false;
        }
        
        //Rescue();
        if (currentHealth <= 0) 
        {
            WaitForRescue = true;
            //print("Player is Dead");

            if(Player1 == true && Player2 == false)
            {
                Player1WaitForRescue = true;
            }
            if(Player2 && !Player1)
            {
                Player2WaitForRescue = true;
            }
            //Die();
            //Invoke("GameOver",2f );
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
