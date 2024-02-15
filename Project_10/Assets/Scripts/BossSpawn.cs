using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public static bool BossTrigger;
    public GameObject Boss;
    public Transform SpawnPoint;
    public Vector3 SpawnPos;
    public bool TriggerYet;
    void Start()
    {
        TriggerYet = false;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);

        if(!TriggerYet && BossTrigger)
        {
            Instantiate(Boss,SpawnPos,SpawnPoint.rotation);
            BossTrigger = false;
            TriggerYet = true;
        }
    }
}
