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

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TogglePowerUp(bool start = true)
    {
        if (start)
        {
            StartCoroutine(ChangeSpriteAndStats(true));
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

                // Wait for the duration of the powerup effect
                float elapsed = 0f;
                while (elapsed < effectDuration)
                {
                    // Update elapsed time
                    elapsed += Time.deltaTime;

                    // Check if we are in the last `flickerStartTime` seconds of the powerup duration
                    if (elapsed >= effectDuration - flickerStartTime)
                    {
                        // Flicker the sprite every `flickerInterval` seconds
                        spriteRenderer.sprite = (spriteRenderer.sprite == powerupSprite) ? originalSprite : powerupSprite;
                        yield return new WaitForSeconds(flickerInterval);
                    }
                    else
                    {
                        // Wait for the next frame
                        yield return null;
                    }
                }

                // Ensure the final sprite is the original sprite
                spriteRenderer.sprite = originalSprite;

                // Revert the powerup effect
                TogglePowerUp(false);
            }
            else
            {
                // Revert player stats to normal
                playerMovement.thrust -= increasedThrust;
                shootingController.fireRate += increasedFireRate;  // Restore original fire rate

                // Restore the original sprite
                spriteRenderer.sprite = originalSprite;
            }
        }
    }
}
