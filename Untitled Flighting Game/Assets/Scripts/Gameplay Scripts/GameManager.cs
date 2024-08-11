using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
    public AsteroidSpawner[] asteroidSpawners;

    [Header("Powerup Spawner")]
    public PowerupSpawner powerupSpawner;

    [Header("Game Timer")]
    public MyTimerScript MyTimerScript;

    [Header("Round Counter")]
    public RoundCounter roundCounter;

    [Header("Dash Controllers")]
    public DashController player1DashController;
    public DashController player2DashController;

    [Header("Shooting Controllers")]
    public ShootingController player1ShootingController;
    public ShootingController player2ShootingController;

    private Vector3 player1StartPos;
    private Vector3 player2StartPos;
    private bool hasPlayer1LostAllLives = false;
    private bool hasPlayer2LostAllLives = false;

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

        // Ensure the game starts unpaused
        Time.timeScale = 1f;

        InitializePlayers();
    }

    private void InitializePlayers()
    {
        player1DashController = player1.GetComponent<DashController>();
        player2DashController = player2.GetComponent<DashController>();
        player1ShootingController = player1.GetComponent<ShootingController>();
        player2ShootingController = player2.GetComponent<ShootingController>();

        if (player1DashController == null || player2DashController == null)
        {
            Debug.LogError("DashController not found on one of the players.");
        }

        if (player1ShootingController == null || player2ShootingController == null)
        {
            Debug.LogError("ShootingController not found on one of the players.");
        }
    }

    private void Update()
    {
        if (playerLife1.lives <= 0 && !hasPlayer1LostAllLives)
        {
            hasPlayer1LostAllLives = true;
            EndGame();
        }
        else if (playerLife2.lives <= 0 && !hasPlayer2LostAllLives)
        {
            hasPlayer2LostAllLives = true;
            EndGame();
        }
    }

    public void EndGame()
    {
        if (playerLife1.lives <= 0)
        {
            ShowGameOver(player2WinsPanel);
            if (!hasPlayer2LostAllLives)
            {
                roundCounter?.IncrementPlayer2Round();
                hasPlayer2LostAllLives = true;
            }
        }
        else if (playerLife2.lives <= 0)
        {
            ShowGameOver(player1WinsPanel);
            if (!hasPlayer1LostAllLives)
            {
                roundCounter?.IncrementPlayer1Round();
                hasPlayer1LostAllLives = true;
            }
        }

        RemoveAllAsteroids();
        player1.position = player1StartPos;
        player2.position = player2StartPos;
    }

    private void ShowGameOver(GameObject winnerPanel)
    {
        Time.timeScale = 0f;
        player1WinsPanel.SetActive(winnerPanel == player1WinsPanel);
        player2WinsPanel.SetActive(winnerPanel == player2WinsPanel);
    }

    public void PlayAgain()
    {
        // Ensure the game is resumed
        Time.timeScale = 1f;

        // Reset the game
        ResetGame();

        // Hide the win panels
        player1WinsPanel.SetActive(false);
        player2WinsPanel.SetActive(false);

        // Reset the flags for next round
        hasPlayer1LostAllLives = false;
        hasPlayer2LostAllLives = false;

        AkSoundEngine.PostEvent("LostLife_Stinger", gameObject);
    }

    public void GoToMainMenu()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        AkSoundEngine.PostEvent("ExitToMenu", gameObject);
        
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
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

        RemoveAllAsteroids();

        asteroidManager.ResetAsteroids();
        powerupSpawner.ResetSpawner();

        foreach (var spawner in asteroidSpawners)
        {
            spawner.ResetAsteroidSpawner();
        }

        MyTimerScript.ResetTimer();

        DeactivatePlayerPowerups(player1);
        DeactivatePlayerPowerups(player2);

        SetPlayerScale(player1, new Vector3(1.3f, 1.3f, 1.3f));
        SetPlayerScale(player2, new Vector3(1.3f, 1.3f, 1.3f));

        SetPlayerVisibility(player1, true);
        SetPlayerVisibility(player2, true);

        player1DashController?.ResetDash();
        player2DashController?.ResetDash();
        player1ShootingController?.ResetShooting();
        player2ShootingController?.ResetShooting();

        AkSoundEngine.SetRTPCValue("FireRate_Powerup", 0);
        AkSoundEngine.SetRTPCValue("Speed_Powerup", 0);
        AkSoundEngine.SetRTPCValue("Size_Powerup", 0);
    }

    private void SetPlayerScale(Transform player, Vector3 scale)
    {
        player.localScale = scale;
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
        SpeedPowerup speedPowerup = player.GetComponent<SpeedPowerup>();
        FireRatePowerup fireRatePowerup = player.GetComponent<FireRatePowerup>();
        SizePowerup sizePowerup = player.GetComponent<SizePowerup>();

        if (speedPowerup != null)
        {
            speedPowerup.TogglePowerUp(false);
        }
        if (fireRatePowerup != null)
        {
            fireRatePowerup.TogglePowerUp(false);
        }
        if (sizePowerup != null)
        {
            sizePowerup.TogglePowerUp(false);
        }
    }

    private void SetPlayerVisibility(Transform player, bool visible)
    {
        SpriteRenderer renderer = player.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.enabled = visible;
        }
    }
}
