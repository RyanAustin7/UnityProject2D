using UnityEngine;
using TMPro;

public class RoundCounter : MonoBehaviour
{
    [Header("TextMeshProUGUI References")]
    public TextMeshProUGUI player1RoundCounter;
    public TextMeshProUGUI player2RoundCounter;

    private int player1Rounds = 0;
    private int player2Rounds = 0;

    private void Start()
    {
        // Initialize the round counters
        UpdateRoundCounters();
    }

    // Call this method when Player 1 loses all lives
    public void IncrementPlayer1Round()
    {
        player1Rounds++;  // Increment by 1
        UpdateRoundCounters();
    }

    // Call this method when Player 2 loses all lives
    public void IncrementPlayer2Round()
    {
        player2Rounds++;  // Increment by 1
        UpdateRoundCounters();
    }

    private void UpdateRoundCounters()
    {
        // Update the TMP texts with the current round counts
        if (player1RoundCounter != null)
        {
            player1RoundCounter.text = player1Rounds.ToString();
        }

        if (player2RoundCounter != null)
        {
            player2RoundCounter.text = player2Rounds.ToString();
        }
    }
}
