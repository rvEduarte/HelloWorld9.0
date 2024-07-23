using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class OutputDisplayScript : MonoBehaviour
{
    public PlayerScoreScriptableObject playerData;

    public TMP_InputField codeInputField1;
    public TMP_InputField codeInputField2;
    public TMP_Text outputDisplay;

    private string currentOutput = ""; // To store the current output

    public GameObject movingObject;
    public GameObject CodePanel;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to the input fields to update display
        codeInputField1.onValueChanged.AddListener(OnCodeInputChanged);
        codeInputField2.onValueChanged.AddListener(OnCodeInputChanged);

        playerData.rawExercisePhase3 = 1;
    }

    void OnCodeInputChanged(string code)
    {
        // Get input from both input fields
        string code1 = codeInputField1.text;
        string code2 = codeInputField2.text;

        // Combine both codes for processing
        string combinedCode = code1 + "\n" + code2;

        // Parse and execute the combined code for console output
        ParseAndExecuteCode(combinedCode);
    }

    void ParseAndExecuteCode(string code)
    {
        // Clear previous output
        currentOutput = ""; // Clear current output string
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
        // outputDisplay.text = currentOutput; // Update the output display
    }

    object EvaluateExpression(string expression)
    {
        // Use DataTable to evaluate the expression
        DataTable table = new DataTable();
        var result = table.Compute(expression, string.Empty);
        return result;
    }
    public void IncreaseAccuracy(string key, int increment)
    {
        int currentAccuracy = PlayerPrefs.GetInt(key, 0);
        int newAccuracy = currentAccuracy + increment;

        //Save EXERCISE ACCURACY VALUE
        PlayerPrefs.SetInt(key, newAccuracy);
        PlayerPrefs.Save();
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
            // Update the output display with the current output
            
            Debug.Log("Conditions met. Output updated.");
            outputDisplay.text = currentOutput;
            StartCoroutine(DisableCodePanel());
        }
        else
        {
            playerData.rawExercisePhase3 += 1;
            outputDisplay.text = currentOutput;
        }
    }
    IEnumerator DisableCodePanel()
    {
        yield return new WaitForSeconds(1.5f);

        

        CodePanel.SetActive(false);
        movingObject.SetActive(true);
        player.SetActive(true);
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
        // Clear the input fields
        codeInputField1.text = "";
        codeInputField2.text = "";
        // Clear the current output
        currentOutput = "";
        outputDisplay.text = "Input fields cleared.";
    }
}
