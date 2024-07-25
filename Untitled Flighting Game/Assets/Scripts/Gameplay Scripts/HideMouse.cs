using UnityEngine;
using UnityEngine.SceneManagement;

public class HideMouse : MonoBehaviour
{
    [Header("Cursor Settings")]
    public bool hideCursorWhenUnpaused = true;

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void Update()
    {
        // Get the current active scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check if the game is paused, if time scale is 0, or if the current scene is the main menu
        if (PauseMenu.isGamePaused || Time.timeScale == 0f || currentSceneName == "MainMenu")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        }
    }

    private void OnSceneUnloaded(Scene current)
    {
        // Destroy this game object when the scene is unloaded
        Destroy(gameObject);
    }
}
