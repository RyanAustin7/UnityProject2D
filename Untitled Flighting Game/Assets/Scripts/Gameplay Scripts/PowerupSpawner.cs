using UnityEngine;
using System.Collections;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerupPrefab;  // Reference to the powerup prefab
    public float minInitialDelay = 11f;  // Minimum delay before the first powerup spawns
    public float maxInitialDelay = 17f;  // Maximum delay before the first powerup spawns
    public float minSpawnInterval = 8f;  // Minimum Time between powerup spawns
    public float maxSpawnInterval = 12f;    // Maximum Time between powerup spawns
    public float spawnRadius = 4.5f;  // Radius within which to spawn powerups
    public float minPowerupLifetime = 3.5f;  // Time before powerup despawns
    public float maxPowerupLifetime = 6f;  // Time before powerup despawns

    private GameObject currentPowerup;
    private Coroutine spawnCoroutine;

    private void Start()
    {
        StartSpawning();
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
        // Random initial delay before the first powerup spawns
        float initialDelay = Random.Range(minInitialDelay, maxInitialDelay);
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            if (currentPowerup == null)
            {
                // Get a random position within the defined radius
                Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
                spawnPosition.x = Mathf.Clamp(spawnPosition.x, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);
                spawnPosition.y = Mathf.Clamp(spawnPosition.y, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y, Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y);

                // Instantiate the powerup at the calculated position
                currentPowerup = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
                currentPowerup.tag = "Powerup";  // Ensure the instantiated powerup is tagged correctly

                // Wait for the powerup to despawn
                yield return new WaitForSeconds(Random.Range(minPowerupLifetime, maxPowerupLifetime));

                // Destroy the powerup
                Destroy(currentPowerup);

                // Wait for some time before spawning the next powerup
                yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            }
            else
            {
                // Wait a frame before checking again
                yield return null;
            }
        }
    }

    public void ResetPowerupSpawner()
    {
        // Clear existing powerups
        ClearPowerups();

        // Restart the spawning routine
        StartSpawning();
    }

    public void ClearPowerups()
    {
        // Find all powerups in the scene and destroy them
        foreach (var powerup in GameObject.FindGameObjectsWithTag("Powerup"))
        {
            Destroy(powerup);
        }
    }
}
