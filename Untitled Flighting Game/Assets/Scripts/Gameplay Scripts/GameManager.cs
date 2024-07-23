using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    [Header("Player Lives")]
    public PlayerLife playerLife1;
    public PlayerLife playerLife2;

    [Header("Player Wins! Panels")]
    public GameObject player1WinsPanel;
    public GameObject player2WinsPanel;

    [Header("Player 1 Wins Buttons")]
    public Button player1PlayAgainButton;
    public Button player1MainMenuButton;

    [Header("Player 2 Wins Buttons")]
    public Button player2PlayAgainButton;
    public Button player2MainMenuButton;

    [Header("Player Transforms")]
    public Transform player1;
    public Transform player2;

    [Header("Asteroid Manager")]
    public AsteroidManager asteroidManager;

    [Header("Asteroid Spawner")]
    public AsteroidSpawner[] asteroidSpawners; // Reference to the asteroid spawners

    [Header("Powerup Spawner")]
    public PowerupSpawner powerupSpawner; // Reference to the powerup spawner

    [Header("Game Timer")]
    public MyTimerScript MyTimerScript; // Reference to the game timer

    private Vector3 player1StartPos;
    private Vector3 player2StartPos;

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
        player1PlayAgainButton.onClick.AddListener(PlayAgain);
        player1MainMenuButton.onClick.AddListener(GoToMainMenu);
        player2PlayAgainButton.onClick.AddListener(PlayAgain);
        player2MainMenuButton.onClick.AddListener(GoToMainMenu);

        player1WinsPanel.SetActive(false);
        player2WinsPanel.SetActive(false);

        player1StartPos = player1.position;
        player2StartPos = player2.position;
    }

    private void Update()
    {
        if (playerLife1.lives <= 0 || playerLife2.lives <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        if (playerLife1.lives <= 0)
        {
            ShowGameOver(player2WinsPanel);
        }
        else if (playerLife2.lives <= 0)
        {
            ShowGameOver(player1WinsPanel);
        }

        RemoveAllPowerups();
        RemoveAllAsteroids(); // Call the method to remove all asteroids
    }

    private void ShowGameOver(GameObject winnerPanel)
    {
        Time.timeScale = 0f;
        player1WinsPanel.SetActive(winnerPanel == player1WinsPanel);
        player2WinsPanel.SetActive(winnerPanel == player2WinsPanel);
    }

    private void PlayAgain()
    {
        player1WinsPanel.SetActive(false);
        player2WinsPanel.SetActive(false);
        Time.timeScale = 1f;
        ResetGame();
    }

    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); // Ensure this matches your menu scene name
    }

    private void ResetGame()
    {
        playerLife1.ResetLives();
        playerLife2.ResetLives();
        player1.position = player1StartPos;
        player2.position = player2StartPos;
        player1.rotation = Quaternion.Euler(0f, 0f, -90f);
        player2.rotation = Quaternion.Euler(0f, 0f, 90f);

        ResetPlayerRigidbody(player1);
        ResetPlayerRigidbody(player2);

        RemoveAllAsteroids(); // Ensure all asteroids are removed before resetting spawners

        asteroidManager.ResetAsteroids();
        powerupSpawner.ResetPowerupSpawner();

        foreach (var spawner in asteroidSpawners)
        {
            spawner.ResetAsteroidSpawner();
        }

        MyTimerScript.ResetTimer();

        DeactivatePlayerPowerups(player1);
        DeactivatePlayerPowerups(player2);
    }

    private void ResetPlayerRigidbody(Transform player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    private void RemoveAllPowerups()
    {
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (GameObject powerup in powerups)
        {
            Destroy(powerup);
        }
    }

    private void RemoveAllAsteroids()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }
    }

    private void DeactivatePlayerPowerups(Transform player)
    {
        Powerup powerup = player.GetComponent<Powerup>();
        if (powerup != null)
        {
            powerup.TogglePowerUp(false);
        }
    }
}
