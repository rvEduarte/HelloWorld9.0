using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Data;
using UnityEditor.Rendering.LookDev;

public class TrialScript : MonoBehaviour
{
    public TMP_InputField codeInputField;
    public TMP_Text codeDisplay;
    public TMP_Text outputDisplay;
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
        ParseAndExecuteCode(code);
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
        //code = code.Replace("Console.WriteLine", "<color=purple>Console.WriteLine</color>");
        // code = code.Replace("Console.Write", "<color=purple>Console.Write</color>");

        // Add more rules as needed
        return code;
    }

    void ParseAndExecuteCode(string code)
    {
        // Clear previous output
        outputDisplay.text = "";

        // Print the input code for debugging
        Debug.Log("Input Code: " + code);

        // Pattern to match Console.WriteLine or Console.Write with text inside quotes or a mathematical expression
        string pattern = @"Console\.Write(Line)?\(\s*(?<expr>""[^""]*""|[^""]+)\s*\)";
        MatchCollection matches = Regex.Matches(code, pattern);

        // Check if matches were found
        if (matches.Count == 0)
        {
            Debug.Log("No matches found!");
        }

        foreach (Match match in matches)
        {
            string expression = match.Groups["expr"].Value;
            Debug.Log("Matched Expression: " + expression);

            if (expression.StartsWith("\"") && expression.EndsWith("\""))
            {
                // It's a string literal, remove quotes and print it
                string outputText = expression.Trim('"');
                outputDisplay.text += outputText + "\n";
                codeDisplay.text += "\n" + outputText; // Append to code display
            }
            else
            {
                // It's a potential mathematical expression, evaluate it
                try
                {
                    string result = EvaluateExpression(expression).ToString();
                    outputDisplay.text += result + "\n";
                    codeDisplay.text += "\n" + result; // Append to code display
                }
                catch (System.Exception ex)
                {
                    string errorMsg = "Error: " + ex.Message;
                    outputDisplay.text += errorMsg + "\n";
                    codeDisplay.text += "\n" + errorMsg; // Append error to code display
                }
            }
        }
    }
    object EvaluateExpression(string expression)
    {
        // Use DataTable to evaluate the expression
        DataTable table = new DataTable();
        var result = table.Compute(expression, string.Empty);
        return result;
    }
}