using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public LineRenderer lineRenderer;
    public float launchForce = 500f;
    public int resolution = 30;
    public float maxTime = 1.0f;

    void Update()
    {
        DrawTrajectory();
    }

    void DrawTrajectory()
    {
        List<Vector3> points = new List<Vector3>();
        Vector2 startPosition = rigidBody2D.position; // Utilizza la posizione del Rigidbody2D per coerenza
        Vector2 launchDirection = -transform.up; // Assicurati che questa sia la direzione corretta di lancio
        Vector2 startVelocity = launchDirection * (launchForce / rigidBody2D.mass); // Velocità iniziale basata sulla forza di lancio

        for (int i = 0; i < resolution; i++)
        {
            float time = (maxTime / resolution) * i;
            Vector2 position = startPosition + 
                               startVelocity * time + 
                               0.5f * Physics2D.gravity * Mathf.Pow(time, 2); // Calcolo della posizione tenendo conto della gravità
            // Conversione da Vector2 a Vector3 per il LineRenderer
            points.Add(new Vector3(position.x, position.y, 0));
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
