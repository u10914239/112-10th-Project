using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public float radius = 5f; // Radius of the pie
    public float startAngle = 45f; // Starting angle of the pie in degrees
    public float endAngle = 135f; // Ending angle of the pie in degrees
    public int segments = 30; // Number of segments to create
    public float height = 2f; // Height of the pie collider along the y-axis
    public Color gizmoColor = Color.green; // Color for Gizmo visualization

    void OnDrawGizmosSelected()
    {
        DrawPieCollider();
    }

    void DrawPieCollider()
    {
        float angleStep = (endAngle - startAngle) / segments * Mathf.Deg2Rad;

        Vector3 previousPoint = Vector3.zero;

        Gizmos.color = gizmoColor;

        for (int i = 0; i <= segments; i++)
        {
            float angle = startAngle * Mathf.Deg2Rad + angleStep * i;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            Vector3 currentPoint = new Vector3(x, 0, z);
            Vector3 nextPoint = new Vector3(x, height, z);

            if (i > 0)
            {
                Gizmos.DrawLine(previousPoint, currentPoint);
                Gizmos.DrawLine(currentPoint, nextPoint);
                Gizmos.DrawLine(nextPoint, previousPoint);
            }

            previousPoint = currentPoint;
        }
    }
}
