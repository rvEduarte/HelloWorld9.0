using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollow : MonoBehaviour
{
    public Transform aiPos;  // The target position to follow
    [SerializeField] private float moveSpeed = 5f;  // Base movement speed
    [SerializeField] private float smoothTime = 0.3f;  // Time for the AI to reach the target smoothly

    private Vector3 velocity = Vector3.zero;  // Required for SmoothDamp

    private void Update()
    {
        // Smoothly move the AI towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, aiPos.position, ref velocity, smoothTime, moveSpeed);
    }
}
