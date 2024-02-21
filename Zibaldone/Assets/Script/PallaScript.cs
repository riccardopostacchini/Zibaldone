using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PallaScript : MonoBehaviour
{
    public GameObject ballPrefab; // Il prefab della palla
    public GameObject arrowPrefab; // Il prefab della freccia
    public Transform cannon; // Il Transform del cannone
    
    public static Vector2 spawnPosition; // La posizione in cui la palla deve essere creata

    private void Start()
    {
        cannon = GameObject.Find("Cannone").transform;
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
        // Distruggi la palla attuale
        Destroy(gameObject);

        CannoneScript cannoneScript = cannon.GetComponent<CannoneScript>();
        cannoneScript.SpawnBall();
        cannoneScript.SpawnArrow();
    }
}