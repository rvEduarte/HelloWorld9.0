using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopUp : MonoBehaviour
{

    public GameObject Template;
    public GameObject backButton;

    void Start()
    {
        Template.GetComponent<CanvasRenderer>().transform.localScale = Vector2.zero;
    }

    public void Open()
    {
        Template.GetComponent<CanvasRenderer>().transform.LeanScale(Vector2.one, 1f);
    }

    public void Close()
    {
        Template.GetComponent<CanvasRenderer>().transform.LeanScale(Vector2.zero, 1f);
    }

}
