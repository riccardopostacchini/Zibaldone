using UnityEngine;

public class CannonMovementChecker : MonoBehaviour
{
    public GameObject cannone;
    private CannoneScript cannoneScript;

    private void Awake()
    {
        cannoneScript = cannone.GetComponent<CannoneScript>();
    }

    private void Update()
    {
        transform.position = new Vector3(cannone.transform.position.x, cannone.transform.position.y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerSinistro"))
        {
            cannoneScript.SetCanMoveLeft(false);
        }
        else if (collision.CompareTag("TriggerDestro"))
        {
            cannoneScript.SetCanMoveRight(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerSinistro"))
        {
            cannoneScript.SetCanMoveLeft(true);
        }
        else if (collision.CompareTag("TriggerDestro"))
        {
            cannoneScript.SetCanMoveRight(true);
        }
    }
}
