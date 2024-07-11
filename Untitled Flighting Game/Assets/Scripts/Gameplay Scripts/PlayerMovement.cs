using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrust = 50f;
    public float rotationSpeed = 200f;
    public float maxSpeed = 10f;

    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rotateLeftKey;
    public KeyCode rotateRightKey;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ControlRocket();
        ClampVelocity();
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

        float rotationAmount = -horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotationAmount);

        rb.AddForce(transform.up * thrust * verticalInput);
    }

    private void ClampVelocity()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

    public void SetThrust(float newThrust)
    {
        thrust = newThrust;
    }
}
