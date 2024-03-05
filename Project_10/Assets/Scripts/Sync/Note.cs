using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class Note : MonoBehaviour
{
    // Start is called before the first frame update
    public bool CanBePress;
    public KeyCode KeytoPress;
    public KeyCode NottoPress,NottoPress2,NottoPress3;
    public static bool ConfirmDestroy;
    
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeytoPress) && CanBePress)
        {
            gameObject.SetActive(false);
            print("Hit");

            if(SyncManager.CallbyKnight)
            {
                PlayerController.powerTime += 0.6f;
            }
            if(SyncManager.CallbyMagic)
            {
                PlayerController_Joystick.powerTime += 0.6f;
            }
        }
        if(Input.GetKeyDown(NottoPress) && CanBePress || Input.GetKeyDown(NottoPress2) && CanBePress || Input.GetKeyDown(NottoPress3) && CanBePress)
        {
            gameObject.SetActive(false);
            print("Missed");
            PlayerController.powerTime -= 0.3f;
            PlayerController_Joystick.powerTime -= 0.3f;
        }
        print(PlayerController.powerTime + "///" + PlayerController_Joystick.powerTime);
        //!銷毀
        if(PlayerController.powerTime <= 0 && PlayerController_Joystick.powerTime <= 0)
        {
            Destroy(gameObject);
            ConfirmDestroy = true;
        }else if(PlayerController.powerTime > 0 || PlayerController_Joystick.powerTime > 0)
        {
            ConfirmDestroy = false;
        }
    }
    void FixedUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x - 12, this.transform.position.y, this.transform.position.z);
    }
    
    void OnTriggerEnter(Collider Other)
    {
        if(Other.gameObject.tag == "Activator")
        {
            CanBePress = true;
        }
        if(Other.gameObject.tag == "Deactivator")
        {
            gameObject.SetActive(false);
            print("Missed");
        }
    }
    void OnTriggerExit(Collider Other)
    {
        if(Other.gameObject.tag == "Activator")
        {
            CanBePress = false;
        }
    }
}
