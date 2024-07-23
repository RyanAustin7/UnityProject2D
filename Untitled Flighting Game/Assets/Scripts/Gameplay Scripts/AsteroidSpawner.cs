using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;  // Reference to the asteroid prefab

    [Header("Wave Settings")]
    public WaveSettings[] waveSettings; // Array of different wave settings
    public float[] waveDurations;       // Array of durations for each wave

    [Header("Spawn Timing")]
    public float minSpawnInterval = 1f; // Minimum time between spawns
    public float maxSpawnInterval = 3f; // Maximum time between spawns

    private int currentWaveIndex = 0;   // Index to keep track of current wave
    private Coroutine waveCoroutine;    // Reference to the active coroutine

    [Header("Spawn Locations")]
    public float minSpawnX = -10f;     // Minimum x position for spawning
    public float maxSpawnX = 10f;      // Maximum x position for spawning
    public float minSpawnY = -5f;      // Minimum y position for spawning
    public float maxSpawnY = 15f;      // Maximum y position for spawning

    [Header("Dynamic Values for Wave Settings")]
    public float minSpeedX = -2f;      // Minimum horizontal speed
    public float maxSpeedX = 2f;       // Maximum horizontal speed
    public float minSpeedY = -2f;      // Minimum vertical speed
    public float maxSpeedY = 2f;       // Maximum vertical speed
    public int asteroidCount = 5;      // Number of asteroids to spawn
    public float minScale = 0.15f;     // Minimum scale for asteroids
    public float maxScale = 0.5f;      // Maximum scale for asteroids

    public int currentWave;            // To display the current wave index

    private void Start()
    {
        waveCoroutine = StartCoroutine(HandleWavesWithDelay());
    }

    private IEnumerator HandleWavesWithDelay()
    {
        yield return new WaitForSeconds(10f); // Wait for 10 seconds before starting the first wave

        // Start handling waves
        yield return StartCoroutine(HandleWaves());
    }

    private IEnumerator HandleWaves()
    {
        while (currentWaveIndex < waveSettings.Length)
        {
            ApplyWaveSettings(waveSettings[currentWaveIndex]);
            currentWave = currentWaveIndex + 1;

            yield return StartCoroutine(SpawnAsteroidsForWave(waveDurations[currentWaveIndex]));

            currentWaveIndex++;
        }
    }

    private IEnumerator SpawnAsteroidsForWave(float waveDuration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < waveDuration)
        {
            SpawnAsteroids(minScale, maxScale);

            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);

            elapsedTime += spawnInterval;
        }
    }

    private void ApplyWaveSettings(WaveSettings settings)
    {
        minSpeedX = settings.minSpeedX;
        maxSpeedX = settings.maxSpeedX;
        minSpeedY = settings.minSpeedY;
        maxSpeedY = settings.maxSpeedY;
        asteroidCount = settings.asteroidCount;
        minScale = settings.minScale;
        maxScale = settings.maxScale;
    }

    public void SpawnAsteroids(float minScale, float maxScale)
    {
        for (int i = 0; i < asteroidCount; i++)
        {
            GameObject asteroid = Instantiate(asteroidPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
            if (asteroidScript != null)
            {
                asteroidScript.InitializeMovement(minSpeedX, maxSpeedX, minSpeedY, maxSpeedY, minScale, maxScale);
            }
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        return new Vector2(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY));
    }

    public void ResetAsteroidSpawner()
    {
        if (waveCoroutine != null)
        {
            StopCoroutine(waveCoroutine);
        }

        currentWaveIndex = 0;
        currentWave = 0;

        // Restart the delay coroutine
        waveCoroutine = StartCoroutine(HandleWavesWithDelay());
    }
}

[System.Serializable]
public class WaveSettings
{
    public float minSpeedX;
    public float maxSpeedX;
    public float minSpeedY;
    public float maxSpeedY;
    public int asteroidCount;
    public float minScale;
    public float maxScale;
}
