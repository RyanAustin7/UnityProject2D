using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject objectForLeftShiftKey; 
    [SerializeField] private GameObject objectForRightShiftKey; 
    [SerializeField] private float holdDuration = 1f;

    private SpriteRenderer leftShiftSpriteRenderer;
    private SpriteRenderer rightShiftSpriteRenderer;
    private float holdTime = 0f;

    void Awake()
    {
        Time.timeScale = 1f; // Ensure time scale is set to normal
        Debug.Log("Time.timeScale set to 1 in Awake of SceneManagerScript");
    }

    void Start()
    {
        if (objectForLeftShiftKey != null)
            leftShiftSpriteRenderer = objectForLeftShiftKey.GetComponent<SpriteRenderer>();

        if (objectForRightShiftKey != null)
            rightShiftSpriteRenderer = objectForRightShiftKey.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        bool isLeftShiftHeld = Input.GetKey(KeyCode.LeftShift);
        bool isRightShiftHeld = Input.GetKey(KeyCode.RightShift);

        // Update the SpriteRenderer for the left Shift key
        if (leftShiftSpriteRenderer != null)
            leftShiftSpriteRenderer.enabled = isLeftShiftHeld;

        // Update the SpriteRenderer for the right Shift key
        if (rightShiftSpriteRenderer != null)
            rightShiftSpriteRenderer.enabled = isRightShiftHeld;

        // Check if both keys are held for the scene loading condition
        if (isLeftShiftHeld && isRightShiftHeld)
        {
            holdTime += Time.deltaTime;
            if (holdTime >= holdDuration)
            {
                SceneManager.LoadScene("TestScene");
            }
        }
        else
        {
            holdTime = 0f;
        }
    }
}
