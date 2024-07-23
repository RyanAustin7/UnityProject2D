using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour
{
    public float effectDuration = 5f;  // Duration of the powerup effect
    public float increasedFireRate = 0.1f;  // Amount to increase the fire rate
    public float increasedThrust = 100f;  // Amount to increase the thrust
    public GameObject powerupSpriteObject;  // Child object indicating the powerup
    public float flickerInterval = 0.1f;  // Time between flicker changes
    public float flickerStartTime = 1f;  // Time before the end of the powerup when flickering starts

    private SpriteRenderer spriteRenderer;
    private Coroutine activeCoroutine;

    private void Start()
    {
        // Ensure the powerupSpriteObject is inactive at the start
        if (powerupSpriteObject != null)
        {
            powerupSpriteObject.SetActive(false);
        }
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

        if (playerMovement != null && shootingController != null)
        {
            if (powerUp)
            {
                // Increase player stats
                playerMovement.thrust += increasedThrust;
                shootingController.fireRate -= increasedFireRate;  // Decrease fire rate to increase it

                // Activate powerup indicator
                if (powerupSpriteObject != null)
                {
                    powerupSpriteObject.SetActive(true);
                }

                // Wait for the duration of the powerup minus flicker start time
                yield return new WaitForSeconds(effectDuration - flickerStartTime);

                // Flicker the powerup sprite
                float flickerEndTime = Time.time + flickerStartTime;
                while (Time.time < flickerEndTime)
                {
                    if (powerupSpriteObject != null)
                    {
                        powerupSpriteObject.SetActive(!powerupSpriteObject.activeSelf);
                    }
                    yield return new WaitForSeconds(flickerInterval);
                }
                
                // Ensure the sprite is turned off at the end
                if (powerupSpriteObject != null)
                {
                    powerupSpriteObject.SetActive(false);
                }

                // Revert player stats to original values
                playerMovement.thrust -= increasedThrust;
                shootingController.fireRate += increasedFireRate;
            }
            else
            {
                // Reset player stats and deactivate powerup indicator
                playerMovement.thrust = playerMovement.originalThrust;
                shootingController.fireRate = shootingController.originalFireRate;
                if (powerupSpriteObject != null)
                {
                    powerupSpriteObject.SetActive(false);
                }
            }
        }
    }
}
