using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D rb;

    private float minSpeedX;
    private float maxSpeedX;
    private float minSpeedY;
    private float maxSpeedY;
    private float minScale;
    private float maxScale;

    public void InitializeMovement(float minX, float maxX, float minY, float maxY, float minScale, float maxScale)
    {
        minSpeedX = minX;
        maxSpeedX = maxX;
        minSpeedY = minY;
        maxSpeedY = maxY;
        this.minScale = minScale;
        this.maxScale = maxScale;

        
        rb = GetComponent<Rigidbody2D>();

        float speedX = Random.Range(minSpeedX, maxSpeedX);
        float speedY = Random.Range(minSpeedY, maxSpeedY);
        rb.velocity = new Vector2(speedX, speedY);

        rb.gravityScale = 0;

        // Randomize the scale
        float randomScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AsteroidBreaker"))
        {
            Destroy(gameObject);
        }
    }
}
