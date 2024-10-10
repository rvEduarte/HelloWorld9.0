using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RvComputer : MonoBehaviour
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

        // Set the default cursor initially (if desired)
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
    }

    public void CloseJigsawPanel()
    {
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
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

        // Change the cursor to the custom texture when the panel is opened
        if (customCursorTexture != null)
        {
            Cursor.SetCursor(customCursorTexture, cursorHotspot, CursorMode.Auto);
        }

        TriggerTutorial.disableMove = false; //disable Move
        TriggerTutorial.disableJump = true; //disable jumping
    }

    private void HidePanel()
    {
        isPanelVisible = false;
        handleEscapeForPanel = false;  // Disable the Escape key handling for this panel
        interactionText.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadePanel(0)); // Fade out the panel

        // Reset the cursor to the default when the panel is closed
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
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
