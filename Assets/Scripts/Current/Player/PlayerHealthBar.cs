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
}
