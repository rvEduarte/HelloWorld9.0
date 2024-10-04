using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class CraneConsole : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    [SerializeField]private GameObject ComputerPanel, OutputPanel, platform1;
    public TMP_Text OutputText;
    public TMP_InputField inputfield;
    private string input1;

    private void Start()
    {
        inputfield.onValueChanged.AddListener(PassingValue);
        OutputPanel.SetActive(false);
    }
    void PassingValue(string input)
    {
        input1 = inputfield.text;
        
    }

    public void RunButton()
    {
        Debug.LogError("PUMASOK");
        Debug.Log(input1);

        if (string.IsNullOrEmpty(input1))
        {
            OutputPanel.SetActive(true);
            OutputText.text = "Error! Invalid expression term ';' \n\nHint: Use Boolean value only";
            return;
        }

        string trimmedInput = input1.Trim(); // Only trim after confirming input1 is not null

        if (trimmedInput == "true")
        {
            Debug.Log("TRUE");
            OutputPanel.SetActive(true);
            OutputText.text = "MoveCraneHeight: 10 \n\n<color=#03960f>...Program finished with exit code 0</color>";
            MoveCraneOne();
        }
        else if (trimmedInput == "false")
        {
            OutputPanel.SetActive(true);
            OutputText.text = "MoveCraneHeight: 20 \n\n<color=#03960f>...Program finished with exit code 0</color>";
            MoveCraneTwo();
        }
        else if (int.TryParse(trimmedInput, out _))
        {
            OutputPanel.SetActive(true);
            OutputText.text = "Cannot implicitly convert type 'int' to 'bool' \n\nHint: Use Boolean value only";
        }
        else if (double.TryParse(trimmedInput, out _))
        {
            OutputPanel.SetActive(true);
            OutputText.text = "Error! Cannot implicitly convert type `double` to `bool` \n\nHint: Use Boolean value only";
        }
        else
        {
            OutputPanel.SetActive(true);
            OutputText.text = "Error! The name '" + trimmedInput + "' does not exist in the current context \n\nHint: Use Boolean value only";
        }
    }

    private void MoveCraneOne()
    {
        StartCoroutine(MovePlatform(5.83f));
    }
    private void MoveCraneTwo()
    {
        StartCoroutine(MovePlatform(12.73f));
        
    }
    IEnumerator MovePlatform(float number)
    {
        vCam.Priority = 11;
        OutputPanel.SetActive(false);
        LeanTween.scale(ComputerPanel, Vector2.zero, 0.5f);
        yield return new WaitForSeconds(2f);
        LeanTween.moveY(platform1, number, 3f);

        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
        yield return new WaitForSeconds(1.5f);
        vCam.Priority = 0;
    }
}
