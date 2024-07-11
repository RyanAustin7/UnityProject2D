using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float maxRotation;
    private float rotationZ;
    private Rigidbody2D rb;

    private float minSpeedX;
    private float maxSpeedX;
    private float minSpeedY;
    private float maxSpeedY;

    public void InitializeMovement(float minX, float maxX, float minY, float maxY)
    {
        minSpeedX = minX;
        maxSpeedX = maxX;
        minSpeedY = minY;
        maxSpeedY = maxY;

        maxRotation = 25f;
        rotationZ = Random.Range(-maxRotation, maxRotation);

        rb = GetComponent<Rigidbody2D>();

        float speedX = Random.Range(minSpeedX, maxSpeedX);
        float speedY = Random.Range(minSpeedY, maxSpeedY);
        rb.velocity = new Vector2(speedX, speedY);

        rb.gravityScale = 0;
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotationZ * Time.deltaTime);
    }

    
}
