using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class CraneConsole : MonoBehaviour
{
    [SerializeField]private GameObject ComputerPanel;
    public TMP_Text OutputText;
    public TMP_InputField inputfield;
    private string input1;

    private void Start()
    {
        inputfield.onValueChanged.AddListener(PassingValue);

    }
    void PassingValue(string input)
    {
        input1 = inputfield.text;
    }

    public void RunButton()
    {
        if (input1 == null)
        {

        }
        else if (input1 == "true")
        {

        }
        else if (input1 == "false")
        {

        }
    }
}
