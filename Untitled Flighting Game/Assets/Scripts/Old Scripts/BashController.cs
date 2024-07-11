using UnityEngine;
using System.Collections;

public class BashController : MonoBehaviour
{
    public float bashDistance = 10f;
    public float bashDuration = 0.2f;
    public KeyCode bashKey = KeyCode.Q;

    private Rigidbody2D rb;
    private bool isBashing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isBashing)
        {
            ControlRocket();
        }
    }

    private void Update()
    {
        HandleBash();
    }

    public void HandleBash()
    {
        if (Input.GetKeyDown(bashKey) && !isBashing)
        {
            StartCoroutine(BashCoroutine());
        }
    }

    private IEnumerator BashCoroutine()
    {
        isBashing = true;
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startPosition + (Vector2)transform.up * bashDistance;
        float elapsedTime = 0f;

        while (elapsedTime < bashDuration)
        {
            rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, elapsedTime / bashDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(targetPosition);
        isBashing = false;
    }

    private void ControlRocket()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float rotationAmount = -horizontalInput * 180f * Time.deltaTime;
        transform.Rotate(0, 0, rotationAmount);

        rb.AddForce(transform.up * 6f * verticalInput);
    }
}
