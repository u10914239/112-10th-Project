using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Vector2 Move;
    public float Speed;
    void Start()
    {
        Speed = -.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Move.x = Input.GetAxisRaw("Horizontal");
        Move.y = Input.GetAxisRaw("Vertical");
        Move.Normalize();

        if(Move.x != 0 || Move.y != 0)
        {
            this.transform.position = this.transform.position + new Vector3(Move.x*Speed,0,-Move.y*Speed);
        }

        Debug.Log("X = " + Move.x + "Y = " +Move.y);
    }
}
