using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrialDragTwo : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //public MouseControllerScript mouseController;
    private RectTransform rectTransform2;
    private CanvasGroup canvasGroup2;

    [SerializeField]
    //private GameObject Gunportal;

    private Vector2 originalPosition;

    private void Start()
    {
        //Gunportal.SetActive(false);
        originalPosition = rectTransform2.anchoredPosition;  // Store the original position
    }

    private void Awake()
    {
        rectTransform2 = GetComponent<RectTransform>();
        canvasGroup2 = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup2.alpha = .6f;
        canvasGroup2.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform2.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup2.alpha = 1f;
        canvasGroup2.blocksRaycasts = true;

        // Check if the object is dropped on a valid drop zone
        if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<TrialDropTWO>() == null)
        {
            // If not dropped on a valid drop zone, snap back to the original position
            rectTransform2.anchoredPosition = originalPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Handle pointer down logic here if needed
    }
}
