using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public static AsteroidManager Instance;

    public GameObject asteroidPrefab;

    [Header("Asteroid Stuff")]
    public int initialAsteroids = 5;
    public float minScale = 0.15f; 
    public float maxScale = 0.5f;  

    [Header("Asteroid Speed")]
    public float minSpeedY = -2f; 
    public float maxSpeedY = -5f;
    public float minSpeedX = -1f; 
    public float maxSpeedX = 1f;  

    [Header("Spawn Parameters")]
    public float minSpawnX = -10f;  
    public float maxSpawnX = 10f;   
    public float spawnY = 15f;     
    public float recycleDistance = 2f;

    private GameObject[] asteroids;

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

    private void Start()
    {
        asteroids = new GameObject[initialAsteroids];
        for (int i = 0; i < initialAsteroids; i++)
        {
            asteroids[i] = Instantiate(asteroidPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            InitializeAsteroid(asteroids[i]);
        }
    }

    private void InitializeAsteroid(GameObject asteroid)
    {
        float randomScale = Random.Range(minScale, maxScale);
        asteroid.transform.localScale = new Vector3(randomScale, randomScale, 1);

        Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
        if (asteroidScript != null)
        {
            asteroidScript.InitializeMovement(minSpeedX, maxSpeedX, minSpeedY, maxSpeedY);
        }

        asteroid.SetActive(true);
    }

    private void Update()
    {
        CheckAsteroidsOffScreen();
        ReinitializeInactiveAsteroids();
    }

    private void CheckAsteroidsOffScreen()
    {
        foreach (var asteroid in asteroids)
        {
            if (asteroid.activeInHierarchy && IsOffScreen(asteroid))
            {
                RecycleAsteroid(asteroid);
            }
        }
    }

    private void ReinitializeInactiveAsteroids()
    {
        for (int i = 0; i < asteroids.Length; i++)
        {
            if (!asteroids[i].activeInHierarchy)
            {
                asteroids[i].SetActive(true);
                asteroids[i].transform.position = GetRandomSpawnPosition();
                InitializeAsteroid(asteroids[i]);
            }
        }
    }

    private void RecycleAsteroid(GameObject asteroid)
    {
        asteroid.SetActive(false);
    }

    private bool IsOffScreen(GameObject obj)
    {
        Vector3 position = obj.transform.position;
        float screenBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        return position.y < (screenBottom - recycleDistance);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        return new Vector2(Random.Range(minSpawnX, maxSpawnX), spawnY);
    }
}
