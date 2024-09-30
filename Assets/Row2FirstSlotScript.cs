using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.EventSystems;

public class Row2FirstSlotScript : MonoBehaviour, IDropHandler
{
    public static bool Row2Wall = false;
    public static bool Row2Empty = false;
    private ElsePlayerController playerController;
    [SerializeField] private SpriteRenderer sprite;

    public GameObject belowRaycast;
    public GameObject aheadRaycastElseIFSLOTS;
    public GameObject emptyRaycastIFSLOTS;
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
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Wall - TRUE");
            Row2Wall = true;
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
        else if (collision.gameObject.CompareTag("Empty"))
        {
            Debug.Log("Empty - TRUE");
            Row2Empty = true;

            if (sprite.flipX == false) // FACING RIGHT
            {
                emptyRaycastIFSLOTS.transform.localPosition = new Vector2(0.494f, 0.687f); // nasa RIGHT   X   Y
            }
            else if (sprite.flipX == true) // FACING LEFT
            {
                emptyRaycastIFSLOTS.transform.localPosition = new Vector2(-0.488f, 0.687f); // nasa LEFT   X   Y
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Wall - FALSE");
            Row2Wall = false;
            playerController.OnLeftButtonUp();
            playerController.OnRightButtonUp();
            belowRaycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba
            aheadRaycastElseIFSLOTS.transform.localPosition = new Vector2(0.016f, 0.775f); // nasa MIDDLE
        }
        else if (collision.gameObject.CompareTag("Empty"))
        {
            Debug.Log("Empty - FALSE");
            Row2Empty = false;
        }
    }
}
