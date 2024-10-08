using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_EyeController : MonoBehaviour
{
    public RectTransform eye;       // The eye UI image
    public Camera mainCamera;       // The main camera
    public float maxRotationAngle = 30f;  // Maximum rotation angle for the eye

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            FollowCursor();
        }
    }

    void FollowCursor()
    {
        // Get the mouse position in screen space
        Vector3 mousePos = Input.mousePosition;

        // Convert the screen position to world space (for UI canvas)
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));

        // Calculate the direction from the eye to the mouse position
        Vector3 direction = worldMousePos - eye.position;

        // Normalize the direction and ensure it's in the 2D plane (UI typically works in 2D space)
        direction.Normalize();
        direction.z = 0;  // Make sure rotation stays on the 2D plane

        // Calculate the angle between the eye's forward vector and the direction to the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Clamp the angle to limit the eye's rotation
        angle = Mathf.Clamp(angle, -maxRotationAngle, maxRotationAngle);

        // Rotate the eye based on the calculated angle
        eye.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
