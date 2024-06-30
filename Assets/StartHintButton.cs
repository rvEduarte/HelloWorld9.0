using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartHintButton : MonoBehaviour
{
    public Button Button;
    public bool isClicked = false;

    public hintController hintController;

    public TextMeshProUGUI hintText1;
    public TextMeshProUGUI hintText2;
    void Start()
    {
        Button btn = Button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TaskOnClick()
    {
        string value = "\n\n<color=red>Write</color> - is like typing words on a typewriter without pressing Enter. It just keeps adding words next to each other on the same line.";
        hintText1.text = value;
        string value2 = "C# PRINTING TEXT";
        hintText2.text = value2;
        hintController.number = 0;
        isClicked = true;

    }
}
