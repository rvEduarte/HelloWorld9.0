using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtComputer : MonoBehaviour
{
    public GameObject interactionText; // The text that shows "Press E to interact with the computer"
    public GameObject panel;           // The panel that will be shown and hidden
    public float transitionDuration = 0.5f; // Duration of the panel transition
    public MonoBehaviour PlayerController; // Reference to the player's movement script

    private bool isPlayerInRange = false;
    private bool isPanelVisible = false;
    private CanvasGroup panelCanvasGroup; // For smooth transition effect

    private void Start()
    {
        // Ensure the panel has a CanvasGroup component for fading
        panelCanvasGroup = panel.GetComponent<CanvasGroup>();
        if (panelCanvasGroup == null)
        {
            panelCanvasGroup = panel.AddComponent<CanvasGroup>();
        }

        // Set initial states
        interactionText.SetActive(false);
        panel.SetActive(false);
        panelCanvasGroup.alpha = 0;
    }

    private void Update()
    {
        // Check if the player is in range and presses the E key
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isPanelVisible)
            {
                HidePanel();
            }
            else
            {
                ShowPanel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the collider
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exits the collider
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionText.SetActive(false);

            // Hide the panel if the player leaves the area
            if (isPanelVisible)
            {
                HidePanel();
            }
        }
    }

    private void ShowPanel()
    {
        isPanelVisible = true;
        interactionText.SetActive(false);
        panel.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadePanel(1)); // Fade in the panel

        // Disable player movement
        if (PlayerController != null)
        {
            PlayerController.enabled = false;
        }
    }

    private void HidePanel()
    {
        isPanelVisible = false;
        interactionText.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadePanel(0)); // Fade out the panel

        // Enable player movement
        if (PlayerController != null)
        {
            PlayerController.enabled = true;
        }
    }

    private System.Collections.IEnumerator FadePanel(float targetAlpha)
    {
        float startAlpha = panelCanvasGroup.alpha;
        float time = 0;

        while (time < transitionDuration)
        {
            time += Time.deltaTime;
            panelCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / transitionDuration);
            yield return null;
        }

        panelCanvasGroup.alpha = targetAlpha;

        // Deactivate the panel if fully hidden
        if (targetAlpha == 0)
        {
            panel.SetActive(false);
        }
    }
}
