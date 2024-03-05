using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncRV2 : MonoBehaviour
{
    public PlayerController_Joystick playerController;
    public Slider SliderValue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowValue();
    }

    void ShowValue()
    {
        SliderValue.value = PlayerController_Joystick.powerTime;
    }
}
