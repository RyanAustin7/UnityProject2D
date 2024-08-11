using UnityEngine;
using AK.Wwise;

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

            // Reset active powerups on Life Lost
            StopActivePowerups();

            // Wwise event: Player 1 loses a life
            if (this == GameManager.Instance.playerLife1)
            {
                AkSoundEngine.PostEvent("SetState_B", gameObject);
                AkSoundEngine.PostEvent("LostLife_Stinger", gameObject);
            }
            // Wwise event: Player 2 loses a life
            else if (this == GameManager.Instance.playerLife2)
            {
                AkSoundEngine.PostEvent("SetState_A", gameObject);
                AkSoundEngine.PostEvent("LostLife_Stinger", gameObject);
            }

            // Check if both players have only 1 life left
            if (GameManager.Instance.playerLife1.lives == 1 && GameManager.Instance.playerLife2.lives == 1)
            {
                AkSoundEngine.PostEvent("SetState_C", gameObject);
                AkSoundEngine.PostEvent("LostLife_Stinger", gameObject);
            }

            if (lives <= 0)
            {
                GameManager.Instance.EndGame();
                AkSoundEngine.PostEvent("LostLife_Stinger", gameObject);
                AkSoundEngine.PostEvent("Clapping", gameObject);
            }
        }
    }

    private void StopActivePowerups()
    {
        // Call LoseLifeLosePower for each type of powerup
        FireRatePowerup fireRatePowerup = GetComponentInChildren<FireRatePowerup>();
        fireRatePowerup?.LoseLifeLosePower();

        SizePowerup sizePowerup = GetComponentInChildren<SizePowerup>();
        sizePowerup?.LoseLifeLosePower();

        SpeedPowerup speedPowerup = GetComponentInChildren<SpeedPowerup>();
        speedPowerup?.LoseLifeLosePower();
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