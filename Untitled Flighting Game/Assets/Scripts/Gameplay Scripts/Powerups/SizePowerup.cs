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
            activeCoroutine = StartCoroutine(ChangeSize(true));
        }
        else
        {
            StartCoroutine(ChangeSize(false));
        }
    }

    private IEnumerator ChangeSize(bool powerUp)
    {
        if (powerUp)
        {
            originalSize = transform.localScale;

            transform.localScale = newSize;

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

            transform.localScale = originalSize;
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
}
