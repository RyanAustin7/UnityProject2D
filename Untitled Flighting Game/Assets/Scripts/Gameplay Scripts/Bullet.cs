using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 11f;
    public float damageAmount = 0.8f;
    public float bulletKnockbackScale = 1.0f; // Knockback scale for bullets

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsValidBulletHit(collision.gameObject))
        {
            ApplyBulletEffect(collision.gameObject);
            Destroy(gameObject);
        }

        if (IsBulletOrWallCollision(collision.gameObject))
        {
            Destroy(gameObject);
        }
    }

    private bool IsValidBulletHit(GameObject other)
    {
        return (gameObject.CompareTag("Player1Bullet") && other.CompareTag("Player2")) ||
               (gameObject.CompareTag("Player2Bullet") && other.CompareTag("Player1"));
    }

    private bool IsBulletOrWallCollision(GameObject other)
    {
        return other.CompareTag("Wall") || 
               (gameObject.CompareTag("Player1Bullet") && other.CompareTag("Player1Bullet")) ||
               (gameObject.CompareTag("Player2Bullet") && other.CompareTag("Player2Bullet"));
    }

    private void ApplyBulletEffect(GameObject target)
    {
        Vector2 knockbackDirection = (target.transform.position - transform.position).normalized;

        KnockBackScript knockback = target.GetComponent<KnockBackScript>();
        if (knockback != null)
        {
            knockback.ApplyKnockback(knockbackDirection, bulletKnockbackScale);
        }

        Damageable damageable = target.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.IncreaseDamage(damageAmount);
        }
    }
}
