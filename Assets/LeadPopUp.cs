using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class LeadPopUp : MonoBehaviour
{

    [SerializeField] public GameObject leadPanel;
    [SerializeField] public GameObject backButton;

    void Start()
    {
        leadPanel.GetComponent<CanvasRenderer>().transform.localScale = Vector2.zero;
    }

    public void Open()
    {
        leadPanel.GetComponent<CanvasRenderer>().transform.LeanScale(Vector2.one, 0.8f);
    }

    public void Close()
    {
        leadPanel.GetComponent<CanvasRenderer>().transform.LeanScale(Vector2.zero, 1f);
    }
}
