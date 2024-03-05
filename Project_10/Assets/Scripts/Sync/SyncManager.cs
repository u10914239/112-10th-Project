using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncManager : MonoBehaviour
{
    public float spawnCoolDown;
    public Transform SpawnPoint;
    public GameObject NoteR, NoteL, NoteU, NoteD;
    public GameObject NoteR1, NoteL1, NoteU1, NoteD1;
    public GameObject NoteR2, NoteL2, NoteU2, NoteD2;
    public GameObject Notes;
    public GameObject Parent, ParentL, ParentR;
    public int RandomNote;
    public static bool CallbyKnight, CallbyMagic;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }
    void Spawn()
    {
        if(CallbyKnight && !CallbyMagic)
        {
            Parent = ParentL;
            NoteR = NoteR2;
            NoteL = NoteL2;
            NoteU = NoteU2;
            NoteD = NoteD2;

        }else if(CallbyMagic && !CallbyKnight)
        {
            Parent = ParentR;
            NoteR = NoteR1;
            NoteL = NoteL1;
            NoteU = NoteU1;
            NoteD = NoteD1;
        }else if(!CallbyKnight && !CallbyMagic)
        {
            Parent = null;
            NoteR = null;
            NoteL = null;
            NoteU = null;
            NoteD = null;
        }





        spawnCoolDown += Time.deltaTime;
        if(spawnCoolDown >=0.5f)
        {
            RandomNote = Random.Range(1,5);
            if(RandomNote == 1)
            {
                Notes = NoteU;
            }else if(RandomNote == 2)
            {
                Notes = NoteD;
            }else if(RandomNote == 3)
            {
                Notes = NoteL;
            }else if(RandomNote == 4)
            {
                Notes = NoteR;
            }
            GameObject newObject = Instantiate(Notes, SpawnPoint.position, Quaternion.identity);
            newObject.transform.SetParent(Parent.transform);
            spawnCoolDown = 0;
        }
    }
}
