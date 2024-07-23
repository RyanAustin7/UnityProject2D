using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public static AsteroidManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void ResetAsteroids()
    {
        // Destroy all existing asteroids
        foreach (var asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            Destroy(asteroid);
        }

        // Reset all asteroid spawners
        foreach (var spawner in FindObjectsOfType<AsteroidSpawner>())
        {
            spawner.ResetAsteroidSpawner();
        }
    }
}
