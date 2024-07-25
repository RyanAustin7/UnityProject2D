using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrust = 50f;
    public float rotationSpeed = 200f;
    
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rotateLeftKey;
    public KeyCode rotateRightKey;

    private Rigidbody2D rb;
    [HideInInspector] public float originalThrust; // Store original thrust value
    [HideInInspector] public float originalRotationSpeed; // Store original rotation speed

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalThrust = thrust; // Initialize original thrust value
        originalRotationSpeed = rotationSpeed; // Initialize original rotation speed value
    }

    private void FixedUpdate()
    {
        ControlRocket();
    }

    private void ControlRocket()
    {
        float horizontalInput = 0;
        if (Input.GetKey(rotateLeftKey))
        {
            horizontalInput = -1;
        }
        else if (Input.GetKey(rotateRightKey))
        {
            horizontalInput = 1;
        }

        float verticalInput = 0;
        if (Input.GetKey(upKey))
        {
            verticalInput = 1;
        }
        else if (Input.GetKey(downKey))
        {
            verticalInput = -1;
        }

        // Rotation
        float rotationAmount = -horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotationAmount);

        // Apply thrust in the direction the player is facing
        Vector2 thrustDirection = transform.up;
        rb.AddForce(thrustDirection * thrust * verticalInput);
    }

    public void SetThrust(float newThrust)
    {
        thrust = newThrust;
    }

    public void ResetThrust()
    {
        thrust = originalThrust;
    }

    public void SetRotationSpeed(float newRotationSpeed)
    {
        rotationSpeed = newRotationSpeed;
    }

    public void ResetRotationSpeed()
    {
        rotationSpeed = originalRotationSpeed;
    }
}
