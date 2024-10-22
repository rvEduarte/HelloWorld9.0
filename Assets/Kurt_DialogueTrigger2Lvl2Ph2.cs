using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Kurt_DialogueTrigger2Lvl2Ph2 : MonoBehaviour
{
    public CinemachineVirtualCamera hiddenPassageReveal;  // Unique virtual camera for this trigger
    public KurtMessageLvl2Ph2[] message;
    public KurtActorLvl2Ph2[] actor;

    private bool hasTriggered = false;
    public static bool trigger3Ph2;

    public SpriteRenderer playerSprite;
    public Tilemap tilemapToFade;
    public float fadeDuration = 2f;

    public GameObject collider1;

    // Reference to the specific dialogue manager for this trigger
    public KurtNew_DialogueManagerLvl2Ph2 dialogueManagerForThisTrigger;

    private void Start()
    {
        trigger3Ph2 = false;
        collider1.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            // Camera zoom effect
            hiddenPassageReveal.Priority = 11;
            StartCoroutine(HandleCameraAndTilemap());

            TriggerElevV2.enableElev = false; // disable elev
            collider1.SetActive(false);

            // Disable player movement and jumping during the interaction
            TriggerTutorial.disableMove = false;
            TriggerTutorial.disableJump = true;
        }
    }

    private IEnumerator HandleCameraAndTilemap()
    {
        // Flip player sprite before the camera zooms in
        playerSprite.flipX = true;

        // Ensure player movement is disabled before zoom and tilemap fade
        TriggerTutorial.disableMove = false;
        TriggerTutorial.disableJump = true;

        // Stop the timer when the conversation begins
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = false;

        // Wait for camera zoom effect to finish
        yield return new WaitForSeconds(4f);

        // Lower the camera priority after zooming in
        hiddenPassageReveal.Priority = 0;

        // Start fading in the tilemap
        StartCoroutine(FadeInTilemap());

        // Wait for some time before starting the dialogue
        yield return new WaitForSeconds(2.5f);

        // Now that the camera is zoomed back and the tilemap is fading, start the dialogue
        StartDialogue();

        // Wait for the specific dialogue manager to finish its dialogue
        yield return new WaitUntil(() => dialogueManagerForThisTrigger.IsDialogueFinished());

        // Once the dialogue is finished, allow the player to move again
        TriggerTutorial.disableMove = true;
        TriggerTutorial.disableJump = false;

        // Resume the timer after the interaction is over
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = true;
    }

    public void StartDialogue()
    {
        hasTriggered = true;

        // Ensure the dialogue is started with this specific manager
        dialogueManagerForThisTrigger.OpenDialogue(message, actor);

        // Keep player movement disabled during the dialogue
        TriggerTutorial.disableMove = false;
        TriggerTutorial.disableJump = true;
    }

    private IEnumerator FadeInTilemap()
    {
        float elapsedTime = 0f;
        Color tilemapColor = tilemapToFade.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolate the alpha value from 0 to 1 over time
            float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            // Update the tilemap's color with the new alpha value
            tilemapColor.a = newAlpha;
            tilemapToFade.color = tilemapColor;

            yield return null; // Wait for the next frame
        }

        // Ensure the alpha is set to 0 after the transition
        tilemapColor.a = 0f;
        tilemapToFade.color = tilemapColor;
    }
}
