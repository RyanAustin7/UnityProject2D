using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    }

    private void ShowGameOver(GameObject winnerPanel)
    {
        // Show the winner panel
        player1WinsPanel.SetActive(winnerPanel == player1WinsPanel);
        player2WinsPanel.SetActive(winnerPanel == player2WinsPanel);
    }

    private void PlayAgain()
    {
        // Resume game time and reload the scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GoToMainMenu()
    {
        // Resume game time and go to the main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); // Replace "Menu" with the name of your main menu scene
    }
}
