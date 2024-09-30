using System.Collections;
using System.Collections.Generic;
using TarodevController;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class JigsawPosition : MonoBehaviour
{   
    [Header("GAME OBJECTS")]
    [SerializeField] private RectTransform panelRectTransform;  // Reference to the RectTransform of your UI Panel;
    [SerializeField] private GameObject jigsaw;
    [SerializeField] private float positionGameObject;

    [SerializeField] private float yPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LeanTween.moveY(jigsaw, positionGameObject, 1f);
            StartCoroutine(ShowPanel());
        }
    }
    IEnumerator ShowPanel()
    {
        yield return new WaitForSeconds(1);
        MovePanelToY(yPosition);
    }

    public void MovePanelToY(float newYPosition)
    {
        // Get the current anchored position (X) of the RectTransform
        Vector2 currentPos = panelRectTransform.anchoredPosition;

        // Use LeanTween to move the panel smoothly to the new Y position over 1 second
        LeanTween.value(panelRectTransform.anchoredPosition.y, newYPosition, 1f)
            .setOnUpdate((float value) =>
            {
                // Update only the Y position while keeping the X position the same
                panelRectTransform.anchoredPosition = new Vector2(currentPos.x, value);
            });
    }
}
