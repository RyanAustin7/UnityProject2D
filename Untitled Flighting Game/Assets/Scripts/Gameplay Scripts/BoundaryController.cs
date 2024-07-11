using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public GameObject topBoundary;
    public GameObject bottomBoundary;
    public GameObject leftBoundary;
    public GameObject rightBoundary;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        PositionBoundaries();
    }

    private void Update()
    {
        // Update the boundaries only if the screen size changes
        if (Screen.width != mainCamera.pixelWidth || Screen.height != mainCamera.pixelHeight)
        {
            PositionBoundaries();
        }
    }

    private void PositionBoundaries()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * screenAspect;

        topBoundary.transform.position = new Vector3(0, mainCamera.orthographicSize, 0);
        bottomBoundary.transform.position = new Vector3(0, -mainCamera.orthographicSize, 0);
        leftBoundary.transform.position = new Vector3(-cameraWidth / 2, 0, 0);
        rightBoundary.transform.position = new Vector3(cameraWidth / 2, 0, 0);

        // Adjust the size of the boundaries to cover the screen edges
        Vector2 horizontalSize = new Vector2(cameraWidth, 1);
        Vector2 verticalSize = new Vector2(1, cameraHeight);

        topBoundary.GetComponent<BoxCollider2D>().size = horizontalSize;
        bottomBoundary.GetComponent<BoxCollider2D>().size = horizontalSize;
        leftBoundary.GetComponent<BoxCollider2D>().size = verticalSize;
        rightBoundary.GetComponent<BoxCollider2D>().size = verticalSize;
    }
}
