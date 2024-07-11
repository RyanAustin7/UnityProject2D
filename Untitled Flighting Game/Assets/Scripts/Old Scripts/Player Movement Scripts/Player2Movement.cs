using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    public float thrust = 300f;
    public float rotationSpeed = 300f;
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
        CheckPosition();
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
        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed),
            Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed)
        );
    }

    private void CheckPosition()
    {
        Camera mainCam = Camera.main;
        float sceneWidth = mainCam.orthographicSize * 2 * mainCam.aspect;
        float sceneHeight = mainCam.orthographicSize * 2;

        float sceneRightEdge = sceneWidth / 2;
        float sceneLeftEdge = sceneRightEdge * -1;
        float sceneTopEdge = sceneHeight / 2;
        float sceneBottomEdge = sceneTopEdge * -1;

        Vector2 newPosition = transform.position;

        if (transform.position.x > sceneRightEdge)
        {
            newPosition.x = sceneLeftEdge;
        }
        else if (transform.position.x < sceneLeftEdge)
        {
            newPosition.x = sceneRightEdge;
        }

        if (transform.position.y > sceneTopEdge)
        {
            newPosition.y = sceneBottomEdge;
        }
        else if (transform.position.y < sceneBottomEdge)
        {
            newPosition.y = sceneTopEdge;
        }

        transform.position = newPosition;
    }
}
