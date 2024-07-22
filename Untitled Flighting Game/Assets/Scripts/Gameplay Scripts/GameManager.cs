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

    // Buttons for Player 1 and Player 2 win panels
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

    [Header("Powerup Spawner")]
    public PowerupSpawner powerupSpawner; // Reference to the powerup spawner

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
        // Ensure all buttons and panels are set up
        if (player1PlayAgainButton == null || player1MainMenuButton == null || player2PlayAgainButton == null || player2MainMenuButton == null)
        {
            Debug.LogError("One or more buttons are not assigned in the GameManager.");
            return;
        }

        if (player1WinsPanel == null || player2WinsPanel == null)
        {
            Debug.LogError("One or more win panels are not assigned in the GameManager.");
            return;
        }

        // Set up button listeners
        player1PlayAgainButton.onClick.AddListener(PlayAgain);
        player1MainMenuButton.onClick.AddListener(GoToMainMenu);
        player2PlayAgainButton.onClick.AddListener(PlayAgain);
        player2MainMenuButton.onClick.AddListener(GoToMainMenu);

        // Initially hide the win panels
        player1WinsPanel.SetActive(false);
        player2WinsPanel.SetActive(false);

        // Store initial player positions
        player1StartPos = player1.position;
        player2StartPos = player2.position;
    }

    private void Update()
    {
        // Check for game end conditions every frame
        if (playerLife1.lives <= 0 || playerLife2.lives <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        // Check the conditions and show the appropriate panel
        if (playerLife1.lives <= 0)
        {
            ShowGameOver(player2WinsPanel);
        }
        else if (playerLife2.lives <= 0)
        {
            ShowGameOver(player1WinsPanel);
        }

        RemoveAllPowerups();
    }

    private void ShowGameOver(GameObject winnerPanel)
    {
        // Pause the game
        Time.timeScale = 0f;

        // Show the winner panel
        player1WinsPanel.SetActive(winnerPanel == player1WinsPanel);
        player2WinsPanel.SetActive(winnerPanel == player2WinsPanel);
    }

    private void PlayAgain()
    {
        // Hide the win panels
        player1WinsPanel.SetActive(false);
        player2WinsPanel.SetActive(false);

        // Resume game time
        Time.timeScale = 1f;

        // Reset the game
        ResetGame();
    }

    private void GoToMainMenu()
    {
        // Resume game time and go to the main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); // Replace "Menu" with the name of your main menu scene
    }

    private void ResetGame()
    {
        // Reset players' lives
        playerLife1.ResetLives();
        playerLife2.ResetLives();

        // Reset players' positions and rotations
        player1.position = player1StartPos;
        player2.position = player2StartPos;

        player1.rotation = Quaternion.Euler(0f, 0f, -90f);
        player2.rotation = Quaternion.Euler(0f, 0f, 90f);

        // Reset players' velocities (if they have Rigidbody2D components)
        Rigidbody2D rb1 = player1.GetComponent<Rigidbody2D>();
        if (rb1 != null)
        {
            rb1.velocity = Vector2.zero;
            rb1.angularVelocity = 0f;
        }

        Rigidbody2D rb2 = player2.GetComponent<Rigidbody2D>();
        if (rb2 != null)
        {
            rb2.velocity = Vector2.zero;
            rb2.angularVelocity = 0f;
        }

        // Reset the asteroid manager
        if (asteroidManager != null)
        {
            asteroidManager.ResetAsteroids();
        }

        // Reset the powerup spawner
        if (powerupSpawner != null)
        {
            powerupSpawner.ResetPowerupSpawner();
        }

        // Deactivate any active powerups for the players
        DeactivatePlayerPowerups(player1);
        DeactivatePlayerPowerups(player2);
    }

    private void RemoveAllPowerups()
    {
        // removes powerups on field tagged as "Powerup"
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (GameObject powerup in powerups)
        {
            Destroy(powerup);
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
