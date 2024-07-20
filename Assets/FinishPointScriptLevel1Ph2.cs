  using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishPointScriptLevel1Ph2 : MonoBehaviour
{
    [SerializeField] public GameObject gameCompletion;

    public RunningTimerLevel1Ph2 timer;

    public TextMeshProUGUI exerciseAccuracy;

    int accuracy;
    float accuracyPercentage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameCompletion.SetActive(true);

            timer.isPicked = true;
            accuracy = PlayerPrefs.GetInt("accuracyBeginnerLevel1Ph2");
            CalculateAndDisplayAccuracy();
        }
    }

    private void CalculateAndDisplayAccuracy()
    {
        accuracyPercentage = Mathf.Max(100f - (accuracy - 1) * 10f, 0f);
        exerciseAccuracy.text = $"{accuracyPercentage}%";
    }
}
