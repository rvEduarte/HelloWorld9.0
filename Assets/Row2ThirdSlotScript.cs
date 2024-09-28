using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Row2ThirdSlotScript : MonoBehaviour, IDropHandler
{
    private ElsePlayerController playerController;
    public static bool Row2Flip = false;
    public static bool Row2Walk = false;
    public static bool Row2Jump = false;

    public GameObject raycast;

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
        //--------------------------------------- FLIP ------------------------------------------------------//
        if (collision.gameObject.CompareTag("Flip"))
        {
            Debug.Log("Flip - TRUE");
            Row2Flip = true;
            raycast.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba
        }
        //--------------------------------------- WALK ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Walk"))
        {
            Debug.Log("Walk - TRUE");
            Row2Walk = true;
            raycast.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba
        }
        //--------------------------------------- JUMP ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Jump"))
        {
            Debug.Log("Jump - TRUE");
            Row2Jump = true;
            raycast.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //--------------------------------------- FLIP ------------------------------------------------------//
        if (collision.gameObject.CompareTag("Flip"))
        {
            Debug.Log("Flip - FALSE");
            Row2Flip = false;
            raycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba
        }
        //--------------------------------------- WALK ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Walk"))
        {
            Debug.Log("Walk - FALSE");
            Row2Walk = false;
            playerController.OnLeftButtonUp();
            playerController.OnRightButtonUp();
            raycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba
        }
        //--------------------------------------- JUMP ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Jump"))
        {
            Debug.Log("Jump - FALSE");
            Row2Jump = false;
            raycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba
        }
    }
}
