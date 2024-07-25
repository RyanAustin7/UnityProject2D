using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonNavigation : MonoBehaviour
{
    [Header("Button Navigation Settings")]
    [SerializeField] private List<Button> buttons = new List<Button>(); // List of buttons to navigate
    [SerializeField] private Color selectedColor = Color.yellow; // Color when button is selected
    [SerializeField] private Color normalColor = Color.white; // Normal button color

    private int currentIndex = 0;

    void Start()
    {
        if (buttons != null && buttons.Count > 0)
        {
            SelectButton(currentIndex);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            NavigateUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            NavigateDown();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            ActivateButton();
        }
    }

    public void SetButtons(List<Button> newButtons)
    {
        buttons = newButtons;
        currentIndex = 0;
        if (buttons.Count > 0)
        {
            SelectButton(currentIndex);
        }
    }

    public void SetSelectedButton(Button button)
    {
        if (buttons.Contains(button))
        {
            currentIndex = buttons.IndexOf(button);
            SelectButton(currentIndex);
        }
    }

    private void NavigateUp()
    {
        if (buttons.Count == 0) return;

        DeselectButton(currentIndex);
        currentIndex = (currentIndex - 1 + buttons.Count) % buttons.Count;
        SelectButton(currentIndex);
    }

    private void NavigateDown()
    {
        if (buttons.Count == 0) return;

        DeselectButton(currentIndex);
        currentIndex = (currentIndex + 1) % buttons.Count;
        SelectButton(currentIndex);
    }

    private void ActivateButton()
    {
        if (buttons.Count == 0) return;

        buttons[currentIndex].onClick.Invoke();
    }

    private void SelectButton(int index)
    {
        if (index >= 0 && index < buttons.Count)
        {
            var button = buttons[index];
            var colors = button.colors;
            colors.normalColor = selectedColor;
            button.colors = colors;
        }
    }

    private void DeselectButton(int index)
    {
        if (index >= 0 && index < buttons.Count)
        {
            var button = buttons[index];
            var colors = button.colors;
            colors.normalColor = normalColor;
            button.colors = colors;
        }
    }
}
