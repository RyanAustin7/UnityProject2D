using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int lives = 3; 
    public GameObject[] lifeIndicators;

    private int initialLives;

    private void Start()
    {
        initialLives = lives;  // Store the initial number of lives
        UpdateLifeIndicators();
    }

    public void DecreaseLife()
    {
        if (lives > 0)
        {
            lives--;
            UpdateLifeIndicators();

            if (lives <= 0)
            {
                GameManager.Instance.EndGame();
            }
        }
    }

    private void UpdateLifeIndicators()
    {
        for (int i = 0; i < lifeIndicators.Length; i++)
        {
            lifeIndicators[i].GetComponent<SpriteRenderer>().enabled = i < lives;
        }
    }

    public void ResetLives()
    {
        lives = initialLives;  // Reset lives to initial value
        UpdateLifeIndicators();
    }
}
