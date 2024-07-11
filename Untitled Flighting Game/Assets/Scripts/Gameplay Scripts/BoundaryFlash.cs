using UnityEngine;
using System.Collections;  

public class BoundaryFlash : MonoBehaviour
{
    public Material defaultMaterial;  
    public Material flashMaterial;   
    public float flashDuration = 0.5f; 

    private Renderer boundaryRenderer;

    private void Start()
    {
        // Cache the Renderer component
        boundaryRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player1") || collider.CompareTag("Player2"))
        {
            StartCoroutine(FlashBoundary());
        }
    }

    private IEnumerator FlashBoundary()
    {
        // Change the material to the flash material
        boundaryRenderer.material = flashMaterial;

        // Wait for the duration of the flash
        yield return new WaitForSeconds(flashDuration);

        // Change back to the default material
        boundaryRenderer.material = defaultMaterial;
    }
}
