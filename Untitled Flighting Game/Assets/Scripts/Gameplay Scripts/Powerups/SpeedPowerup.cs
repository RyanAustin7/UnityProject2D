using UnityEngine;
using System.Collections;

public class SpeedPowerup : MonoBehaviour
{
    public float effectDuration = 5f;
    public float increasedSpeed = 45f;
    public float increasedRotationSpeed = 55f;
    public float decreaseDashCooldown = 0.6f; // Corrected spelling from 'devrease'
    public GameObject powerupObject; // GameObject reference
    public float flickerInterval = 0.1f;
    public float flickerStartTime = 1f;
    private Vector3 newSize = new Vector3(1.1f, 1.1f, 1.1f);
    private Vector3 originalSize;
    private bool isActive = false; // Track if the powerup is active

    private Coroutine activeCoroutine;

    private void Start()
    {
        if (powerupObject != null)
        {
            powerupObject.SetActive(false); // Ensure the object is initially inactive
        }
    }

    public void TogglePowerUp(bool start = true)
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }

        if (start)
        {
            isActive = true;
            activeCoroutine = StartCoroutine(ChangeSpeed(true));
            AkSoundEngine.SetRTPCValue("Speed_Powerup", 1);
        }
        else
        {
            isActive = false;
            StartCoroutine(ChangeSpeed(false));
        }
        
    }

    public void LoseLifeLosePower()
    {
        if (isActive) // Only reset if the powerup is active
        {
            if (activeCoroutine != null)
            {
                StopCoroutine(activeCoroutine);
            }

            if (powerupObject != null)
            {
                powerupObject.SetActive(false);
            }

            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            DashController dashController = GetComponent<DashController>();

            if (playerMovement != null && dashController != null)
            {
                transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                playerMovement.thrust = playerMovement.originalThrust;
                playerMovement.rotationSpeed = playerMovement.originalRotationSpeed;
                dashController.dashCooldown = 1.0f;
            }
            AkSoundEngine.SetRTPCValue("Speed_Powerup", 0);
            isActive = false; // Mark as inactive after resetting
        }
    }


    private IEnumerator ChangeSpeed(bool powerUp)
    {
        PlayerMovement playerMovement = GetComponentInParent<PlayerMovement>();
        DashController dashController = GetComponentInParent<DashController>();

        if (playerMovement != null && dashController != null) // Corrected typo here
        {
            if (powerUp)
            {
                originalSize = transform.localScale;
                transform.localScale = newSize; // Apply new size

                playerMovement.thrust += increasedSpeed;
                playerMovement.rotationSpeed += increasedRotationSpeed;
                dashController.dashCooldown -= decreaseDashCooldown; // Corrected spelling

                if (powerupObject != null)
                {
                    powerupObject.SetActive(true); // Activate the powerup object
                }

                yield return new WaitForSeconds(effectDuration - flickerStartTime);

                StartCoroutine(PlayPowerupOverSoundWithDelay(0.2f));

                float flickerEndTime = Time.time + flickerStartTime;
                while (Time.time < flickerEndTime)
                {
                    if (powerupObject != null)
                    {
                        powerupObject.SetActive(!powerupObject.activeSelf); // Flicker
                        
                    }
                    yield return new WaitForSeconds(flickerInterval);
                }

                

                if (powerupObject != null)
                {
                    powerupObject.SetActive(false); // Deactivate the powerup object
                }
                
                transform.localScale = originalSize; // Reset size

                playerMovement.thrust -= increasedSpeed;
                playerMovement.rotationSpeed -= increasedRotationSpeed;
                dashController.dashCooldown += decreaseDashCooldown; // Reset cooldown

                AkSoundEngine.SetRTPCValue("Speed_Powerup", 0);
            }
            else
            {
                playerMovement.thrust = playerMovement.originalThrust;
                playerMovement.rotationSpeed = playerMovement.originalRotationSpeed;
                dashController.dashCooldown = dashController.originalDashCooldown;
                if (powerupObject != null)
                {
                    powerupObject.SetActive(false); // Ensure it’s turned off when deactivating
                }
            }
        }
    }
    private IEnumerator PlayPowerupOverSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        AkSoundEngine.PostEvent("Play_PowerupOver", gameObject);
    }
}
