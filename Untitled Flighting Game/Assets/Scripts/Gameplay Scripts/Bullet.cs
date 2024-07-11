using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 11f;
    public float knockbackForce = 10f; 

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((gameObject.CompareTag("Player1Bullet") && collision.gameObject.CompareTag("Player2")) ||
            (gameObject.CompareTag("Player2Bullet") && collision.gameObject.CompareTag("Player1")))
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }

            Destroy(gameObject);
        }

        if ((gameObject.CompareTag("Player1Bullet") && collision.gameObject.CompareTag("Player1Bullet")) ||
            (gameObject.CompareTag("Player2Bullet") && collision.gameObject.CompareTag("Player2Bullet")))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
