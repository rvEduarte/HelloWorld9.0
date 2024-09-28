using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Row2ThirdSlotScript : MonoBehaviour, IDropHandler
{
    public static bool Row2Flip = false;
    public static bool Row2Walk = false;
    public static bool Row2Jump = false;
    private ElsePlayerController playerController;
    [SerializeField] private SpriteRenderer sprite;

    public GameObject belowRaycast;
    public GameObject aheadRaycastElseIFSLOTS;

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
            belowRaycast.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba

            if (sprite.flipX == false) // FACING RIGHT
            {
                aheadRaycastElseIFSLOTS.transform.localPosition = new Vector2(0.574f, 0.775f); // nasa RIGHT
            }
            else if (sprite.flipX == true) // FACING LEFT
            {
                aheadRaycastElseIFSLOTS.transform.localPosition = new Vector2(-0.582f, 0.775f); // nasa LEFT
            }
        }
        //--------------------------------------- WALK ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Walk"))
        {
            Debug.Log("Walk - TRUE");
            Row2Walk = true;
            belowRaycast.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba

            if (sprite.flipX == false) // FACING RIGHT
            {
                aheadRaycastElseIFSLOTS.transform.localPosition = new Vector2(0.574f, 0.775f); // nasa RIGHT
            }
            else if (sprite.flipX == true) // FACING LEFT
            {
                aheadRaycastElseIFSLOTS.transform.localPosition = new Vector2(-0.582f, 0.775f); // nasa LEFT
            }
        }
        //--------------------------------------- JUMP ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Jump"))
        {
            Debug.Log("Jump - TRUE");
            Row2Jump = true;
            belowRaycast.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba

            if (sprite.flipX == false) // FACING RIGHT
            {
                aheadRaycastElseIFSLOTS.transform.localPosition = new Vector2(0.574f, 0.775f); // nasa RIGHT
            }
            else if (sprite.flipX == true) // FACING LEFT
            {
                aheadRaycastElseIFSLOTS.transform.localPosition = new Vector2(-0.582f, 0.775f); // nasa LEFT
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //--------------------------------------- FLIP ------------------------------------------------------//
        if (collision.gameObject.CompareTag("Flip"))
        {
            Debug.Log("Flip - FALSE");
            Row2Flip = false;
            belowRaycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba

            aheadRaycastElseIFSLOTS.transform.localPosition = new Vector2(0.016f, 0.775f); // nasa MIDDLE
        }
        //--------------------------------------- WALK ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Walk"))
        {
            Debug.Log("Walk - FALSE");
            Row2Walk = false;
            playerController.OnLeftButtonUp();
            playerController.OnRightButtonUp();
            belowRaycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba

            aheadRaycastElseIFSLOTS.transform.localPosition = new Vector2(0.016f, 0.775f); // nasa MIDDLE
        }
        //--------------------------------------- JUMP ------------------------------------------------------//
        else if (collision.gameObject.CompareTag("Jump"))
        {
            Debug.Log("Jump - FALSE");
            Row2Jump = false;
            belowRaycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba

            aheadRaycastElseIFSLOTS.transform.localPosition = new Vector2(0.016f, 0.775f); // nasa MIDDLE
        }
    }
}
