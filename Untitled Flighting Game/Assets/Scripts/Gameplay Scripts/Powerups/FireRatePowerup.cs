using UnityEngine;
using System.Collections;

public class FireRatePowerup : MonoBehaviour
{
    public float effectDuration = 5f;
    public float increasedFireRate = 0.1f;
    public GameObject powerupObject; // GameObject reference
    public float flickerInterval = 0.1f;
    public float flickerStartTime = 1f;
    private ShootingController shootingController;
    private bool isActive = false; // Track if the powerup is active

    private Coroutine activeCoroutine;

    private void Start()
    {
        if (powerupObject != null)
        {
            powerupObject.SetActive(false); // Initially inactive
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
            activeCoroutine = StartCoroutine(ChangeFireRate(true));
            AkSoundEngine.SetRTPCValue("FireRate_Powerup", 1);
        }
        else
        {
            isActive = false;
            StartCoroutine(ChangeFireRate(false));
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

            if (shootingController != null)
            {
                shootingController.fireRate = shootingController.originalFireRate; // Reset the fire rate immediately
            }

            AkSoundEngine.SetRTPCValue("FireRate_Powerup", 0);
            isActive = false; // Mark as inactive after resetting
        }
    }


    private IEnumerator ChangeFireRate(bool powerUp)
    {
        ShootingController shootingController = GetComponentInParent<ShootingController>();

        if (shootingController != null)
        {
            if (powerUp)
            {
                shootingController.fireRate -= increasedFireRate;

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

                AkSoundEngine.SetRTPCValue("FireRate_Powerup", 0);
                shootingController.fireRate += increasedFireRate;
            }
            else
            {
                shootingController.fireRate = shootingController.originalFireRate;
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
