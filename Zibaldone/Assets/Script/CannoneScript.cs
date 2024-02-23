using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CannoneScript : MonoBehaviour {

    public GameObject ballPrefab;
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public GameObject currentBall;
    public GameObject currentArrow;
    public float ballScale = 0.25f;

    // VARIABILI PER IL MOVIMENTO DEL CANNONE
    public float speed = 5.0f; // Velocità di movimento del cannone
    private Rigidbody2D rb;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    // VARIABILI PER IL SET DELLA FORZA DI LANCIO DELLA PALLA
    public float minLaunchForce = 100f;
    public float maxLaunchForce = 1000f;
    public float changeSpeed = 500f;
    public float currentLaunchForce;
    private bool isIncreasing = true; // Direzione dell'incremento della forza
    private bool isSettingForce = false; // Sta impostando la forza
    public TMP_Text launchForceText;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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

        float input = Input.GetAxis("Horizontal");

        if ((input < 0 && canMoveLeft) || (input > 0 && canMoveRight))
        {
            Move(input);
        }
        else if (!canMoveLeft || !canMoveRight)
        {
            // Ferma il cannone se non può muoversi in quella direzione
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetMouseButtonDown(0) && currentBall != null)
        {
            isSettingForce = true;
            currentLaunchForce = minLaunchForce;
        }

        if (Input.GetMouseButton(0) && isSettingForce && currentBall != null)
        {
            // Oscilla la forza tra minimo e massimo
            if (isIncreasing)
            {
                currentLaunchForce += changeSpeed * Time.deltaTime;
                if (currentLaunchForce >= maxLaunchForce)
                {
                    isIncreasing = false;
                }
            }
            else
            {
                currentLaunchForce -= changeSpeed * Time.deltaTime;
                if (currentLaunchForce <= minLaunchForce)
                {
                    isIncreasing = true;
                }
            }
        }

        if (launchForceText != null)
        {
            launchForceText.text = currentLaunchForce.ToString("F2");
        }

        if (Input.GetMouseButtonUp(0) && isSettingForce && currentBall != null)
        {
            ApplyLaunchForce();
            isSettingForce = false;
        }
    }

    void RotateCannonTowardsMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
                (mousePosition.y > 4.2f ? (mousePosition.x > transform.position.x ? transform.position.x + 3f : transform.position.x - 3f) : mousePosition.x) - transform.position.x,
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
            launchDirection = (new Vector3((mousePosition.x > transform.position.x ? transform.position.x + 3f : transform.position.x - 3f), 4.2f, 0) - transform.position).normalized;
        }
        else launchDirection = (mousePosition - transform.position).normalized;
        ballRigidbody.AddForce(launchDirection * currentLaunchForce);
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

    void Move(float direction)
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    public void SetCanMoveLeft(bool canMove)
    {
        canMoveLeft = canMove;
    }

    public void SetCanMoveRight(bool canMove)
    {
        canMoveRight = canMove;
    }
}

