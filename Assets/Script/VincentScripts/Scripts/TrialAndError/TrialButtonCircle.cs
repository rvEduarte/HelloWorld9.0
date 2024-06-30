using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class TrialButtonCircle : MonoBehaviour
{
    public Button yourButton;
    public CanvasRenderer yourDraggable1;
    public CanvasRenderer yourDraggable2;
    public CanvasRenderer yourDraggable3;
    public CanvasRenderer yourDraggable4;
    public CanvasRenderer yourDrop1;

    public CanvasRenderer yourCanvas;
    public CanvasRenderer yourString;
    //public CanvasRenderer yourX;

    //public CanvasRenderer yourText1;
    public CanvasRenderer yourText2;
    public CanvasRenderer yourText3;
    public CanvasRenderer yourText4;
    public CanvasRenderer yourText5;
    public CanvasRenderer yourText6;

    public CanvasRenderer yourPanel;
    public CanvasRenderer yourHint;

    public bool clicked = true;
   // public bool condition;

    public CanvasRenderer rightFire;

    public CanvasRenderer backgroundBorder;
    public CanvasRenderer hintText;
    public CanvasRenderer hintText2;
    public CanvasRenderer bgLeftRightButton;
    public CanvasRenderer rightButton;
    public CanvasRenderer leftButton;
    public CanvasRenderer bgTransparentButton;
    public CanvasRenderer redButton;
    public CanvasRenderer greentButton;
    public CanvasRenderer startText;
    public CanvasRenderer endText;

    void Start()
    {

        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {

        if (clicked == true)
        {
            //Debug.Log("HIDE");
            CanvasRenderer canvasRenderer1 = yourDraggable1.GetComponent<CanvasRenderer>();
            canvasRenderer1.cull = true;

            CanvasRenderer canvasRenderer2 = yourDraggable2.GetComponent<CanvasRenderer>();
            canvasRenderer2.cull = true;

            CanvasRenderer canvasRenderer3 = yourDraggable3.GetComponent<CanvasRenderer>();
            canvasRenderer3.cull = true;

            CanvasRenderer canvasRenderer4 = yourDraggable4.GetComponent<CanvasRenderer>();
            canvasRenderer4.cull = true;

            CanvasRenderer canvasRenderer5 = yourDrop1.GetComponent<CanvasRenderer>();
            canvasRenderer5.cull = true;

            CanvasRenderer canvasRenderer7 = yourCanvas.GetComponent<CanvasRenderer>();
            canvasRenderer7.cull = true;

            CanvasRenderer canvasRenderer8 = yourString.GetComponent<CanvasRenderer>();
            canvasRenderer8.cull = true;

            CanvasRenderer canvasRenderer10 = yourText2.GetComponent<CanvasRenderer>();
            canvasRenderer10.cull = true;

            CanvasRenderer canvasRenderer11 = yourText3.GetComponent<CanvasRenderer>();
            canvasRenderer11.cull = true;

            CanvasRenderer canvasRenderer12 = yourText4.GetComponent<CanvasRenderer>();
            canvasRenderer12.cull = true;

            CanvasRenderer canvasRenderer13 = yourText5.GetComponent<CanvasRenderer>();
            canvasRenderer13.cull = true;

            CanvasRenderer canvasRenderer14 = yourText6.GetComponent<CanvasRenderer>();
            canvasRenderer14.cull = true;

            CanvasRenderer canvasRenderer15 = yourPanel.GetComponent<CanvasRenderer>();
            canvasRenderer15.cull = true;

            CanvasRenderer canvasRenderer16 = yourHint.GetComponent<CanvasRenderer>();
            canvasRenderer16.cull = true;

            CanvasRenderer canvasRenderer17 = rightFire.GetComponent<CanvasRenderer>();
            canvasRenderer17.cull = true;

            CanvasRenderer canvasRenderer18 = backgroundBorder.GetComponent<CanvasRenderer>();
            canvasRenderer18.cull = true;

            CanvasRenderer canvasRenderer19 = hintText.GetComponent<CanvasRenderer>();
            canvasRenderer19.cull = true;

            CanvasRenderer canvasRenderer20 = hintText2.GetComponent<CanvasRenderer>();
            canvasRenderer20.cull = true;

            CanvasRenderer canvasRenderer21 = bgLeftRightButton.GetComponent<CanvasRenderer>();
            canvasRenderer21.cull = true;

            CanvasRenderer canvasRenderer22 = rightButton.GetComponent<CanvasRenderer>();
            canvasRenderer22.cull = true;

            CanvasRenderer canvasRenderer23 = leftButton.GetComponent<CanvasRenderer>();
            canvasRenderer23.cull = true;

            CanvasRenderer canvasRenderer24 = bgTransparentButton.GetComponent<CanvasRenderer>();
            canvasRenderer24.cull = true;

            CanvasRenderer canvasRenderer25 = redButton.GetComponent<CanvasRenderer>();
            canvasRenderer25.cull = true;

            CanvasRenderer canvasRenderer26 = greentButton.GetComponent<CanvasRenderer>();
            canvasRenderer26.cull = true;

            CanvasRenderer canvasRenderer27 = startText.GetComponent<CanvasRenderer>();
            canvasRenderer27.cull = true;

            CanvasRenderer canvasRenderer28 = endText.GetComponent<CanvasRenderer>();
            canvasRenderer28.cull = true;

        }
        else if (clicked == false)
        {
            //SHOW
            CanvasRenderer canvasRenderer1 = yourDraggable1.GetComponent<CanvasRenderer>();
            canvasRenderer1.cull = false;

            CanvasRenderer canvasRenderer2 = yourDraggable2.GetComponent<CanvasRenderer>();
            canvasRenderer2.cull = false;

            CanvasRenderer canvasRenderer3 = yourDraggable3.GetComponent<CanvasRenderer>();
            canvasRenderer3.cull = false;

            CanvasRenderer canvasRenderer4 = yourDraggable4.GetComponent<CanvasRenderer>();
            canvasRenderer4.cull = false;

            CanvasRenderer canvasRenderer5 = yourDrop1.GetComponent<CanvasRenderer>();
            canvasRenderer5.cull = false;

            CanvasRenderer canvasRenderer7 = yourCanvas.GetComponent<CanvasRenderer>();
            canvasRenderer7.cull = false;

            CanvasRenderer canvasRenderer8 = yourString.GetComponent<CanvasRenderer>();
            canvasRenderer8.cull = false;

            CanvasRenderer canvasRenderer10 = yourText2.GetComponent<CanvasRenderer>();
            canvasRenderer10.cull = false;

            CanvasRenderer canvasRenderer11 = yourText3.GetComponent<CanvasRenderer>();
            canvasRenderer11.cull = false;

            CanvasRenderer canvasRenderer12 = yourText4.GetComponent<CanvasRenderer>();
            canvasRenderer12.cull = false;

            CanvasRenderer canvasRenderer13 = yourText5.GetComponent<CanvasRenderer>();
            canvasRenderer13.cull = false;

            CanvasRenderer canvasRenderer14 = yourText6.GetComponent<CanvasRenderer>();
            canvasRenderer14.cull = false;

            CanvasRenderer canvasRenderer15 = yourPanel.GetComponent<CanvasRenderer>();
            canvasRenderer15.cull = false;

            CanvasRenderer canvasRenderer16 = yourHint.GetComponent<CanvasRenderer>();
            canvasRenderer16.cull = false;

            CanvasRenderer canvasRenderer17 = rightFire.GetComponent<CanvasRenderer>();
            canvasRenderer17.cull = false;

            CanvasRenderer canvasRenderer18 = backgroundBorder.GetComponent<CanvasRenderer>();
            canvasRenderer18.cull = false;

            CanvasRenderer canvasRenderer19 = hintText.GetComponent<CanvasRenderer>();
            canvasRenderer19.cull = false;

            CanvasRenderer canvasRenderer20 = hintText2.GetComponent<CanvasRenderer>();
            canvasRenderer20.cull = false;

            CanvasRenderer canvasRenderer21 = bgLeftRightButton.GetComponent<CanvasRenderer>();
            canvasRenderer21.cull = false;

            CanvasRenderer canvasRenderer22 = rightButton.GetComponent<CanvasRenderer>();
            canvasRenderer22.cull = false;

            CanvasRenderer canvasRenderer23 = leftButton.GetComponent<CanvasRenderer>();
            canvasRenderer23.cull = false;

            CanvasRenderer canvasRenderer24 = bgTransparentButton.GetComponent<CanvasRenderer>();
            canvasRenderer24.cull = false;

            CanvasRenderer canvasRenderer25 = redButton.GetComponent<CanvasRenderer>();
            canvasRenderer25.cull = false;

            CanvasRenderer canvasRenderer26 = greentButton.GetComponent<CanvasRenderer>();
            canvasRenderer26.cull = false;

            CanvasRenderer canvasRenderer27 = startText.GetComponent<CanvasRenderer>();
            canvasRenderer27.cull = false;

            CanvasRenderer canvasRenderer28 = endText.GetComponent<CanvasRenderer>();
            canvasRenderer28.cull = false;
        }        
    }

    void TaskOnClick()
    {
        //Debug.Log("You have clicked the buttonCircle!");



        //disableCanvas.SetActive(true);
        //GameObject.Find("ButtonX").transform.localScale = new Vector3(1, (float)2.9, 1);
        //GameObject.Find("DraggableSlots").transform.localScale = new Vector3(1, 1, 1);
        //GameObject.Find("ButtonCircle").transform.localScale = new Vector3(0, 0, 0);
        //CanvasRenderer canvasRenderer = GetComponent<CanvasRenderer>();
        //yourCanvas = canvasRenderer;
        // yourCanvas.cull = true;
        //GetComponent<CanvasRenderer>().cull = true;

        if (clicked == true)
        {
            clicked = false;
            return;
        }
        else if(clicked == false)
        {
            clicked = true;
            return;
        }


    }
}
