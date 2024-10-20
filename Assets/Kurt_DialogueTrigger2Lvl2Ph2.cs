using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Kurt_DialogueTrigger2Lvl2Ph2 : MonoBehaviour
{
    public CinemachineVirtualCamera hiddenPassageReveal;
    public KurtMessageLvl2Ph2[] message;
    public KurtActorLvl2Ph2[] actor;

    private bool hasTriggered = false;
    public static bool trigger3Ph2;

    public SpriteRenderer playerSprite;
    public Tilemap tilemapToFade;
    public float fadeDuration = 2f;

    public GameObject collider1;

    private void Start()
    {
        trigger3Ph2 = false;
        collider1.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            // Camera gets priority for zoom effect
            hiddenPassageReveal.Priority = 11;
            StartCoroutine(HandleCameraAndTilemap());

            collider1.SetActive(false);

            // Disable player movement and jumping
            TriggerTutorial.disableMove = false;
            TriggerTutorial.disableJump = true;
        }
    }

    private IEnumerator HandleCameraAndTilemap()
    {
        // Flip player sprite before the camera zooms in
        playerSprite.flipX = true;

        // Ensure the player remains disabled and the dialogue is hidden initially
        TriggerTutorial.disableMove = false;
        TriggerTutorial.disableJump = true;

        // Stop the timer when the conversation begins
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = false;

        // Wait for camera zoom effect to finish (keep the camera priority for some time)
        yield return new WaitForSeconds(4f);

        // Lower the priority of the camera after zooming in
        hiddenPassageReveal.Priority = 0;

        // Start fading in the tilemap while keeping player disabled
        StartCoroutine(FadeInTilemap());

        // Wait for some time (camera transition and tilemap fade)
        yield return new WaitForSeconds(2.5f);

        // Now that the camera is back and tilemap is fading, start the dialogue
        StartDialogue();

        // Wait for the dialogue to finish
        yield return new WaitUntil(() => FindObjectOfType<KurtNew_DialogueManagerLvl2Ph2>().IsDialogueFinished());

        // After the dialogue finishes, allow the player to move again
        TriggerTutorial.disableMove = true;
        TriggerTutorial.disableJump = false;

        // Resume the timer
        RunningTimerLevel2Ph2.timerStopLevel2Ph2 = true;
    }

    public void StartDialogue()
    {
        hasTriggered = true;

        // Start the dialogue after camera transition and tilemap fade
        FindObjectOfType<KurtNew_DialogueManagerLvl2Ph2>().OpenDialogue(message, actor);

        // Keep the player's movement disabled during the dialogue
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
