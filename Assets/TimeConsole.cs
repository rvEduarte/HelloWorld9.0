using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeConsole : MonoBehaviour
{
    [SerializeField] private GameObject ComputerPanel;
    public TMP_Text timeLine, OutputText;
    public TMP_InputField firstInputFieldIF, secondInputFieldIF, firstInputFieldELSEIF, secondInputFieldELSEIF;
    private double _firstInput, _secondInput, _thirdInput, _fourthInput;
    private string input1, input2, input3, input4;

    [Header("TimeConsoleObjects")]
    public GameObject bridgeLeft, bridgeRight, tilemapPresent,tilemapFuture, tilemapPast, flooring, batteryFuture, batteryPast, computer, OutputPanel; 

    private void Start() 
    {
        OutputPanel.SetActive(false);
        firstInputFieldIF.onValueChanged.AddListener(PassingValue);
        secondInputFieldIF.onValueChanged.AddListener(PassingValue);
        firstInputFieldELSEIF.onValueChanged.AddListener(PassingValue);
        secondInputFieldELSEIF.onValueChanged.AddListener(PassingValue);
    }

    void PassingValue(string input)
    {
        input1 = firstInputFieldIF.text;
        input2 = secondInputFieldIF.text;
        input3 = firstInputFieldELSEIF.text;
        input4 = secondInputFieldELSEIF.text;

        Debug.Log(input1);
        Debug.Log(input2);
    }

    public void RunButton()
    {
        // Check for null or empty input1 and input2
        if ((string.IsNullOrEmpty(input1) || string.IsNullOrEmpty(input2)) && (string.IsNullOrEmpty(input3) || string.IsNullOrEmpty(input4)))
        {
            OutputPanel.SetActive(true);
            Debug.Log("Input1 or Input2 and Input3 or Input 4 is empty or null.");
            OutputText.text = "Line 7: Invalid expression term ' > ' \nLine 12: Invalid expression term ' < ' \n\nHint: Please FILL the required fields";
            return;
        }
        else if (string.IsNullOrEmpty(input1) || string.IsNullOrEmpty(input2))
        {
            OutputPanel.SetActive(true);
            Debug.Log("Input1 or Input2 is empty or null.");
            OutputText.text = "Line 7: Invalid expression term ' > ' \n\nHint: Please FILL the required fields";
            return;
        }
        // Check for null or empty input3 and input4
        else if (string.IsNullOrEmpty(input3) || string.IsNullOrEmpty(input4))
        {
            OutputPanel.SetActive(true);
            Debug.Log("Input3 or Input4 is empty or null.");
            OutputText.text = "Line 12: Invalid expression term ' < ' \n\nHint: Please FILL the required fields";
            return;
        }
        _firstInput = int.Parse(input1);
        _secondInput = int.Parse(input2);
        _thirdInput = int.Parse(input3);
        _fourthInput = int.Parse(input4);

        if (_firstInput > _secondInput)
        {
            Debug.Log("if statement");
            OutputPanel.SetActive(true);
            OutputText.text = "\"BackToFuture\"\n\n<color=#03960f>...Program finished with exit code 0</color>";
            FutureEvent();

        }
        else if (_thirdInput < _fourthInput)
        {
            Debug.Log("else if statement");
            OutputPanel.SetActive(true);
            OutputText.text = "\"BackToPast\"\n\n<color=#03960f>...Program finished with exit code 0</color>";
            PastEvent();
        }
        else
        {
            Debug.Log("else statement");
            OutputPanel.SetActive(true);
            OutputText.text = "\"BackToPresent\"\n\n<color=#03960f>...Program finished with exit code 0</color>";
            PresentEvent();
        }
    }
    private IEnumerator DelayHidePanel()
    {
        yield return new WaitForSeconds(2);
        LeanTween.scale(ComputerPanel, Vector2.zero, 0.5f);
        OutputPanel.SetActive(false);
        OutputText.text = "";
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping

        firstInputFieldIF.enabled = false;
        secondInputFieldIF.enabled = false;
        firstInputFieldELSEIF.enabled = false;
        secondInputFieldELSEIF.enabled = false;
    }

    private void FutureEvent()
    {
        timeLine.text = "Timeline: FUTURE";
        bridgeLeft.transform.localPosition = new Vector2(45.955f, -4.921f);
        SetRotation(bridgeLeft, 0, -31.7f);
        bridgeRight.transform.localPosition = new Vector2(51.68f, -4.957f);
        SetRotation(bridgeRight, 0, 32.707f);

        flooring.transform.localPosition = new Vector2(60.43f, 3.03f);
        SetRotation(flooring, 0, 46.931f);

        tilemapPast.SetActive(false);
        tilemapFuture.SetActive(true);
        tilemapPresent.SetActive(false);
        batteryFuture.SetActive(true);
        batteryPast.SetActive(false);
        computer.SetActive(false);

        StartCoroutine(DelayHidePanel());
    }
    private void PastEvent()
    {
        timeLine.text = "Timeline: PAST";
        bridgeLeft.transform.localPosition = new Vector2(46.2f, -3.573f);
        SetRotation(bridgeLeft, 0, 0);
        bridgeRight.transform.localPosition = new Vector2(51.381f, -3.58f);
        SetRotation(bridgeRight, 0, 0);

        flooring.transform.localPosition = new Vector2(60.85f, 3.0581f);
        SetRotation(flooring, 0, 0);

        tilemapPast.SetActive(true);
        tilemapFuture.SetActive(false);
        tilemapPresent.SetActive(false);
        batteryFuture.SetActive(false);
        batteryPast.SetActive(true);
        computer.SetActive(false);

        StartCoroutine(DelayHidePanel());
    }
    private void PresentEvent()
    {
        timeLine.text = "Timeline: PRESENT";
        bridgeLeft.transform.localPosition = new Vector2(46.12f, -4.21f);
        SetRotation(bridgeLeft, 0, -14.04f);
        bridgeRight.transform.localPosition = new Vector2(51.68f, -4.957f);
        SetRotation(bridgeRight, 0, 32.707f);

        flooring.transform.localPosition = new Vector2(60.85f, 3.0581f);
        SetRotation(flooring, 0, 0);

        tilemapPresent.SetActive(true);
        tilemapFuture.SetActive(false);
        tilemapPast.SetActive(false);
        batteryPast.SetActive(false);
        batteryFuture.SetActive(false);
        computer.SetActive(true);

        StartCoroutine(DelayHidePanel());
    }
    // Set specific Y and Z rotation values
    void SetRotation(GameObject platform,float newYRotation, float newZRotation)
    {
        // Set Y and Z rotation, keeping X the same
        platform.transform.eulerAngles = new Vector3(transform.eulerAngles.x, newYRotation, newZRotation);
    }
}
