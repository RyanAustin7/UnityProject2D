using UnityEngine;
using System.Collections;

public class SizePowerup : MonoBehaviour
{
    public float effectDuration = 5f;
    public Vector3 newSize = new Vector3(0.7f, 0.7f, 0.7f);
    public GameObject powerupObject; // GameObject reference
    public float flickerInterval = 0.1f;
    public float flickerStartTime = 1f;
    private Vector3 originalSize;
    private bool isActive = false; // Track if the powerup is active

    private Coroutine activeCoroutine;

    private void Start()
    {
        if (powerupObject != null)
        {
            powerupObject.SetActive(false); // Ensure the object is initially inactive
        }
        originalSize = transform.localScale;
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
            activeCoroutine = StartCoroutine(ChangeSize(true));
            AkSoundEngine.SetRTPCValue("Size_Powerup", 1);
        }
        else
        {
            isActive = false;
            StartCoroutine(ChangeSize(false));
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

            transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            AkSoundEngine.SetRTPCValue("Size_Powerup", 0);
            isActive = false; // Mark as inactive after resetting
        }
    }


    public IEnumerator ChangeSize(bool powerUp)
    {
        if (powerUp)
        {
            // Store original size before changing it
            originalSize = transform.localScale;

            transform.localScale = newSize;

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

            transform.localScale = originalSize;
            AkSoundEngine.SetRTPCValue("Size_Powerup", 0);
        }
        else
        {
            transform.localScale = originalSize;
            if (powerupObject != null)
            {
                powerupObject.SetActive(false); // Ensure it’s turned off when deactivating
            }
        }
    }

    private IEnumerator PlayPowerupOverSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        AkSoundEngine.PostEvent("Play_PowerupOver", gameObject);
    }

}
