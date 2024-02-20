using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PallaScript : MonoBehaviour
{
    public GameObject ballPrefab;
    private Vector2 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Floor") || other.CompareTag("Basket"))
        {
            DestroyAndRespawn();
        }
    }

    void DestroyAndRespawn()
    {
        Destroy(gameObject);

        Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
    }
}
