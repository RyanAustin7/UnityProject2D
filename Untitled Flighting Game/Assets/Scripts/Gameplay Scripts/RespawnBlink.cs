using UnityEngine;
using System.Collections;

public class RespawnBlink : MonoBehaviour
{
    public float blinkDuration = 2f; 
    public float blinkSpeed = 0.2f;  

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float originalMass; // Store the original mass

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            originalMass = rb.mass; // Save the original mass
        }
    }

    public void StartBlinking(float respawnMass)
    {
        if (rb != null)
        {
            rb.mass = respawnMass; // Set the mass during blinking
        }
        StartCoroutine(BlinkSprite());
    }

    private IEnumerator BlinkSprite()
    {
        float endTime = Time.time + blinkDuration;
        while (Time.time < endTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }
        spriteRenderer.enabled = true;

        // Reset the mass after blinking is done
        if (rb != null)
        {
            rb.mass = originalMass;
        }
    }
}
