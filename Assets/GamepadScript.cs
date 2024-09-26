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
            jumpPanel.anchoredPosition = new Vector2(314, 948.9999f);
            flipPanel.anchoredPosition = new Vector2(474, 949);

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
        LeanTween.moveLocalY(gamePanel, 233.7885f, 0.5f);
    }
}
