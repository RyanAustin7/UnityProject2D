using UnityEngine;

public class DashController : MonoBehaviour
{
    [Header("Dash Settings")]
    public KeyCode dashKey = KeyCode.LeftShift;
    public bool isDashing = false;
    public float dashDuration = 0.2f;
    public float dashSpeed = 10f;
    public float dashCooldown = 1.0f;
    public float dashDamageAmount = 0.4f; // Amount of damage applied during a dash
    public float dashKnockbackScale = 1.5f; // Knockback scale for dash

    [Header("Audio")]
    public AudioSource dashAudioSource;
    public AudioClip dashAudioClip;

    [Header("Trail Renderer")]
    public GameObject trailRendererObject;

    private float dashTime = 0;
    private float lastDashTime = 0;
    private Vector2 dashDirection;

    private void Update()
    {
        if (PauseMenu.IsGamePaused())
            return;

        if (Input.GetKeyDown(dashKey) && Time.time >= lastDashTime + dashCooldown)
        {
            StartDashing();
        }

        if (isDashing)
        {
            dashTime += Time.deltaTime;
            if (dashTime >= dashDuration)
            {
                StopDashing();
            }
            else
            {
                transform.position += (Vector3)dashDirection * dashSpeed * Time.deltaTime;
            }
        }
        else if (Time.time >= lastDashTime + dashCooldown)
        {
            if (trailRendererObject != null && !trailRendererObject.activeSelf)
            {
                trailRendererObject.SetActive(true);
            }
        }
    }

    private void StartDashing()
    {
        isDashing = true;
        dashTime = 0;
        dashDirection = new Vector2(transform.up.x, transform.up.y).normalized;
        lastDashTime = Time.time;

        if (trailRendererObject != null)
        {
            trailRendererObject.SetActive(false);
        }

        if (dashAudioSource != null && dashAudioClip != null)
        {
            dashAudioSource.PlayOneShot(dashAudioClip);
        }
    }

    private void StopDashing()
    {
        isDashing = false;
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing && IsValidCollision(collision.gameObject))
        {
            ApplyDashEffect(collision.gameObject);
        }
    }

    private bool IsValidCollision(GameObject other)
    {
        return (gameObject.CompareTag("Player1") && other.CompareTag("Player2")) ||
               (gameObject.CompareTag("Player2") && other.CompareTag("Player1"));
    }

    private void ApplyDashEffect(GameObject target)
    {
        Vector2 knockbackDirection = (target.transform.position - transform.position).normalized;

        KnockBackScript knockback = target.GetComponent<KnockBackScript>();
        if (knockback != null)
        {
            knockback.ApplyKnockback(knockbackDirection, dashKnockbackScale);
        }

        Damageable damageable = target.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.IncreaseDamage(dashDamageAmount);
        }
    }
}
