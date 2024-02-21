using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    public Transform shootPoint;
    public Rigidbody2D ballPrefab;
    public LineRenderer lineRenderer;
    public float launchForce = 500f; // Forza di lancio che applicherai
    public int resolution = 30; // Numero di punti lungo la traiettoria
    public float maxDistance = 5.0f; // Distanza massima della traiettoria
    public LayerMask collisionLayer; // Layer su cui controllare le collisioni

    void Update()
    {
        shootPoint = GameObject.Find("ShootPoint").transform;

        DrawTrajectory();
    }

    void DrawTrajectory()
    {
        Transform cannon = GameObject.Find("Cannone").transform;

        List<Vector3> points = new List<Vector3>();
        Vector2 startPosition = shootPoint.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 launchDirection;

        if (mousePosition.y > 4.2)
        {
            launchDirection = (new Vector3((mousePosition.x > -1.5f ? 2f : -5f), 4.2f, 0) - cannon.position).normalized;
        }
        else launchDirection = (mousePosition - cannon.position).normalized;
        Vector2 startVelocity = launchDirection * (launchForce / ballPrefab.mass);

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
