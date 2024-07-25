using UnityEngine;
using TMPro;

public class StartGameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float holdDuration = 1f; // Duration to count up to 100
    [SerializeField] private TextMeshProUGUI timerText; // TMP for displaying the timer

    private float timer = 0f;
    private bool isCounting = false;

    void Start()
    {
        if (timerText != null)
        {
            timerText.text = "Fighting Game"; // Initial text
        }
    }

    void Update()
    {
        bool isLeftShiftHeld = Input.GetKey(KeyCode.LeftShift);
        bool isRightShiftHeld = Input.GetKey(KeyCode.RightShift);

        // Check if both Shift keys are pressed
        if (isLeftShiftHeld && isRightShiftHeld)
        {
            if (!isCounting)
            {
                StartTimer();
            }
        }
        else
        {
            if (isCounting)
            {
                StopTimer();
            }
        }

        if (isCounting)
        {
            UpdateTimer();
        }
    }

    private void StartTimer()
    {
        if (timerText != null)
        {
            timerText.text = "0%"; // Initialize the counter text
        }
        timer = 0f;
        isCounting = true;
    }

    private void StopTimer()
    {
        if (timerText != null)
        {
            timerText.text = "Fighting Game"; // Reset to initial text
        }
        isCounting = false;
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime * (100f / holdDuration); // Increment timer based on duration
        int percentage = Mathf.Clamp(Mathf.RoundToInt(timer), 0, 100); // Clamp the value between 0 and 100
        timerText.text = percentage.ToString() + "%"; // Update TMP text with percentage symbol

        if (timer >= 100f)
        {
            StopTimer(); // Stop counting when reaching 100
            timerText.text = "100%";
        }
    }
}
