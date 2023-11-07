using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyTransform : MonoBehaviour
{

    
    public Animator anim;

    float powerTime;
    public bool isTransformed;

    void Start()
    {
        
        isTransformed = false;
    }

    // Update is called once per frame
    void Update()
    {
        TurnIntoWeapon();
    }

    void TurnIntoWeapon()
    {
        if (isTransformed == false && Input.GetKeyDown(KeyCode.T))
        {
            anim.SetBool("Transform", true);
            isTransformed = true;
            

        }
        if (isTransformed)
        {
            powerTime += Time.deltaTime;
            if (powerTime >= 10)
            {
                anim.SetBool("Transform", false);
                isTransformed = false;
                
                powerTime = 0;
            }

        }

    }
}
