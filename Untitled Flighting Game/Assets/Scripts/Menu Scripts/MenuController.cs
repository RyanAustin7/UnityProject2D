using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuController : MonoBehaviour
{
    [Header("Controls Panel Settings")]
    [SerializeField] private GameObject controlsImage; 
    [SerializeField] private Button controlsButton;    
    [SerializeField] private Button backButton;       

    [Header("Quit Panel Settings")]
    [SerializeField] private GameObject areYouSurePanel; 
    [SerializeField] private Button quitButton;        
    [SerializeField] private Button yesButton;         
    [SerializeField] private Button noButton;          

    [Header("Settings Panel Settings")]
    [SerializeField] private GameObject settingsPanel; // Reference to settings panel
    [SerializeField] private Button settingsButton;     // Reference to settings button
    [SerializeField] private Button settingsBackButton; // Reference to back button on settings panel

    [Header("Button Navigation Settings")]
    [SerializeField] private ButtonNavigation buttonNavigation; // Reference to the ButtonNavigation script

    public bool IsControlsPanelVisible { get; private set; } = false;
    public bool IsQuitPanelVisible { get; private set; } = false;
    public bool IsSettingsPanelVisible { get; private set; } = false;

    private void Start()
    {
        // Ensure the panels are hidden at the start
        if (controlsImage != null)
            controlsImage.SetActive(false);

        if (areYouSurePanel != null)
            areYouSurePanel.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        // Add listeners to the buttons
        if (controlsButton != null)
            controlsButton.onClick.AddListener(ShowControls);

        if (backButton != null)
            backButton.onClick.AddListener(HideControls);

        if (quitButton != null)
            quitButton.onClick.AddListener(ShowQuitPanel);

        if (yesButton != null)
            yesButton.onClick.AddListener(QuitTheGame);

        if (noButton != null)
            noButton.onClick.AddListener(NoClicked);

        if (settingsButton != null)
            settingsButton.onClick.AddListener(ShowSettings);

        if (settingsBackButton != null)
            settingsBackButton.onClick.AddListener(HideSettings);

        // Set up ButtonNavigation
        if (buttonNavigation != null)
        {
            // Initially, only the main buttons are navigable
            buttonNavigation.SetButtons(new List<Button> { controlsButton, quitButton, settingsButton });
            buttonNavigation.SetSelectedButton(controlsButton); // Set the default selection
        }
    }

    private void Update()
    {
        // Only handle key inputs if neither panel is visible
        if (!IsControlsPanelVisible && !IsQuitPanelVisible && !IsSettingsPanelVisible)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowQuitPanel();
            }

            // Check shift key inputs here if needed
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                // Handle shift key inputs if necessary
            }
        }
    }

    private void ShowControls()
    {
        if (controlsImage != null)
            controlsImage.SetActive(true);
        IsControlsPanelVisible = true;

        // Switch to controls panel buttons
        if (buttonNavigation != null)
        {
            buttonNavigation.SetButtons(new List<Button> { backButton });
            buttonNavigation.SetSelectedButton(backButton); // Highlight backButton
        }
    }

    private void HideControls()
    {
        if (controlsImage != null)
            controlsImage.SetActive(false);
        IsControlsPanelVisible = false;

        // Switch back to main buttons
        if (buttonNavigation != null)
        {
            buttonNavigation.SetButtons(new List<Button> { controlsButton, quitButton, settingsButton });
            buttonNavigation.SetSelectedButton(controlsButton); // Highlight controlsButton
        }
    }

    private void ShowQuitPanel()
    {
        if (areYouSurePanel != null)
            areYouSurePanel.SetActive(true);
        IsQuitPanelVisible = true;

        // Switch to quit panel buttons
        if (buttonNavigation != null)
        {
            buttonNavigation.SetButtons(new List<Button> { yesButton, noButton });
            buttonNavigation.SetSelectedButton(yesButton); // Highlight yesButton
        }
    }

    private void HideQuitPanel()
    {
        if (areYouSurePanel != null)
            areYouSurePanel.SetActive(false);
        IsQuitPanelVisible = false;

        // Switch back to main buttons
        if (buttonNavigation != null)
        {
            buttonNavigation.SetButtons(new List<Button> { controlsButton, quitButton, settingsButton });
            buttonNavigation.SetSelectedButton(quitButton); // Highlight quitButton
        }
    }

    private void ShowSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
        IsSettingsPanelVisible = true;

        // Switch to settings panel buttons
        if (buttonNavigation != null)
        {
            buttonNavigation.SetButtons(new List<Button> { settingsBackButton });
            buttonNavigation.SetSelectedButton(settingsBackButton); // Highlight settingsBackButton
        }
    }

    private void HideSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
        IsSettingsPanelVisible = false;

        // Switch back to main buttons
        if (buttonNavigation != null)
        {
            buttonNavigation.SetButtons(new List<Button> { controlsButton, quitButton, settingsButton });
            buttonNavigation.SetSelectedButton(settingsButton); // Highlight settingsButton
        }
    }

    private void NoClicked()
    {
        HideQuitPanel();

        // Re-select the quit button after closing the quit panel
        if (buttonNavigation != null)
        {
            buttonNavigation.SetSelectedButton(quitButton); // Ensure quitButton is highlighted
        }
    }

    private void QuitTheGame()
    {
        #if UNITY_EDITOR
        // This function is called if running game through UNITY
        EditorApplication.isPlaying = false;
        #else
        // This function is called if running game through BUILD
        Application.Quit();
        #endif
    }

    public enum Direction
    {
        Up,
        Down
    }
}
