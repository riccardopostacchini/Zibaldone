using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCesto : MonoBehaviour
{
    public float maxSpeed = 3.0f; // Velocità massima di movimento
    public Transform leftLimit; // Punto di riferimento per il limite sinistro
    public Transform rightLimit; // Punto di riferimento per il limite destro
    private float targetSpeed = 0f; // Velocità target verso cui interpolare
    private int direction = -1; // Direzione iniziale del movimento: -1 sinistra, 1 destra

    void Update()
    {
        // Calcola la distanza dal limite più vicino e usa questa per determinare la targetSpeed
        float distanceToLimit = direction == -1 ? transform.position.x - leftLimit.position.x : rightLimit.position.x - 0.5f - transform.position.x;
        // Mappa la distanza a un valore di velocità, con 0 quando il cesto è molto vicino al limite
        targetSpeed = Mathf.Lerp(0, maxSpeed, distanceToLimit / maxSpeed);

        // Aggiorna la velocità corrente del cesto verso la targetSpeed
        float currentSpeed = Mathf.MoveTowards(GetComponent<Rigidbody2D>().velocity.x, targetSpeed * direction, Time.deltaTime * maxSpeed);
        GetComponent<Rigidbody2D>().velocity = new Vector2(currentSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CambioDirezione"))
        {
            // Inverti la direzione del movimento
            direction *= -1;
        }
    }
}
