using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hammer : MonoBehaviour
{
    public Transform animatedObject; // Reference to the animated parent object

    private Vector3 initialPosition;
    private Vector3 offset;

    void Start()
    {
        if (animatedObject == null)
        {
            Debug.LogError("Animated object not assigned.");
            enabled = false; // Disable script if no animatedObject is assigned
            return;
        }

        // Calculate initial offset between the collider and the animated object
        initialPosition = transform.position;
        offset = transform.position - animatedObject.position;
    }

    void Update()
    {
        if (animatedObject != null)
        {
            // Update collider's position based on animated object's position plus offset
            transform.position = animatedObject.position + offset;
        }
    }
}
