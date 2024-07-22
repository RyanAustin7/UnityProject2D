using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour
{
    public float effectDuration = 5f;  // Duration of the powerup effect
    public float increasedFireRate = 0.1f;  // Amount to increase the fire rate
    public float increasedThrust = 100f;  // Amount to increase the thrust
    public Sprite powerupSprite;  // New sprite for the powerup
    public Sprite originalSprite;  // Original sprite for the player
    public float flickerInterval = 0.1f;  // Time between flicker changes
    public float flickerStartTime = 1f;  // Time before the end of the powerup when flickering starts

    private SpriteRenderer spriteRenderer;
    private Coroutine activeCoroutine;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TogglePowerUp(bool start = true)
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }

        if (start)
        {
            activeCoroutine = StartCoroutine(ChangeSpriteAndStats(true));
        }
        else
        {
            StartCoroutine(ChangeSpriteAndStats(false));
        }
    }

    private IEnumerator ChangeSpriteAndStats(bool powerUp)
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        ShootingController shootingController = GetComponent<ShootingController>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (playerMovement != null && shootingController != null && spriteRenderer != null)
        {
            if (powerUp)
            {
                // Increase player stats
                playerMovement.thrust += increasedThrust;
                shootingController.fireRate -= increasedFireRate;  // Decrease fire rate to increase it

                // Change player's sprite to indicate powerup effect
                spriteRenderer.sprite = powerupSprite;

                // Wait for the duration of the powerup
                yield return new WaitForSeconds(effectDuration - flickerStartTime);

                // Flicker the player's sprite
                float flickerEndTime = Time.time + flickerStartTime;
                while (Time.time < flickerEndTime)
                {
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                    yield return new WaitForSeconds(flickerInterval);
                }
                spriteRenderer.enabled = true;

                // Revert the player's sprite and stats to original values
                playerMovement.thrust -= increasedThrust;
                shootingController.fireRate += increasedFireRate;
                spriteRenderer.sprite = originalSprite;
            }
            else
            {
                // Reset player stats and sprite
                playerMovement.thrust = playerMovement.originalThrust;
                shootingController.fireRate = shootingController.originalFireRate;
                spriteRenderer.sprite = originalSprite;
            }
        }
    }
}
