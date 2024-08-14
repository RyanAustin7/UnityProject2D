using UnityEngine;

public class CursorManagerScript : MonoBehaviour
{
    public Texture2D customCursor;
    public Vector2 hotspot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    private static CursorManagerScript instance; // Corrected the class name here

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persist across scenes
            SetCustomCursor();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate CursorManagerScript objects
        }
    }

    public void SetCustomCursor()
    {
        if (customCursor != null)
        {
            Cursor.SetCursor(customCursor, hotspot, cursorMode);
        }
    }
}
