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

        if (droppedObject != null)
        {
            // Check if this drop zone already has a child
            if (transform.childCount > 0)
            {
                // Get the current object in the drop zone and swap positions
                GameObject objectInDropZone = transform.GetChild(0).gameObject;

                // Swap positions
                RectTransform droppedRect = droppedObject.GetComponent<RectTransform>();
                RectTransform inDropZoneRect = objectInDropZone.GetComponent<RectTransform>();

                Vector2 tempPosition = inDropZoneRect.anchoredPosition;
                Transform tempParent = objectInDropZone.transform.parent;

                inDropZoneRect.anchoredPosition = droppedRect.anchoredPosition;
                objectInDropZone.transform.SetParent(droppedObject.transform.parent);

                droppedRect.anchoredPosition = tempPosition;
                droppedObject.transform.SetParent(tempParent);

                // Update original positions in the drag scripts to handle reset correctly
                droppedObject.GetComponent<DragAndDrop>().SetOriginalPosition(droppedRect.anchoredPosition, tempParent);
                objectInDropZone.GetComponent<DragAndDrop>().SetOriginalPosition(inDropZoneRect.anchoredPosition, droppedObject.transform.parent);
            }

            // Set the new object in the drop zone
            droppedObject.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            droppedObject.transform.SetParent(transform);
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
