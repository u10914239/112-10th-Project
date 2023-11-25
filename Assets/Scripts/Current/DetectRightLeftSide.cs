using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRightLeftSide : MonoBehaviour
{
    public bool isOnRight;
    public bool isOnLeft;
    public Transform player; // Reference to your player object
    public LayerMask enemyLayer; // Layer mask for enemies
    public float rightAngleThreshold = 45f; // Angle threshold for detection on the right side
    public float leftAngleThreshold = 45f; // Angle threshold for detection on the left side
    public float detectionRange = 10f; // Range of detection

    void OnDrawGizmosSelected()
    {
        if (player == null)
            return;

        Gizmos.color = Color.green;

        Vector3 startRight = Quaternion.AngleAxis(-rightAngleThreshold, Vector3.up) * transform.right;
        Vector3 endRight = Quaternion.AngleAxis(rightAngleThreshold, Vector3.up) * transform.right;

        DrawDetectionRays(startRight, endRight);

        Gizmos.color = Color.red;

        Vector3 startLeft = Quaternion.AngleAxis(-leftAngleThreshold, Vector3.up) * -transform.right;
        Vector3 endLeft = Quaternion.AngleAxis(leftAngleThreshold, Vector3.up) * -transform.right;

        DrawDetectionRays(startLeft, endLeft);
    }

    void DrawDetectionRays(Vector3 startDir, Vector3 endDir)
    {
        Gizmos.DrawRay(transform.position, startDir * detectionRange);
        Gizmos.DrawRay(transform.position, endDir * detectionRange);
    }

    void Update()
    {
        if (player == null)
            return;

        DetectEnemiesInRange();
    }

    void DetectEnemiesInRange()
    {
        Collider[] hitCollidersRight = Physics.OverlapSphere(transform.position, detectionRange, enemyLayer);
        Collider[] hitCollidersLeft = Physics.OverlapSphere(transform.position, detectionRange, enemyLayer);

        foreach (var collider in hitCollidersRight)
        {
            Vector3 direction = collider.transform.position - transform.position;
            float angle = Vector3.Angle(transform.right, direction);

            if (angle <= rightAngleThreshold)
            {
               isOnRight=true;
            }
            else
            {
                isOnRight=false;
            }
        }

        foreach (var collider in hitCollidersLeft)
        {
            Vector3 direction = collider.transform.position - transform.position;
            float angle = Vector3.Angle(-transform.right, direction);

            if (angle <= leftAngleThreshold)
            {
                isOnLeft=true;
            }
            else
            {
                isOnLeft=false;
            }
        }
    }

    
}
