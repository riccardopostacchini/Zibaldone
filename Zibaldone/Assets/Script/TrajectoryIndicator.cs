using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public LineRenderer lineRenderer;
    public float launchForce = 500f; // Forza di lancio che applicherai
    public int resolution = 30; // Numero di punti lungo la traiettoria
    public float maxDistance = 5.0f; // Distanza massima della traiettoria
    public LayerMask collisionLayer; // Layer su cui controllare le collisioni

    void Update()
    {
        DrawTrajectory();
    }

    void DrawTrajectory()
    {
        List<Vector3> points = new List<Vector3>();
        Vector2 startPosition = rigidBody2D.position;
        Vector2 launchDirection = -transform.up;
        Vector2 startVelocity = launchDirection * (launchForce / rigidBody2D.mass);

        float timeDelta = maxDistance / resolution;

        for (int i = 0; i < resolution; i++)
        {
            float time = timeDelta * i;
            Vector2 position = startPosition + startVelocity * time + 0.5f * Physics2D.gravity * Mathf.Pow(time, 2);
            points.Add(new Vector3(position.x, position.y, 0));

            // Esegui un raycast per controllare le collisioni
            RaycastHit2D hit = Physics2D.Raycast(startPosition, position - startPosition, Vector2.Distance(startPosition, position), collisionLayer);
            if (hit.collider != null)
            {
                // Se c'è una collisione, interrompi il ciclo e usa il punto di collisione come ultimo punto della traiettoria
                points.Add(hit.point);
                break;
            }
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
