using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannoneScript : MonoBehaviour {

    public GameObject ballPrefab;
    public GameObject arrowPrefab;
    public float launchForce = 500f;
    public Transform shootPoint;
    public GameObject currentBall;
    public GameObject currentArrow;
    public float ballScale = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBall();
        SpawnArrow();
    }

    // Update is called once per frame
    void Update()
    {
        RotateCannonTowardsMouse();

        if (Input.GetMouseButtonDown(0) && currentBall != null)
        {
            ApplyLaunchForce();

            Destroy(currentArrow);
        }
    }

    void RotateCannonTowardsMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
                (mousePosition.y > 4.2f ? (mousePosition.x > -1.5f ? 2f : -5f) : mousePosition.x) - transform.position.x,
                (mousePosition.y > 4.2f ? 4.2f : mousePosition.y) - transform.position.y
            ).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
    }

    void ApplyLaunchForce()
    {
        currentBall.transform.SetParent(null); // Scollega la palla dal cannone
        Rigidbody2D ballRigidbody = currentBall.GetComponent<Rigidbody2D>();
        ballRigidbody.isKinematic = false; // La palla ora risponderà alla gravità
        ballRigidbody.gravityScale = 1; // Assicurati che la gravità sia impostata correttamente
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 launchDirection;

        if (mousePosition.y > 4.2)
        {
            launchDirection = (new Vector3((mousePosition.x + 1.5f) > 0 ? 2f : -5f, 4.2f, 0) - transform.position).normalized;
        }
        else launchDirection = (mousePosition - transform.position).normalized;
        ballRigidbody.AddForce(launchDirection * launchForce);
        currentBall = null; // Resetta il riferimento corrente della palla
    }

    public void SpawnBall()
    {
        if (currentBall != null) Destroy(currentBall); // Distruggi eventuali palle precedenti
        currentBall = Instantiate(ballPrefab, shootPoint.position, Quaternion.identity);
        currentBall.transform.SetParent(shootPoint); // Collega la palla al punto di lancio
        currentBall.GetComponent<Rigidbody2D>().isKinematic = true; // La palla non sarà soggetta alla gravità fino al lancio
        currentBall.transform.localScale = new Vector3(ballScale, ballScale, ballScale);
    }

    public void SpawnArrow()
    {
        if (currentArrow != null) Destroy(currentArrow); // Distruggi eventuali frecce precedenti
        currentArrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        currentArrow.transform.SetParent(shootPoint); // Collega la freccia al punto di lancio
    }
}
