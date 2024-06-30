using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrialAim : MonoBehaviour
{
    [SerializeField] private GameObject gun;

    private Vector2 worldPosition2;
    private Vector2 direction2;
    private float angle2;

    private void Update()
    {
        handleGunRotation();
    }

    private void handleGunRotation()
    {
        //rotate the gun according to mouse movement
        worldPosition2 = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction2 = (worldPosition2 - (Vector2)gun.transform.position.normalized);
        gun.transform.right = direction2;

        //flid the gun when it reaches 90 degree
        angle2 = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;

        Vector3 localScale = new Vector3(1f, 1f, 1f);

        if (angle2 > 90 || angle2 < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }
        gun.transform.localScale = localScale;
    }
}
