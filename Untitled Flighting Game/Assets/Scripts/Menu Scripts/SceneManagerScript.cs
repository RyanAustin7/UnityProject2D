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
        AkSoundEngine.PostEvent("Play_Menu", gameObject);

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
                    AkSoundEngine.SetRTPCValue("DrumRoll", 0, gameObject); // Set DrumRoll to 0
                    isSnareRollPlaying = false;
                }

                // Play drum_fill if both keys are held
                if (!isDrumFillPlaying)
                {
                    AkSoundEngine.PostEvent("drum_fill", gameObject);
                    AkSoundEngine.SetRTPCValue("DrumRoll", 100, gameObject); // Set DrumRoll to 100
                    isDrumFillPlaying = true;
                }

                holdTime += Time.deltaTime;
                if (holdTime >= holdDuration)
                {
                    AkSoundEngine.PostEvent("Stop_Menu_Music", gameObject);
                    SceneManager.LoadScene("TestScene");
                }
            }
            else if (isLeftShiftHeld || isRightShiftHeld)
            {
                // Stop drum_fill if only one key is held
                if (isDrumFillPlaying)
                {
                    AkSoundEngine.PostEvent("Stop_drum_fill", gameObject);
                    AkSoundEngine.SetRTPCValue("DrumRoll", 0, gameObject); // Set DrumRoll to 0
                    isDrumFillPlaying = false;
                }

                // Play Snare_Roll if only one key is held
                if (!isSnareRollPlaying)
                {
                    AkSoundEngine.PostEvent("Snare_Roll", gameObject);
                    AkSoundEngine.SetRTPCValue("DrumRoll", 50, gameObject); // Set DrumRoll to 50
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

                // Set DrumRoll to 0 if neither sound is playing
                AkSoundEngine.SetRTPCValue("DrumRoll", 0, gameObject);
                holdTime = 0f;
            }
        }
    }
}
