using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int lives = 3; 
    public GameObject[] lifeIndicators;

    private int initialLives;
    private Damageable damageable;

    private void Start()
    {
        initialLives = lives;  // Store the initial number of lives
        damageable = GetComponent<Damageable>(); // Get the Damageable component
        UpdateLifeIndicators();
    }

    public void DecreaseLife()
    {
        if (lives > 0)
        {
            lives--;
            UpdateLifeIndicators();

            // Reset player damage when a life is lost
            ResetPlayerDamage();

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
        ResetPlayerDamage();  // Also reset the damage when lives are reset
    }

    public void ResetPlayerDamage()
    {
        if (damageable != null)
        {
            damageable.ResetDamage();
        }
    }
}
