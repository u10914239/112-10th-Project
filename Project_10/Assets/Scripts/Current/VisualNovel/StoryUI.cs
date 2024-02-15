using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryUI : MonoBehaviour
{
    public GameObject VisualNovel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider Sign)
    {
        if(Input.GetKeyDown(KeyCode.F) &&Sign.gameObject.tag == "Sign")
        {
            VisualNovel.SetActive(true);
        }
    }
}
