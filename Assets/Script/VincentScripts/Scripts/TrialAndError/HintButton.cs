using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintButton : MonoBehaviour
{
    public Button ButtonHint;
    public GameObject DisablePanel;

    public TMP_Text canvasText1;
    public TMP_Text canvasText2;
    public TMP_Text canvasText3;
    public TMP_Text canvasText4;

    public bool clicked = true;

    void Start()
    {
        DisablePanel.SetActive(false);

        Button btn = ButtonHint.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked == true)
        {
            DisablePanel.SetActive(false);
            //string color = "#18eded";
            canvasText1.text = "" + "Write" + "(\"HELLOWORLD\")";
            canvasText2.text = "" + "WriteLine" + "(\"HELLOWORLD\")";
            canvasText3.text = "" + "Write" + "( 4 + 3 )";
            canvasText4.text = "" + "WriteLine" + "( 4 + 3 )";

        }

        else if (clicked == false)
        {
            DisablePanel.SetActive(true);
            string color = "#FF0000";
            string color1 = "#0000FF";
            canvasText1.text = "" + "<color=" + color + ">Write</color>" + "(\"HELLOWORLD\")";
            canvasText2.text = "" + "<color=" + color1 + ">WriteLine</color>" + "(\"HELLOWORLD\")";
            canvasText3.text = "" + "<color=" + color + ">Write</color>" + "( 4 + 3 )";
            canvasText4.text = "" + "<color=" + color1 + ">WriteLine</color>" + "( 4 + 3 )";

        }
    }

    public void TaskOnClick()
    {
        if (clicked == true)
        {
            clicked = false;
            return;
        }
        else if (clicked == false)
        {
            clicked = true;
            return;
        }
    }
}
