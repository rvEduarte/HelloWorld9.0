using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtComputer : MonoBehaviour
{
    public GameObject interactionText;          // The text that shows "Press E to interact with the computer"
    public GameObject panel;                    // The panel that will be shown and hidden
    public float transitionDuration = 0.5f;     // Duration of the panel transition
    public MonoBehaviour PlayerController;      // Reference to the player's movement script

    private bool isPlayerInRange = false;
    private bool isPanelVisible = false;
    private CanvasGroup panelCanvasGroup;       // For smooth transition effect
    private bool handleEscapeForPanel = false;  // Flag to handle Escape key specifically for the panel 
    private bool disableE = true;

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
        if (!disableE) return;

        // Check if the player is in range and presses the E key to show the panel
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowPanel();
            disableE = false;
        }
    }

    public void EscPanel()
    {
        HidePanel();
        disableE = true;
    }

    public void CloseJigsawPanel()
    {
        PlayerController.enabled = true;
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
        }
    }

    private void ShowPanel()
    {
        isPanelVisible = true;
        handleEscapeForPanel = true;  // Enable the Escape key specifically for this panel
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
        handleEscapeForPanel = false;  // Disable the Escape key handling for this panel
        interactionText.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadePanel(0)); // Fade out the panel

        // Enable player movement
        if (PlayerController != null)
        {
            PlayerController.enabled = true;
        }
    }

    private IEnumerator FadePanel(float targetAlpha)
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
