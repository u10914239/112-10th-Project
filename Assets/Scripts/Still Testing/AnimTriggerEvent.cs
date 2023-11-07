using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTriggerEvent : MonoBehaviour
{
    OnlyTransform onlyTransform;

    void Awake()
    {
        onlyTransform = GameObject.Find("Player 2").GetComponent<OnlyTransform>();

    }

    public void TriggerEvent()
    {

        onlyTransform.isTransformed = true;
    }
}
