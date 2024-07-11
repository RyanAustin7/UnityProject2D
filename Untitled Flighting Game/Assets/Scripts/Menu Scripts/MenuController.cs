using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void Start()
    {
        // Ensure the controls image is hidden at the start
        if (controlsImage != null)
            controlsImage.SetActive(false);

        // Ensure the "Are you sure?" panel is hidden at the start
        if (areYouSurePanel != null)
            areYouSurePanel.SetActive(false);

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
            noButton.onClick.AddListener(HideQuitPanel);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowQuitPanel();
        }
    }

    void ShowControls()
    {
        if (controlsImage != null)
            controlsImage.SetActive(true);
    }

    void HideControls()
    {
        if (controlsImage != null)
            controlsImage.SetActive(false);
    }

    void ShowQuitPanel()
    {
        if (areYouSurePanel != null)
            areYouSurePanel.SetActive(true);
    }

    void HideQuitPanel()
    {
        if (areYouSurePanel != null)
            areYouSurePanel.SetActive(false);
    }

    void QuitTheGame()
    {
        #if UNITY_EDITOR
        // This function is called if running game through UNITY
        EditorApplication.isPlaying = false;
        #else
        // This function is called if running game through BUILD
        Application.Quit();
        #endif
    }
}
