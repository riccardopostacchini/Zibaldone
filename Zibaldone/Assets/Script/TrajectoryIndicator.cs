using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryIndicator : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float maxDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawTrajectory();
    }

    void DrawTrajectory()
    {
        Vector3 startPoint = transform.position;
        Vector3 endPoint;

        RaycastHit2D hit = Physics2D.Raycast(startPoint, -transform.up, maxDistance);
        if (hit.collider != null)
        {
            endPoint = hit.point;
        }
        else
        {
            endPoint = startPoint - transform.up * maxDistance;
        }

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }
}
