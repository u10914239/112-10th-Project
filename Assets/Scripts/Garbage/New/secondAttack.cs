using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondAttack : MonoBehaviour
{
    // Start is called before the first frame update
    

    public Animator anim;
    public AudioSource Punch;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            
            Attack();
            secondCharactor.magicNumber +=1;
            
            
        }
        
    }

    void Attack()
    {
        
        anim.SetTrigger("Attack");
        Punch.Play();

    }

    


}
