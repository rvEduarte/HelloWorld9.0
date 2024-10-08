using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_CameraZoom : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the main camera
    public float zoomDuration = 1.5f;  // Duration of the zoom effect
    public float zoomSize = 3f;  // Desired zoom size for the camera

    private float originalSize;  // Store the original camera size
    private Vector3 originalPosition;  // Store the original camera position

    public void StartZoom(Vector3 targetPosition)
    {
        // Store the original camera state
        originalSize = mainCamera.orthographicSize;
        originalPosition = mainCamera.transform.position;

        // Start the zoom coroutine
        StartCoroutine(ZoomToPosition(targetPosition));
    }

    private IEnumerator ZoomToPosition(Vector3 targetPosition)
    {
        // Zoom the camera to the target position
        LeanTween.move(mainCamera.gameObject, targetPosition, zoomDuration).setEaseInOutQuad();
        LeanTween.value(mainCamera.gameObject, originalSize, zoomSize, zoomDuration).setOnUpdate((float value) => {
            mainCamera.orthographicSize = value;
        });

        // Wait for the zoom to finish
        yield return new WaitForSeconds(zoomDuration);

        // Optionally reset the camera back to original position/size if needed
        // Uncomment the following lines if you want to reset after zooming
        // LeanTween.move(mainCamera.gameObject, originalPosition, zoomDuration).setEaseInOutQuad();
        // LeanTween.value(mainCamera.gameObject, zoomSize, originalSize, zoomDuration).setOnUpdate((float value) => {
        //     mainCamera.orthographicSize = value;
        // });
    }
}
