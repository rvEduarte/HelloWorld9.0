using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThirdSlotScript : MonoBehaviour, IDropHandler
{
    public static bool Row1Flip = false;
    public static bool Row1Walk = false;
    public static bool Row1Jump = false;
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
        //--------------------------------------- FLIP ------------------------------------------------------//
        if (collision.gameObject.CompareTag("Flip"))
        {
            Debug.Log("Flip - TRUE");
            Row1Flip = true;
        }
        //--------------------------------------- WALK ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Walk"))
        {
            Debug.Log("Walk - TRUE");
            Row1Walk = true;
        }
        //--------------------------------------- JUMP ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Jump"))
        {
            Debug.Log("Jump - TRUE");
            Row1Jump = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //--------------------------------------- FLIP ------------------------------------------------------//
        if (collision.gameObject.CompareTag("Flip"))
        {
            Debug.Log("Flip - FALSE");
            Row1Flip = false;
        }
        //--------------------------------------- WALK ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Walk"))
        {
            Debug.Log("Walk - FALSE");
            Row1Walk = false;
        }
        //--------------------------------------- JUMP ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Jump"))
        {
            Debug.Log("Jump - FALSE");
            Row1Jump = false;
        }
    }
}
