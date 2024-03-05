using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncLV2 : MonoBehaviour
{
    public PlayerController playerController;
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
        SliderValue.value = PlayerController.powerTime;
    }

}
