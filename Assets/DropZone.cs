using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public static bool Row3Walk;
    public static bool Row3Jump;
    public static bool Row3Flip;

    private ElsePlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<ElsePlayerController>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        // Get the object being dragged
        GameObject droppedObject = eventData.pointerDrag;

        if (eventData.pointerDrag != null)
        {
            RectTransform droppedRect = droppedObject.GetComponent<RectTransform>();
            // Snap the dragged object to the drop zone
            droppedRect.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            droppedObject.transform.SetParent(transform);

            // Update the original position in the dragged object's script
            droppedObject.GetComponent<DragAndDrop>().SetOriginalPosition(droppedRect.anchoredPosition, transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Walk"))
        {
            Row3Walk = true;
        }
        if (collision.gameObject.CompareTag("Jump"))
        {
            Row3Jump = true;
        }
        if (collision.gameObject.CompareTag("Flip"))
        {
            Row3Flip = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Walk"))
        {
            Row3Walk = false;
            playerController.OnLeftButtonUp();
            playerController.OnRightButtonUp();
        }
        if (collision.gameObject.CompareTag("Jump"))
        {
            Row3Jump = false;
        }
        if (collision.gameObject.CompareTag("Flip"))
        {
            Row3Flip = false;
        }
    }
}
