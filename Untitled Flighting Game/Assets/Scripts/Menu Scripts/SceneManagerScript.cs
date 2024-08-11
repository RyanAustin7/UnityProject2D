using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject objectForLeftShiftKey; 
    [SerializeField] private GameObject objectForRightShiftKey; 
    [SerializeField] private float holdDuration = 1f;
    [SerializeField] private MenuController menuController; 

    private SpriteRenderer leftShiftSpriteRenderer;
    private SpriteRenderer rightShiftSpriteRenderer;
    private float holdTime = 0f;

    private bool isSnareRollPlaying = false;
    private bool isDrumFillPlaying = false;

    void Awake()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

        if (!menuController.IsControlsPanelVisible && !menuController.IsQuitPanelVisible)
        {
            if (leftShiftSpriteRenderer != null)
                leftShiftSpriteRenderer.enabled = isLeftShiftHeld;

            if (rightShiftSpriteRenderer != null)
                rightShiftSpriteRenderer.enabled = isRightShiftHeld;

            if (isLeftShiftHeld && isRightShiftHeld)
            {
                // Stop Snare_Roll if both keys are held
                if (isSnareRollPlaying)
                {
                    AkSoundEngine.PostEvent("Stop_Snare_Roll", gameObject);
                    isSnareRollPlaying = false;
                }

                // Play drum_fill if both keys are held
                if (!isDrumFillPlaying)
                {
                    AkSoundEngine.PostEvent("drum_fill", gameObject);
                    isDrumFillPlaying = true;
                }

                holdTime += Time.deltaTime;
                if (holdTime >= holdDuration)
                {
                    SceneManager.LoadScene("TestScene");
                }
            }
            else if (isLeftShiftHeld || isRightShiftHeld)
            {
                // Stop drum_fill if only one key is held
                if (isDrumFillPlaying)
                {
                    AkSoundEngine.PostEvent("Stop_drum_fill", gameObject);
                    isDrumFillPlaying = false;
                }

                // Play Snare_Roll if only one key is held
                if (!isSnareRollPlaying)
                {
                    AkSoundEngine.PostEvent("Snare_Roll", gameObject);
                    isSnareRollPlaying = true;
                }

                holdTime = 0f;
            }
            else
            {
                // Stop both sounds if no keys are held
                if (isSnareRollPlaying)
                {
                    AkSoundEngine.PostEvent("Stop_Snare_Roll", gameObject);
                    isSnareRollPlaying = false;
                }
                
                if (isDrumFillPlaying)
                {
                    AkSoundEngine.PostEvent("Stop_drum_fill", gameObject);
                    isDrumFillPlaying = false;
                }

                holdTime = 0f;
            }
        }
    }
}
