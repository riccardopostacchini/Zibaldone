using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannoneScript : MonoBehaviour {

    public Rigidbody2D ballRigidbody;
    public float launchForce = 500f;

    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCannonTowardsMouse();

        if (Input.GetMouseButtonDown(0) && ballRigidbody.transform.parent == transform)
        {
            ballRigidbody.gravityScale = 1;
            ballRigidbody.transform.parent = null;

            ApplyLaunchForce();
        }
    }

    void RotateCannonTowardsMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y
            );

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
    }

    void ApplyLaunchForce()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 launchDirection = (mousePosition - transform.position).normalized;

        ballRigidbody.AddForce(launchDirection * launchForce);
    }
}
