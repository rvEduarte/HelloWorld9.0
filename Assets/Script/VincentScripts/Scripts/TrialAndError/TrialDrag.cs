using UnityEngine;
using UnityEngine.EventSystems;

public class TrialDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform2;
    private CanvasGroup canvasGroup2;

    [SerializeField]
    private GameObject Gunportal;

    private void Start()
    {

        Gunportal.SetActive(false);
    }

    private void Awake()
    {
        rectTransform2 = GetComponent<RectTransform>();
        canvasGroup2 = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
       // Debug.Log("OnBeginDrag");
        canvasGroup2.alpha = .6f;
        canvasGroup2.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
       // Debug.Log("OnDrag");
        rectTransform2.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        canvasGroup2.alpha = 1f;
        canvasGroup2.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{

        
   // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("ENTERWORLD");
        //if the game object we collided with is not equal to the current game object (so anything other than itself)
        if (collision.gameObject.tag == "Player")
        {
            //turn dragging for this object off because we hit something
            Gunportal.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Gunportal.gameObject.SetActive(false);
        }
    }

}
