using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThirdSlotScript : MonoBehaviour, IDropHandler
{
    private ElsePlayerController playerController;
    public static bool Row1Flip = false;
    public static bool Row1Walk = false;
    public static bool Row1Jump = false;

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
            Row1Flip = true;
            raycast.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba
        }
        //--------------------------------------- WALK ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Walk"))
        {
            Debug.Log("Walk - TRUE");
            Row1Walk = true;
            raycast.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba
        }
        //--------------------------------------- JUMP ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Jump"))
        {
            Debug.Log("Jump - TRUE");
            Row1Jump = true;
            raycast.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //--------------------------------------- FLIP ------------------------------------------------------//
        if (collision.gameObject.CompareTag("Flip"))
        {
            Debug.Log("Flip - FALSE");
            Row1Flip = false;
            raycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba
        }
        //--------------------------------------- WALK ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Walk"))
        {
            Debug.Log("Walk - FALSE");
            Row1Walk = false;
            playerController.OnLeftButtonUp();
            playerController.OnRightButtonUp();
            raycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba
        }
        //--------------------------------------- JUMP ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Jump"))
        {
            Debug.Log("Jump - FALSE");
            Row1Jump = false;
            raycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba
        }
    }
}
