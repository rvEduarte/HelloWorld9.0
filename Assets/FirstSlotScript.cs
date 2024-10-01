using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstSlotScript : MonoBehaviour, IDropHandler
{
    public static bool Row1Wall = false;
    public static bool Row1Empty = false;
    public static bool Row1Spike = false;
    private ElsePlayerController playerController;
    [SerializeField] private SpriteRenderer sprite;

    public GameObject belowRaycast;
    public GameObject aheadRaycastIFSLOTS;
    public GameObject emptyOntrigger; 
    public GameObject spikeOntriggerBelow;
    public GameObject spikeOntriggerAhead;
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
            Row1Wall = true;
            belowRaycast.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba

            if(sprite.flipX == false) // FACING RIGHT
            {
                aheadRaycastIFSLOTS.transform.localPosition = new Vector2(0.574f, 0.775f); // nasa RIGHT
            }
            else if (sprite.flipX == true) // FACING LEFT
            {
                aheadRaycastIFSLOTS.transform.localPosition = new Vector2(-0.582f, 0.775f); // nasa LEFT
            }
        }
        else if (collision.gameObject.CompareTag("Empty"))
        {
            Debug.Log("Empty - TRUE");
            Row1Empty = true;

            if (sprite.flipX == false) // FACING RIGHT
            {
                emptyOntrigger.transform.localPosition = new Vector2(0.494f, 0.687f); // nasa RIGHT   X   Y
            }
            else if (sprite.flipX == true) // FACING LEFT
            {
                emptyOntrigger.transform.localPosition = new Vector2(-0.488f, 0.687f); // nasa LEFT   X   Y
            }
        }
        else if (collision.gameObject.CompareTag("Spike"))
        {
            Debug.Log("Spike - TRUE");
            Row1Spike = true;

            if (sprite.flipX == false) // FACING RIGHT
            {
                spikeOntriggerBelow.transform.localPosition = new Vector2(0.579f, -0.502f); // nasa RIGHT   X   Y
                spikeOntriggerAhead.transform.localPosition = new Vector2(0.579f, 0); // nasa RIGHT   X   Y
            }
            else if (sprite.flipX == true) // FACING LEFT
            {
                spikeOntriggerBelow.transform.localPosition = new Vector2(-0.611f, -0.502f); // nasa LEFT   X   Y
                spikeOntriggerAhead.transform.localPosition = new Vector2(-0.611f, 0); // nasa LEFT   X   Y
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Wall - FALSE");
            Row1Wall = false;
            playerController.OnLeftButtonUp();
            playerController.OnRightButtonUp();
            belowRaycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba
            
            aheadRaycastIFSLOTS.transform.localPosition = new Vector2(0.016f, 0.775f); // nasa MIDDLE
        }
        else if (collision.gameObject.CompareTag("Empty"))
        {
            Debug.Log("Empty - FALSE");
            Row1Empty = false;
        }
        else if (collision.gameObject.CompareTag("Spike"))
        {
            Debug.Log("Spike - FALSE");
            Row1Spike = false;
        }
    }
}
