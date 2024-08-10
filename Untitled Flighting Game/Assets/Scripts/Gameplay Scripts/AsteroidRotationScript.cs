using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{
    public float minRotationSpeed = -50f;
    public float maxRotationSpeed = 50f;
    private float rotationSpeed;

    private void Start()
    {
        // Initialize rotation speed
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    private void Update()
    {
        // Rotate the asteroid
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
