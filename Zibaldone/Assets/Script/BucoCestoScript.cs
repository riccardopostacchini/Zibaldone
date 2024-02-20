using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucoCestoScript : MonoBehaviour
{
    public GameObject Palla;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Palla"))
        {
            Destroy(Palla);
            Debug.Log("Palla distrutta");
        }
    }
}
