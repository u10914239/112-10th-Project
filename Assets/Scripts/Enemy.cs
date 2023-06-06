using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public GameObject enemy1,enemy2,enemy3;
    //public int enemy1H,enemy2H,enemy3H = 3;
    public int Health = 3;
    public bool FireCoolDown = false;
    public SpriteRenderer color;
    void Start()
    {
        color = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Health<=0)
        {
            Destroy(gameObject,0f);
            
            MainCharactor.KillCount += 1;
        }
    }
    void OnTriggerStay(Collider Player)
    {
        if(FireCoolDown == false && Player.CompareTag("Player") && Input.GetButton("Fire1"))
        {
            Debug.Log("dasdasdasdas");
            Health -= 1;
            FireCoolDown = true;
            color.color = Color.red;
            Invoke("colorwhite",0.3f);
        }
        if(!Input.GetButton("Fire1") && FireCoolDown == true)
        {
            FireCoolDown = false;
        }
    }
    public void colorwhite()
    {
        color.color = Color.white;
    }
}
