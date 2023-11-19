using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    public Transform target;
    public Camera camera;

    void Start()
    {
        //camera = GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 viewPos = camera.WorldToViewportPoint(target.position);
        if (viewPos.x > 0.5F)
            print("target is on the right side!");
        else
            print("target is on the left side!");
    }
}
