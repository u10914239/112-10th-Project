using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    
    public GameObject bounds;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Camera.main.fieldOfView >= 14f)
        {
            bounds.GetComponent<BoxCollider>().enabled=true;

        }
        else
        {
            bounds.GetComponent<BoxCollider>().enabled=false;
        }

    }
}
