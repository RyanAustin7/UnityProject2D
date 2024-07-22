using UnityEngine;
using TMPro;

public class MyTimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // Reference to the TextMeshProUGUI component

    private float elapsedTime = 0f;

    private void Update()
    {
        // Update elapsed time
        elapsedTime += Time.deltaTime;

        // Update the timer text
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        // Calculate minutes, seconds, and milliseconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime % 1f) * 10f); // Single digit milliseconds (0-9)

        // Format the timer text
        if (minutes > 0)
        {
            timerText.text = string.Format("{0}:{1:00}.{2}", minutes, seconds, milliseconds);
        }
        else
        {
            timerText.text = string.Format("{0}.{1}", seconds, milliseconds);
        }
    }
}
