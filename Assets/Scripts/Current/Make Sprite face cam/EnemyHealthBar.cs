using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform Cam;
    public static bool FlipR;
    public bool FlipShield;

    void Start()
    {
        Cam = GameObject.Find("Camera").transform;
        FlipR = false;
    }
    void Update()
    {
        Flip();
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + Cam.forward);
    }
    public void Flip()
    {
        if(FlipR)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            FlipR = false;
            if(FlipShield)
            {
                Vector3 newScale2 = transform.localScale;
                newScale2.x *= -1;
                transform.localScale = newScale2;
                FlipShield = false;
            }
        }
    }
}
