using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class InputDisplayScript : MonoBehaviour
{
    public TMP_Text codeDisplay;
    public TMP_InputField codeInputField1;
    public TMP_InputField codeInputField2;

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to the input fields to update display
        codeInputField1.onValueChanged.AddListener(OnCodeInputChanged);
        codeInputField2.onValueChanged.AddListener(OnCodeInputChanged);
    }

    void OnCodeInputChanged(string code)
    {
        // Get input from both input fields
        string code1 = codeInputField1.text;
        string code2 = codeInputField2.text;

        // Combine both codes for syntax highlighting
        string combinedCode = code1 + "\n" + code2;

        // Apply syntax highlighting
        combinedCode = ApplySyntaxHighlighting(combinedCode);

        // Update display
        codeDisplay.text = combinedCode;
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
        code = code.Replace("Console", "<color=#05f711>Console</color>");
        code = code.Replace("WriteLine", "<color=#fcdc5d>WriteLine</color>");
        code = code.Replace("Write", "<color=#fcdc5d>Write</color>");

        // Add more rules as needed
        return code;
    }
}
