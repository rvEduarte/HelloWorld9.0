using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Row2ThirdSlotScript : MonoBehaviour, IDropHandler
{
    public static bool Row2Flip = false;
    public static bool Row2Walk = false;
    public static bool Row2Jump = false;
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
            Row2Flip = true;
        }
        //--------------------------------------- WALK ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Walk"))
        {
            Debug.Log("Walk - TRUE");
            Row2Walk = true;
        }
        //--------------------------------------- JUMP ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Jump"))
        {
            Debug.Log("Jump - TRUE");
            Row2Jump = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //--------------------------------------- FLIP ------------------------------------------------------//
        if (collision.gameObject.CompareTag("Flip"))
        {
            Debug.Log("Flip - FALSE");
            Row2Flip = false;
        }
        //--------------------------------------- WALK ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Walk"))
        {
            Debug.Log("Walk - FALSE");
            Row2Walk = false;
        }
        //--------------------------------------- JUMP ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Jump"))
        {
            Debug.Log("Jump - FALSE");
            Row2Jump = false;
        }
    }
}
