using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.EventSystems;

public class Row2SecondSlotScript : MonoBehaviour, IDropHandler
{
    public static bool Row2Ahead = false;
    public static bool Row2Below = false;
    private ElsePlayerController playerController;
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
        if (collision.gameObject.CompareTag("Ahead"))
        {
            Debug.Log("Ahead - TRUE");
            Row2Ahead = true;
        }
        else if (collision.gameObject.CompareTag("W7"))
        {
            Debug.Log("Below - TRUE");
            Row2Below = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ahead"))
        {
            Debug.Log("Ahead - FALSE");
            Row2Ahead = false;
        }
        else if (collision.gameObject.CompareTag("W7"))
        {
            Debug.Log("Below - FALSE");
            Row2Below = true;
            playerController.OnLeftButtonUp();
            playerController.OnRightButtonUp();
        }
    }
}
