using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Row2SecondSlotScript : MonoBehaviour, IDropHandler
{
    public static bool Row2Ahead = false;
    public static bool Row2Below = false;
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
        if (collision.gameObject.CompareTag("Ahead"))
        {
            Debug.Log("Ahead - TRUE");
            Row2Ahead = true;

            if (sprite.flipX == false) // FACING RIGHT
            {
                aheadRaycastElseIFSLOTS.transform.localPosition = new Vector2(0.574f, 0.775f); // nasa RIGHT
            }
            else if (sprite.flipX == true) // FACING LEFT
            {
                aheadRaycastElseIFSLOTS.transform.localPosition = new Vector2(-0.582f, 0.775f); // nasa LEFT
            }
        }
        else if (collision.gameObject.CompareTag("W7"))
        {
            Debug.Log("Below - TRUE");
            Row2Below = true;
            belowRaycast.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ahead"))
        {
            Debug.Log("Ahead - FALSE");
            Row2Ahead = false;

            aheadRaycastElseIFSLOTS.transform.localPosition = new Vector2(0.016f, 0.775f); // nasa MIDDLE
        }
        else if (collision.gameObject.CompareTag("W7"))
        {
            Debug.Log("Below - FALSE");
            Row2Below = false;
            playerController.OnLeftButtonUp();
            playerController.OnRightButtonUp();
            belowRaycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba
        }
    }
}
