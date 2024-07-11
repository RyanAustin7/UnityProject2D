using UnityEngine;

public class Respawn : MonoBehaviour
{
    public PlayerLife playerLife1; 
    public PlayerLife playerLife2;

    public float respawnMass = 0.5f; // Mass to set during the blink animation

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player1"))
        {
            HandlePlayerCollision(collider, playerLife1); 
        }
        else if (collider.CompareTag("Player2"))
        {
            HandlePlayerCollision(collider, playerLife2); 
        }
    }

    private void HandlePlayerCollision(Collider2D collider, PlayerLife playerLife)
    {
        if (playerLife != null)
        {
            playerLife.DecreaseLife();
            RespawnPlayer(collider.transform);
            RespawnBlink respawnBlink = collider.GetComponent<RespawnBlink>();
            if (respawnBlink != null)
            {
                respawnBlink.StartBlinking(respawnMass); // Pass the mass to RespawnBlink
            }
        }
    }

    private void RespawnPlayer(Transform player)
    {
        player.position = Vector3.zero; 
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; 
        }
    }
}
