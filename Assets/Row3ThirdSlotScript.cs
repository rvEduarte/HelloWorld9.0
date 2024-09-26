using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Row3ThirdSlotScript : MonoBehaviour, IDropHandler
{
    private ElsePlayerController playerController;
    public static bool Row3Walk;
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
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Walk"))
        {
            Row3Walk = false;
            playerController.OnLeftButtonUp();
            playerController.OnRightButtonUp();
        }
    }
}
