using System.Collections;
using System.Collections.Generic;
using TarodevController;
using Unity.VisualScripting;
using UnityEngine;

public class GamepadScript : MonoBehaviour
{
    [Header("CONTROLLERS")]
    public ElsePlayerController elseController;
    public PlayerController playerController;
    public ElsePlayerAnimator elsePlayerAnimator;
    public PlayerAnimator playerAnimator;

    [Header("GAME OBJECTS")]
    public GameObject gamepad;
    public GameObject gamePanel;

    public RectTransform jumpPanel;
    public RectTransform flipPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {


            LeanTween.moveY(gamepad, 17f, 1f);
            StartCoroutine(ShowPanel());
            //Disable the controller
            playerController.enabled = false;
            playerAnimator.enabled = false;

            //Enable elseController;
            elseController.enabled = true;
            elsePlayerAnimator.enabled = true;


        }
    }

    IEnumerator ShowPanel()
    {
        yield return new WaitForSeconds(1);
        LeanTween.moveLocalY(gamePanel, -307, 0.5f);
        MovePanelToY(929);
    }

    public void MovePanelToY(float newYPosition)
    {
        // Get the current anchored position (X and Y) of the RectTransform
        Vector2 currentPos = jumpPanel.anchoredPosition;

        Vector2 currentPos1 = flipPanel.anchoredPosition;

        // Change only the Y position
        jumpPanel.anchoredPosition = new Vector2(currentPos.x, newYPosition);
        flipPanel.anchoredPosition = new Vector2(currentPos1.x, newYPosition);
    }
}
