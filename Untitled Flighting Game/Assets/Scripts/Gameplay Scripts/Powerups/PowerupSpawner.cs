using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupSpawner : MonoBehaviour
{
    public List<GameObject> powerupPrefabs;
    public float minInitialDelay = 11f;
    public float maxInitialDelay = 17f;
    public float minSpawnInterval = 8f;
    public float maxSpawnInterval = 12f;
    public float spawnRadius = 4.5f;
    public float minPowerupLifetime = 3.5f;
    public float maxPowerupLifetime = 6f;

    private GameObject currentPowerup;
    private Coroutine spawnCoroutine;
    private List<GameObject> shuffledPowerups;
    private int currentIndex = 0;

    private void Start()
    {
        shuffledPowerups = new List<GameObject>(powerupPrefabs);
        ShufflePowerups();
        StartSpawning();
    }

    private void ShufflePowerups()
    {
        for (int i = 0; i < shuffledPowerups.Count; i++)
        {
            GameObject temp = shuffledPowerups[i];
            int randomIndex = Random.Range(i, shuffledPowerups.Count);
            shuffledPowerups[i] = shuffledPowerups[randomIndex];
            shuffledPowerups[randomIndex] = temp;
        }
    }

    private void StartSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnPowerupRoutine());
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        float initialDelay = Random.Range(minInitialDelay, maxInitialDelay);
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            if (currentPowerup == null)
            {
                Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
                spawnPosition.x = Mathf.Clamp(spawnPosition.x, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);
                spawnPosition.y = Mathf.Clamp(spawnPosition.y, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y, Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y);

                currentPowerup = Instantiate(shuffledPowerups[currentIndex], spawnPosition, Quaternion.identity);
                currentPowerup.SetActive(true); 
                float lifetime = Random.Range(minPowerupLifetime, maxPowerupLifetime);
                Destroy(currentPowerup, lifetime);

                currentIndex = (currentIndex + 1) % shuffledPowerups.Count;
                if (currentIndex == 0)
                {
                    ShufflePowerups();
                }
            }

            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void ResetSpawner()
    {
        StopAllCoroutines();

        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (GameObject powerup in powerups)
        {
            Destroy(powerup);
        }

        StartSpawning();
    }
}
