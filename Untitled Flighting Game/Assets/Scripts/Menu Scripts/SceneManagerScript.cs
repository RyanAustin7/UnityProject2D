using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject objectForLeftShiftKey; 
    [SerializeField] private GameObject objectForRightShiftKey; 
    [SerializeField] private float holdDuration = 2f;

    private SpriteRenderer leftShiftSpriteRenderer;
    private SpriteRenderer rightShiftSpriteRenderer;
    private float holdTime = 0f;

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

        // Check if both keys are held
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
