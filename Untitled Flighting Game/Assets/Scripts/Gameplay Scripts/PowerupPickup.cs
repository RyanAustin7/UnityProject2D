using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    private Powerup powerupScript;

    private void Start()
    {
        powerupScript = GetComponent<Powerup>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            Debug.Log("Powerup picked up by: " + collision.tag); // Debugging line
            collision.gameObject.GetComponent<Powerup>().TogglePowerUp(true);  // Activate powerup for the player
            Destroy(gameObject);  // Destroy the powerup object
        }
    }
}
