using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button controlsButton;
    [Header("Controls Menu")]
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject controlsPanel;
    [Header("Quit Game: Are You Sure?")]
    [SerializeField] private GameObject areYouSurePanel;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenuPanel.SetActive(false);
        controlsPanel.SetActive(false);  
        areYouSurePanel.SetActive(false);

        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(OpenAreYouSurePanel);
        settingsButton.onClick.AddListener(OpenSettings);
        controlsButton.onClick.AddListener(OpenControls);
        backButton.onClick.AddListener(CloseControls);
        yesButton.onClick.AddListener(QuitGame);
        noButton.onClick.AddListener(CloseAreYouSurePanel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        controlsPanel.SetActive(false);  
        areYouSurePanel.SetActive(false);
    }

    private void OpenSettings()
    {
        Debug.Log("Open Settings");
    }

    private void OpenControls()
    {
        pauseMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    private void CloseControls()
    {
        controlsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    private void OpenAreYouSurePanel()
    {
        pauseMenuPanel.SetActive(false);
        areYouSurePanel.SetActive(true);
    }

    private void CloseAreYouSurePanel()
    {
        areYouSurePanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    private void QuitGame()
    {
        Time.timeScale = 1f; // Ensure the game time is resumed before quitting
        SceneManager.LoadScene("Menu"); // Replace "Menu" with the name of your main menu scene
    }

    public static bool IsGamePaused()
    {
        return Time.timeScale == 0f;
    }
}
