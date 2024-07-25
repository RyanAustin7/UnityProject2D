using UnityEngine;
using System.Collections;

public class FireRatePowerup : MonoBehaviour
{
    public float effectDuration = 5f;
    public float increasedFireRate = 0.1f;
    public GameObject powerupObject; // GameObject reference
    public float flickerInterval = 0.1f;
    public float flickerStartTime = 1f;

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
            activeCoroutine = StartCoroutine(ChangeFireRate(true));
        }
        else
        {
            StartCoroutine(ChangeFireRate(false));
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
}
