using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    // Define the PowerupType enum within this script
    public enum PowerupType
    {
        FireRate,
        Speed,
        Size
    }

    // Field to specify the type of powerup
    public PowerupType powerupType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            Debug.Log("Powerup picked up by: " + collision.tag);

            // Find the player Powerup components
            FireRatePowerup fireRatePowerup = collision.GetComponentInChildren<FireRatePowerup>();
            SpeedPowerup speedPowerup = collision.GetComponentInChildren<SpeedPowerup>();
            SizePowerup sizePowerup = collision.GetComponentInChildren<SizePowerup>();

            switch (powerupType)
            {
                case PowerupType.FireRate:
                    fireRatePowerup?.TogglePowerUp(true);
                    break;
                case PowerupType.Speed:
                    speedPowerup?.TogglePowerUp(true);
                    break;
                case PowerupType.Size:
                    sizePowerup?.TogglePowerUp(true);
                    break;
            }

            Destroy(gameObject);
        }
    }
}
