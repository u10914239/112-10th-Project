using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cmV;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (Camera.main.fieldOfView >= 30f)
        {
            cmV.gameObject.SetActive(false);

        }
        else if (Camera.main.fieldOfView <= 30f)
        {
            cmV.gameObject.SetActive(true);
        }
    }
}
