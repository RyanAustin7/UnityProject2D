using UnityEngine;
using System.Collections;

public class RespawnBlink : MonoBehaviour
{
    [Header("Blink Settings")]
    public float blinkDuration = 2f; 
    public float blinkSpeed = 0.2f;  

    [Header("Components")]
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private TrailRenderer trailRenderer; // Reference to the Trail Renderer

    private float originalMass; // Store the original mass

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponentInChildren<TrailRenderer>(); // Get the Trail Renderer component from the child object

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

        // Disable the Trail Renderer at the start of blinking
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
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

        // Re-enable the Trail Renderer after blinking is done
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
    }
}
