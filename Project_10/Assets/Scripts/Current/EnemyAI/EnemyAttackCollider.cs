using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    Collider col;
    void Start()
    {
        col = GetComponentInChildren<Collider>();
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenCollider()
    {
        col.enabled = true;
    }

    public void CloseCollider()
    {
        col.enabled = false;
    }
}
