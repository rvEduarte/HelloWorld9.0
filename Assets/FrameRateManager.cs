using System.Collections;
using System.Threading;
using UnityEngine;
public class FrameRateManager : MonoBehaviour
{
    [Header("Frame Settings")]
    [SerializeField]public float TargetFrameRate = 60.0f;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0; // Disabling VSync to control frame rate manually.
        Application.targetFrameRate = (int)TargetFrameRate;
    }
}
