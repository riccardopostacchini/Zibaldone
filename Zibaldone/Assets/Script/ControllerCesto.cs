using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCesto : MonoBehaviour
{
    public float basketSpeed = 5f; // Velocità di movimento del cesto
    

    private Rigidbody2D rb;
    private float direction = 1f; // Inizialmente va verso destra

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Muovi il cesto a sinistra e a destra
        float movement = direction * basketSpeed * Time.deltaTime;
        rb.MovePosition(new Vector2(transform.position.x + movement, transform.position.y));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Cambia direzione quando il cesto entra in contatto con un trigger
        if (other.CompareTag("CambioDirezione"))
        {
            direction *= -1f;
        }
        
    }
}
