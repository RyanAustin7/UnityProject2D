using UnityEngine;
using TMPro;

public class MyTimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component
    private float elapsedTime = 0f;   // Variable to track elapsed time
    private bool isRunning = false;   // Flag to check if the timer is running

    private void Start()
    {
        timerText.text = "0.0"; // Initialize the timer display
        StartTimer();
    }

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        float seconds = elapsedTime % 60f;

        if (minutes > 0)
        {
            timerText.text = string.Format("{0}:{1:00.0}", minutes, seconds);
        }
        else
        {
            timerText.text = string.Format("{0:0.0}", seconds);
        }
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        StopTimer();
        elapsedTime = 0f;
        timerText.text = "0.0";
        StartTimer();
    }
}
