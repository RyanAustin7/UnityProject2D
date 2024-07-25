using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerWinsPanelButtons : MonoBehaviour
{
    [Header("Player 1 Wins Panel")]
    [SerializeField] private GameObject player1WinsPanel;
    [SerializeField] private Button player1PlayAgainButton;
    [SerializeField] private Button player1MainMenuButton;

    [Header("Player 2 Wins Panel")]
    [SerializeField] private GameObject player2WinsPanel;
    [SerializeField] private Button player2PlayAgainButton;
    [SerializeField] private Button player2MainMenuButton;

    private EventSystem eventSystem;

    private void Start()
    {
        eventSystem = EventSystem.current;

        player1PlayAgainButton.onClick.AddListener(() => GameManager.Instance.PlayAgain());
        player1MainMenuButton.onClick.AddListener(() => GameManager.Instance.GoToMainMenu());
        player2PlayAgainButton.onClick.AddListener(() => GameManager.Instance.PlayAgain());
        player2MainMenuButton.onClick.AddListener(() => GameManager.Instance.GoToMainMenu());

        player1WinsPanel.SetActive(false);
        player2WinsPanel.SetActive(false);
    }

    public void ShowPlayer1WinsPanel()
    {
        ShowPanel(player1WinsPanel, player1PlayAgainButton);
    }

    public void ShowPlayer2WinsPanel()
    {
        ShowPanel(player2WinsPanel, player2PlayAgainButton);
    }

    private void ShowPanel(GameObject panel, Button defaultButton)
    {
        player1WinsPanel.SetActive(panel == player1WinsPanel);
        player2WinsPanel.SetActive(panel == player2WinsPanel);

        // Ensure the panel is the topmost
        panel.transform.SetAsLastSibling();

        // Force the default button to be selected
        StartCoroutine(SelectButton(defaultButton));
        
        // Ensure the button is interactable and visible
        EnsureButtonInteractable(defaultButton);

        Debug.Log("Showing panel: " + panel.name);
        Debug.Log("Button selected: " + defaultButton.name);
    }

    private IEnumerator SelectButton(Button button)
    {
        yield return null; // Wait a frame to ensure UI updates
        
        if (button != null)
        {
            eventSystem.SetSelectedGameObject(button.gameObject);
        }
    }

    private void EnsureButtonInteractable(Button button)
    {
        if (button != null)
        {
            button.interactable = true;  // Make sure the button is interactable
            if (!button.gameObject.activeInHierarchy)
            {
                button.gameObject.SetActive(true); // Ensure the button is active
            }
        }
    }
}
