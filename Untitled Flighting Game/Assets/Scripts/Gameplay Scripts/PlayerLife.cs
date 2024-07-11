using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int lives = 3; 
    public GameObject[] lifeIndicators; 

    private void Start()
    {
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
}
