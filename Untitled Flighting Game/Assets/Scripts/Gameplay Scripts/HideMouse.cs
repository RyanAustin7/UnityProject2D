using UnityEngine;
using UnityEngine.SceneManagement;

public class HideMouse : MonoBehaviour
{
    [Header("Cursor Settings")]
    public bool hideCursorWhenUnpaused = true;

    private void Update()
    {
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
         
    }
}
