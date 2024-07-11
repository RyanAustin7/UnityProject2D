using UnityEngine;
using System.Collections;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerupPrefab;  // Reference to the powerup prefab
    public float spawnInterval = 10f;  // Time between powerup spawns
    public float spawnRadius = 5f;  // Radius within which to spawn powerups

    private void Start()
    {
        StartCoroutine(SpawnPowerupRoutine());
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Get a random position within the defined radius
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            spawnPosition.x = Mathf.Clamp(spawnPosition.x, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);
            spawnPosition.y = Mathf.Clamp(spawnPosition.y, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y, Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y);

            // Instantiate the powerup at the calculated position
            Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
