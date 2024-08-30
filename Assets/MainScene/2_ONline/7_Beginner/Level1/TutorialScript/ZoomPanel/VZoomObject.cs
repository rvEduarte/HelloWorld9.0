using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VZoomObject : MonoBehaviour
{
    public GameObject Jigsaw; // The target object to zoom in on
    public CinemachineVirtualCamera cinemachineCamera; // Reference to the Cinemachine Virtual Camera
    public float zoomSize = 5f; // The size or field of view for zooming in
    public float zoomDuration = 0.5f; // How long it takes to zoom in
    public float zoomStayDuration = 0.5f; // How long the camera stays zoomed in
    public float moveDuration = 0.5f; // Duration for camera to move to the target

    private float originalSize;
    private Transform originalFollowTarget;
    private bool isZooming = false;

    private CinemachineConfiner2D cinemachineConfiner; // Reference to the Cinemachine Confiner component

    private void Start()
    {
        // Store the original size or field of view
        originalSize = cinemachineCamera.m_Lens.OrthographicSize;
        // Store the original Follow target
        originalFollowTarget = cinemachineCamera.Follow;
        // Get the Cinemachine Confiner component
        cinemachineConfiner = cinemachineCamera.GetComponent<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && !isZooming)
        {
            StartCoroutine(ZoomInAndOut());
        }
    }

    private IEnumerator ZoomInAndOut()
    {
        isZooming = true;

        // Temporarily disable the confiner to allow free camera movement
        if (cinemachineConfiner != null)
        {
            cinemachineConfiner.enabled = false;
        }

        // Smoothly move the camera to follow the Jigsaw object
        StartCoroutine(MoveCameraToTarget(Jigsaw.transform.position));

        // Zoom in
        LeanTween.value(cinemachineCamera.m_Lens.OrthographicSize, zoomSize, zoomDuration).setOnUpdate((float val) =>
        {
            cinemachineCamera.m_Lens.OrthographicSize = val;
        });

        // Wait for the camera to stay zoomed in
        yield return new WaitForSeconds(zoomDuration + zoomStayDuration);

        // Zoom out
        LeanTween.value(cinemachineCamera.m_Lens.OrthographicSize, originalSize, zoomDuration).setOnUpdate((float val) =>
        {
            cinemachineCamera.m_Lens.OrthographicSize = val;
        });

        // Reset the camera to follow the original target (the player)
        cinemachineCamera.Follow = originalFollowTarget;

        // Smoothly move the camera back to the original position
        StartCoroutine(MoveCameraToTarget(originalFollowTarget.position));

        // Re-enable the confiner to restore the original boundaries
        if (cinemachineConfiner != null)
        {
            cinemachineConfiner.enabled = true;
        }

        isZooming = false;
    }

    private IEnumerator MoveCameraToTarget(Vector3 targetPosition)
    {
        Vector3 startPosition = cinemachineCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            cinemachineCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cinemachineCamera.transform.position = targetPosition;
    }
}

public class ivan
{
    public GameObject Jigsaw; // The target object to zoom in on
    public CinemachineVirtualCamera cinemachineCamera; // Reference to the Cinemachine Virtual Camera
    public float zoomSize = 5f; // The size or field of view for zooming in
    public float zoomDuration = 0.5f; // How long it takes to zoom in
    public float zoomStayDuration = 0.5f; // How long the camera stays zoomed in

    private float originalSize;
    private Transform originalFollowTarget;
    private Vector3 originalPosition;
    private bool isZooming = false;

    private CinemachineConfiner cinemachineConfiner; // Reference to the Cinemachine Confiner component

    private void Start()
    {
        // Store the original size or field of view
        originalSize = cinemachineCamera.m_Lens.OrthographicSize;
        // Store the original Follow target
        originalFollowTarget = cinemachineCamera.Follow;
        // Store the original position
        originalPosition = cinemachineCamera.transform.position;
        // Get the Cinemachine Confiner component
        cinemachineConfiner = cinemachineCamera.GetComponent<CinemachineConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && !isZooming)
        {
            //StartCoroutine(ZoomInAndOut());
        }
    }

    private IEnumerator ZoomInAndOut()
    {
        isZooming = true;

        // Temporarily disable the confiner to allow free camera movement
        if (cinemachineConfiner != null)
        {
            cinemachineConfiner.enabled = false;
        }

        // Change the camera to follow the Jigsaw object and move to its position
        cinemachineCamera.Follow = null; // Temporarily set Follow to null to manually control position
        Vector3 targetPosition = Jigsaw.transform.position + (cinemachineCamera.transform.position - originalPosition);

        // Move camera to the target position
        LeanTween.move(cinemachineCamera.gameObject, targetPosition, zoomDuration);

        // Zoom in
        LeanTween.value(cinemachineCamera.m_Lens.OrthographicSize, zoomSize, zoomDuration).setOnUpdate((float val) =>
        {
            cinemachineCamera.m_Lens.OrthographicSize = val;
        });

        // Wait for the camera to stay zoomed in
        yield return new WaitForSeconds(zoomDuration + zoomStayDuration);

        // Zoom out and move back to the original position
        LeanTween.value(cinemachineCamera.m_Lens.OrthographicSize, originalSize, zoomDuration).setOnUpdate((float val) =>
        {
            cinemachineCamera.m_Lens.OrthographicSize = val;
        });

        LeanTween.move(cinemachineCamera.gameObject, originalPosition, zoomDuration).setOnComplete(() =>
        {
            // Reset the camera to follow the original target (the player)
            cinemachineCamera.Follow = originalFollowTarget;

            // Re-enable the confiner to restore the original boundaries
            if (cinemachineConfiner != null)
            {
                cinemachineConfiner.enabled = true;
            }

            isZooming = false;
        });
    }
}
