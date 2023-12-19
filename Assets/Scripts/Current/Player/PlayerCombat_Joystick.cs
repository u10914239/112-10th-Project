using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat_Joystick : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform firePoint;

    public float maxChargeTime = 2f;
    public float maxSpeed = 10f;
    private float currentChargeTime = 0f;

    public bool isAttacking;

    void Update()
    {
        if (Input.GetKey(KeyCode.Joystick1Button5)) // Assuming left mouse button for charging
        {
            currentChargeTime += Time.deltaTime;
            // Add visual feedback for charging (e.g., UI bar or particle effect)
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button5) && currentChargeTime >= 1f) // Release to fire
        {
            StartCoroutine(Attack());
        }
    }


    IEnumerator Attack()
    {

        isAttacking = true;



        GameObject newArrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = newArrow.GetComponent<Rigidbody>();

        // Adjust arrow's initial speed along the global X-axis based on charge level
        rb.velocity = Vector3.right * maxSpeed;

        currentChargeTime = 0;

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;

    }
}
