using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_CharComputer : MonoBehaviour
{
    public GameObject interactionText;          // The text that shows "Press E to interact with the computer"
    public GameObject panel;                    // The panel that will be shown and hidden
    public float transitionDuration = 0.5f;     // Duration of the panel transition
    public Texture2D customCursorTexture;       // Custom cursor texture when the panel is opened
    public Vector2 cursorHotspot = Vector2.zero; // The hotspot point of the custom cursor

    private bool isPlayerInRange = false;
    private bool isPanelVisible = false;
    private CanvasGroup panelCanvasGroup;       // For smooth transition effect
    private bool handleEscapeForPanel = false;  // Flag to handle Escape key specifically for the panel 
    public static bool disableE;

    private void Start()
    {

        disableE = true;

        // Ensure the panel has a CanvasGroup component for fading
        panelCanvasGroup = panel.GetComponent<CanvasGroup>();
        if (panelCanvasGroup == null)
        {
            panelCanvasGroup = panel.AddComponent<CanvasGroup>();
        }

        // Set initial states
        interactionText.SetActive(false);
        panelCanvasGroup.alpha = 0;
        panelCanvasGroup.interactable = false;
        panelCanvasGroup.blocksRaycasts = false;  // Ensure it doesn't block interactions

        // Set the default cursor initially
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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

        TriggerTutorial.disableMove = true;  // Enable Move
        TriggerTutorial.disableJump = false; // Enable jumping
    }

    public void CloseJigsawPanel()
    {
        TriggerTutorial.disableMove = true;  // Enable Move
        TriggerTutorial.disableJump = false; // Enable jumping
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
        // Disable player movement
        TriggerTutorial.disableMove = false;
        TriggerTutorial.disableJump = true;

        isPanelVisible = true;
        handleEscapeForPanel = true;  // Enable the Escape key specifically for this panel
        interactionText.SetActive(false);

        // Activate the panel so it can be shown again
        panel.SetActive(true);

        // Immediately scale and fade in the panel
        LeanTween.scale(panel, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);

        StopAllCoroutines();
        StartCoroutine(FadePanel(1)); // Fade in the panel

        // Enable interaction and block raycasts when the panel is visible
        panelCanvasGroup.interactable = true;
        panelCanvasGroup.blocksRaycasts = true;

        // Change the cursor to the custom texture when the panel is opened
        if (customCursorTexture != null)
        {
            Cursor.SetCursor(customCursorTexture, cursorHotspot, CursorMode.Auto);
        }
    }

    private void HidePanel()
    {
        isPanelVisible = false;
        handleEscapeForPanel = false;  // Disable the Escape key handling for this panel
        interactionText.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(FadePanel(0));  // Fade out the panel

        // Disable interaction and raycasting when the panel is hidden
        panelCanvasGroup.interactable = false;
        panelCanvasGroup.blocksRaycasts = false;

        // Reset the cursor to the default when the panel is closed
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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
            // This ensures that after hiding, it can be shown again
            panel.SetActive(false);
        }
    }
}
