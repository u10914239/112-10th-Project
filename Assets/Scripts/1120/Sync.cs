using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : MonoBehaviour
{
    public Transform Target;
    public Transform TargetArea;
    public bool Add;
    public bool Touching;
    public bool TouchingCoolDown;

    
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Target.localPosition.x<=-225)
        {
            Add = true;
        }else if(Target.localPosition.x>=225)
        {
            Add = false;
        }
        
        if(Touching && !TouchingCoolDown)
        {
            if(Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.P))
                {
                    //print("Fun");
                    TargetArea.localPosition = new Vector3(Random.Range(-220,220),0,0);
                    PlayerController_Joystick.powerTime = 0;
                    TouchingCoolDown = true;
                }
        }
        

    }
    
    void FixedUpdate()
    {
        if(Add)
        {
            Target.position = Target.position + new Vector3(0,10,0);
        }else if(!Add)
        {
            Target.position = Target.position + new Vector3(0,-10,0);
        }
    }

    void OnTriggerStay(Collider Touch)
    {
        if(Touch.gameObject.tag == "Target")
        {
            Touching = true;
        }
    }
    void OnTriggerExit(Collider Touch)
    {
        if(Touch.gameObject.tag == "Target")
        {
            Touching = false;
            TouchingCoolDown = false;
        }
    }
}
