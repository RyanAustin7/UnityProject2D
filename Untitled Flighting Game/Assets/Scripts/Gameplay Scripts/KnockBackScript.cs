using UnityEngine;

public class KnockBackScript : MonoBehaviour
{
    public float baseKnockbackForce = 10f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 direction, float knockbackScale)
    {
        Damageable damageable = GetComponent<Damageable>();
        if (damageable != null)
        {
            float damage = damageable.damageTaken;
            float knockbackStrength = baseKnockbackForce * damage * knockbackScale;

            if (rb != null)
            {
                rb.velocity = direction * knockbackStrength;
            }
        }
    }
}
