using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Row3ThirdSlotScript : MonoBehaviour, IDropHandler
{
    private ElsePlayerController playerController;
    public static bool Row3Walk;
    public static bool Row3Jump;
    public static bool Row3Flip;
    private void Awake()
    {
        playerController = FindObjectOfType<ElsePlayerController>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Walk"))
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