using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SecondSlotScript : MonoBehaviour, IDropHandler
{
    public static bool Row1Ahead = false;
    public static bool Row1Below = false;
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
            Row1Ahead = true;
        }
        else if (collision.gameObject.CompareTag("Below"))
        {
            Debug.Log("Below - TRUE");
            Row1Below = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ahead"))
        {
            Debug.Log("Ahead - FALSE");
            Row1Ahead = false;
        }
        else if (collision.gameObject.CompareTag("Below"))
        {
            Debug.Log("Below - FALSE");
            Row1Below = false;
        }
    }
}
