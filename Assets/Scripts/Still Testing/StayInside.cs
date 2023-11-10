using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour
{
    public Transform target;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4f, 4f), Mathf.Clamp(transform.position.y, -4f, 4f), transform.position.z);
    }
    void No()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4f, 4f), Mathf.Clamp(transform.position.y, -4f, 4f), transform.position.z);

        Vector3 viewPos = cam.WorldToViewportPoint(target.position);
        if (viewPos.x > 0.5F)
            print("target is on the right side!");
        else
            print("target is on the left side!");
    }
}
