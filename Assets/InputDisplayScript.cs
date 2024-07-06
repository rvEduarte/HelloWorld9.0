using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class InputDisplayScript : MonoBehaviour
{
    public TMP_Text codeDisplay;
    public TMP_InputField codeInputField;


    // Start is called before the first frame update
    void Start()
    {
        // Add listener to the input field to update display
        codeInputField.onValueChanged.AddListener(UpdateCodeDisplay);
    }

    void UpdateCodeDisplay(string code)
    {
        // Apply basic syntax highlighting
        code = ApplySyntaxHighlighting(code);
        codeDisplay.text = code;

        // Parse and execute the code for console output
        //ParseAndExecuteCode(code);
    }

    string ApplySyntaxHighlighting(string code)
    {
        // Example: Highlight keywords, comments, strings
        code = code.Replace("void", "<color=blue>void</color>");
        code = code.Replace("public", "<color=blue>public</color>");
        code = code.Replace("private", "<color=blue>private</color>");
        code = code.Replace("if", "<color=blue>if</color>");
        code = code.Replace("else", "<color=blue>else</color>");
        code = code.Replace("//", "<color=green>//</color>");
        code = code.Replace("Console.WriteLine", "<color=purple>Console.WriteLine</color>");
        code = code.Replace("Console.Write", "<color=purple>Console.Write</color>");

        // Add more rules as needed
        return code;
    }

    void ParseAndExecuteCode(string code)
    {

        codeDisplay.text = code;
    }
}
