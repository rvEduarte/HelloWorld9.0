using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockAnimator : MonoBehaviour
{
    [SerializeField] private Pause pauseMenu;

    private Transform clockHandTransform;

    private void Awake()
    {
        clockHandTransform = transform.Find("clock hand");
    }

    private void Update()
    {
        clockHandTransform.eulerAngles = new Vector3(0, 0, -Time.realtimeSinceStartup * 90f);
    }
}
