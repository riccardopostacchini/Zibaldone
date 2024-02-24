using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPowerBarScript : MonoBehaviour
{
    public CannoneScript cannoneScript;

    public float currentLaunchForce = 100f;
    private float originalPositionY;
    private float originalScaleY = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        originalPositionY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        currentLaunchForce = cannoneScript.currentLaunchForce;

        // Calcola la nuova scala
        float newScaleY = currentLaunchForce / 200;
        transform.localScale = new Vector3(0.6f, newScaleY, 1);

        // Ajust position to compensate for centered expansion
        // Assumendo che il pivot originale sia al centro e vogliamo espandere verso destra
        float adjustedPositionY = originalPositionY + (newScaleY - originalScaleY) / 2;
        transform.position = new Vector3(transform.position.x, adjustedPositionY, transform.position.z);

        float lerpValue = currentLaunchForce / 900f; // Assicurati di avere accesso a maxLaunchForce nel tuo CannoneScript
        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.green, Color.red, lerpValue);
    }
}
