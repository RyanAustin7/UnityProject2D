using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; // Ensure this line is present

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

    private bool controlsPanelActive = false;
    private bool areYouSurePanelActive = false;
    private EventSystem eventSystem;
    public static bool isGamePaused = false; // Changed to static

    private bool canActivateButton = true;

    private void Start()
    {
        eventSystem = EventSystem.current;

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
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (controlsPanelActive)
            {
                CloseControls();
            }
            else if (areYouSurePanelActive)
            {
                CloseAreYouSurePanel();
            }
            else if (isGamePaused)
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
        isGamePaused = true;
        pauseMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);
        areYouSurePanel.SetActive(false);
        canActivateButton = true;

        AkSoundEngine.SetRTPCValue("Game_Paused", 1);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        pauseMenuPanel.SetActive(false);
        controlsPanel.SetActive(false);
        areYouSurePanel.SetActive(false);
        canActivateButton = true;

        AkSoundEngine.SetRTPCValue("Game_Paused", 0);
    }

    private void OpenSettings()
    {
        // Implement your settings menu logic here
    }

    private void OpenAreYouSurePanel()
    {
        areYouSurePanel.SetActive(true);
        controlsPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        areYouSurePanelActive = true;
        canActivateButton = true;
    }

    private void CloseAreYouSurePanel()
    {
        areYouSurePanel.SetActive(false);
        if (isGamePaused)
        {
            pauseMenuPanel.SetActive(true);
        }
        else if (controlsPanelActive)
        {
            controlsPanel.SetActive(true);
        }
        areYouSurePanelActive = false;
    }

    private void OpenControls()
    {
        controlsPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        areYouSurePanel.SetActive(false);
        controlsPanelActive = true;
        canActivateButton = true;
    }

    private void CloseControls()
    {
        controlsPanel.SetActive(false);
        if (isGamePaused)
        {
            pauseMenuPanel.SetActive(true);
        }
        controlsPanelActive = false;
    }

    public void QuitGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        Time.timeScale = 1f;
        ResumeGame();
        GameManager.Instance.GoToMainMenu();
    }
}
