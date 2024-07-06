using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class OutputDisplayScript : MonoBehaviour
{
    public TMP_InputField codeInputField;
    public TMP_Text outputDisplay;

    private string currentOutput = ""; // To store the current output

    // Start is called before the first frame update
    void Start()
    {

        codeInputField.onValueChanged.AddListener(UpdateCodeDisplay);
    }

    void UpdateCodeDisplay(string code)
    {
        
        codeInputField.text = code;

        // Parse and execute the code for console output
        ParseAndExecuteCode(code);
    }
    void ParseAndExecuteCode(string code)
    {
        // Clear previous output
        currentOutput = ""; // Clear current output string
        //outputDisplay.text = "";

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
            bool isWriteLine = match.Groups[1].Success;
            Debug.Log("Matched Expression: " + expression);

            if (expression.StartsWith("\"") && expression.EndsWith("\""))
            {
                // It's a string literal, remove quotes
                string outputText = expression.Trim('"');
                AppendOutput(outputText, isWriteLine);
            }
            else
            {
                // It's a potential mathematical expression, evaluate it
                try
                {
                    string result = EvaluateExpression(expression).ToString();
                    AppendOutput(result, isWriteLine);
                }
                catch (System.Exception ex)
                {
                    string errorMsg = "Error: " + ex.Message;
                    AppendOutput(errorMsg, isWriteLine);
                }
            }
        }

        //codeDisplay.text = codeInputField.text;
        // Update the code display
        //codeDisplay.text = ApplySyntaxHighlighting(currentOutput); // Apply syntax highlighting
    }

    void AppendOutput(string output, bool isWriteLine)
    {
        if (isWriteLine)
        {
            currentOutput += output + "\n";
        }
        else
        {
            currentOutput += output;
        }
        outputDisplay.text = currentOutput; // Update the output display
    }

    object EvaluateExpression(string expression)
    {
        // Use DataTable to evaluate the expression
        DataTable table = new DataTable();
        var result = table.Compute(expression, string.Empty);
        return result;
    }
}
