using UnityEngine;

public class DashController : MonoBehaviour
{
    [Header("Dash Settings")]
    public KeyCode dashKey = KeyCode.LeftShift;  // Key to initiate the dash, settable in the Inspector
    public bool isDashing = false;
    public float dashDuration = 0.2f; // Duration of the dash
    public float dashSpeed = 10f;    // Speed of the dash
    public float dashCooldown = 1.0f; // Cooldown time between dashes
    public float knockbackForce = 10f; // Knockback force applied to the opponent during dash

    [Header("Audio")]
    public AudioSource dashAudioSource; // AudioSource for dashing sound
    public AudioClip dashAudioClip; // AudioClip for dashing sound

    [Header("Trail Renderer")]
    public GameObject trailRendererObject; // The child GameObject with the Trail Renderer

    private float dashTime = 0;
    private float lastDashTime = 0; // Time when the last dash occurred
    private Vector2 dashDirection;

    private void Update()
    {
        if (PauseMenu.IsGamePaused())
            return; // Skip update if the game is paused

        // Check for the dash key input only if the cooldown period has passed
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
                // Continue to apply dash movement
                transform.position += (Vector3)dashDirection * dashSpeed * Time.deltaTime;
            }
        }
        else if (Time.time >= lastDashTime + dashCooldown)
        {
            // Reactivate the trail renderer when the cooldown is over
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
        dashDirection = new Vector2(transform.up.x, transform.up.y).normalized;  // Set dash direction based on current facing direction
        lastDashTime = Time.time; // Record the time of the current dash

        // Deactivate the trail renderer during the dash cooldown
        if (trailRendererObject != null)
        {
            trailRendererObject.SetActive(false);
        }

        // Play the dash sound
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
        // Check if the collision is with an opponent and that dashing is active
        if (isDashing &&
            ((gameObject.CompareTag("Player1") && collision.gameObject.CompareTag("Player2")) ||
            (gameObject.CompareTag("Player2") && collision.gameObject.CompareTag("Player1"))))
        {
            // Get the direction for knockback
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            knockbackDirection.y = 0; // Ensure knockback is only horizontal

            // Apply the knockback force to the opponent's Rigidbody2D
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
