using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{protected float m_JoyX = 0.0f;  
     protected float m_JoyY = 0.0f;  
       
     // Use this for initialization  
     void Start () {  
       
     }  
       
     // Update is called once per frame  
     void Update ()   
     {          
         m_JoyX = Input.GetAxis("Horizontal");  
         m_JoyY = Input.GetAxis("Vertical");  
                 
         Debug.Log("Get Horizontal input from Joystick = " + m_JoyX.ToString());  
         Debug.Log("GetVertical input from Joystick = " + m_JoyY.ToString());  
     }  
 }  