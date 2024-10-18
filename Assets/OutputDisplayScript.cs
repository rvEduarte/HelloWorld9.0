using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class OutputDisplayScript : MonoBehaviour
{
    public PlayerScoreScriptableObject playerData;

    public TMP_InputField codeInputField1;
    public TMP_InputField codeInputField2;
    public TMP_Text outputDisplay;

    private string currentOutput = ""; // To store the current output

    public GameObject platform;
    public GameObject CodePanel;

    private bool stopCounting;

    // Start is called before the first frame update
    void Start()
    {
        stopCounting = false;
        // Add listeners to the input fields to update display
        codeInputField1.onValueChanged.AddListener(OnCodeInputChanged);
        codeInputField2.onValueChanged.AddListener(OnCodeInputChanged);

        playerData.rawExercisePhase3 = 0;
    }

    void OnCodeInputChanged(string code)
    {
        // Clear currentOutput each time input is changed
        currentOutput = "";

        // Process each input field separately
        ProcessCode(codeInputField1.text);
        ProcessCode(codeInputField2.text);

        // Update the output display with the current result from both fields
        outputDisplay.text = currentOutput;
    }

    void ProcessCode(string code)
    {
        // Clear previous output for this input field
        string tempOutput = "";

        // Pattern to match Console.WriteLine or Console.Write with text inside quotes or a mathematical expression
        string pattern = @"Console\.Write(Line)?\(\s*(?<expr>""[^""]*""|[^""]+)\s*\);";
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
                tempOutput += isWriteLine ? outputText + "\n" : outputText;
            }
            else
            {
                // It's a potential mathematical expression, evaluate it
                try
                {
                    string result = EvaluateExpression(expression).ToString();
                    tempOutput += isWriteLine ? result + "\n" : result;
                }
                catch (System.Exception ex)
                {
                    string error = ex.Message;
                    string pattern1 = @"\[(.*?)\]";
                    Match match1 = Regex.Match(error, pattern1);

                    if (match1.Success)
                    {
                        // Extract the value inside the square brackets
                        string result = match1.Groups[1].Value;
                        string errorMsg = "Error: The name " + "'" + result + "'" + " does not exist in the current context";
                        tempOutput += isWriteLine ? errorMsg + "\n" : errorMsg;
                    }
                }
            }
        }

        // Append the tempOutput for this field to the overall currentOutput
        currentOutput += tempOutput;
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
    }

    object EvaluateExpression(string expression)
    {
        // Use DataTable to evaluate the expression
        DataTable table = new DataTable();
        var result = table.Compute(expression, string.Empty);
        return result;
    }

    public void OnDisplayButtonClick()
    {
        // Get input from both input fields
        string code1 = codeInputField1.text;
        string code2 = codeInputField2.text;

        // Conditions to check
        bool isCode1Valid = IsValidCode1(code1);
        bool isCode2Valid = IsValidCode2(code2);

        if (isCode1Valid && isCode2Valid)
        {
            // Output the current result        
            if(!stopCounting)
            {
                Debug.LogError("YES - IF");
                playerData.rawExercisePhase3 += 1;
                stopCounting = true;
            }
            outputDisplay.text = currentOutput;
            StartCoroutine(DisableCodePanel());
        }
        else
        {
            if (!stopCounting)
            {
                Debug.LogError("YES - ELSE");
                playerData.rawExercisePhase3 += 1;
            }
            outputDisplay.text = currentOutput + "\n\n <color=green>Follow the instruction Properly!</color> \n\n Click the <color=red>red Button</color> to type again!";
        }
    }

    IEnumerator DisableCodePanel()
    {
        yield return new WaitForSeconds(1.5f);

        ShowHideScript.stopMovement = true;  // ENABLE MOVEMENT
        LeanTween.scale(CodePanel, Vector2.zero, 0.5f);
        LeanTween.moveLocal(platform, new Vector3(38.32f, 53.77f, 0), 2.5f);
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
    }

    bool IsValidCode1(string code)
    {
        string pattern = @"Console\.Write\(\s*""I Love Coding!""\s*\);";
        return Regex.IsMatch(code, pattern);
    }

    bool IsValidCode2(string code)
    {
        // Pattern to match Console.WriteLine with a mathematical expression
        string pattern = @"Console\.WriteLine\(\s*(?<expr>[^""]+)\s*\);";
        Match match = Regex.Match(code, pattern);

        if (match.Success)
        {
            string expression = match.Groups["expr"].Value;
            try
            {
                // Evaluate the expression
                int result = (int)EvaluateExpression(expression);
                return result == 69;
            }
            catch
            {
                return false; // If evaluation fails, return false
            }
        }
        return false; // If pattern doesn't match, return false
    }

    public void ClearInputFields()
    {
        // Optionally clear input fields here
    }
}
