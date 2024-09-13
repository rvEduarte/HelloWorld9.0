using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class TrialButtonCircle : MonoBehaviour
{
    public Button backpackButton;
    public Button tutorialButton;

    public Button yourButton;

    public Image border;

    public Image slotDrop;

    public Image drag1;
    public Image drag2;
    public Image drag3;
    public Image drag4;

    public Image draggableSlots;

    public Image hintPanel;
    public Image hintButton;

    //public Image bgBorder;
    public Image moveButton;
    public Image rightButton1;
    public Image leftButton1;
    public Image transparentButton;
    public Image redButton1;
    public Image greenButton1;

    public CanvasRenderer yourString;
    //public CanvasRenderer yourX;

    //public CanvasRenderer yourText1;
    public CanvasRenderer yourText2;
    public CanvasRenderer yourText3;
    public CanvasRenderer yourText4;
    public CanvasRenderer yourText5;
    public CanvasRenderer yourText6;

    public bool clicked = true;
   // public bool condition;

    public CanvasRenderer rightFire;
    public CanvasRenderer hintText2;
    public CanvasRenderer startText;
    //public CanvasRenderer endText;

    void Start()
    {
        // Cull all objects at the start
        CullAllObjects(false);
        SetImageVisibility(false);

        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        // Toggle visibility based on the 'clicked' state
        if (clicked)
        {
            CullAllObjects(false); // Show objects
            SetImageVisibility(true);
            backpackButton.enabled = false;
            tutorialButton.enabled = false;

        }
        else
        {
            CullAllObjects(true); // Hide objects
            SetImageVisibility(false);
            backpackButton.enabled = true;
            tutorialButton.enabled = true;
        }
    }

    void TaskOnClick()
    {
        // Toggle the 'clicked' state
        clicked = !clicked;
    }

    void CullAllObjects(bool shouldCull)
    {
        yourString.cull = shouldCull;

        yourText2.cull = shouldCull;
        yourText3.cull = shouldCull;
        yourText4.cull = shouldCull;
        yourText5.cull = shouldCull;
        yourText6.cull = shouldCull;

        rightFire.cull = shouldCull;

        hintText2.cull = shouldCull;
        startText.cull = shouldCull;
        //endText.cull = shouldCull;
    }

    void SetImageVisibility(bool isVisible)
    {
        // Using `enabled` to show or hide the images
        border.enabled = isVisible;
        drag1.enabled = isVisible;
        drag2.enabled = isVisible;
        drag3.enabled = isVisible;
        drag4.enabled = isVisible;
        slotDrop.enabled = isVisible;
        draggableSlots.enabled = isVisible;
        hintPanel.enabled = isVisible;
        hintButton.enabled = isVisible;
        //bgBorder.enabled = isVisible;
        moveButton.enabled = isVisible;
        rightButton1.enabled = isVisible;
        leftButton1.enabled = isVisible;
        transparentButton.enabled = isVisible;
        redButton1.enabled = isVisible;
        greenButton1.enabled = isVisible;
    }
}
