using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeConsole : MonoBehaviour
{
    [SerializeField] private GameObject ComputerPanel;
    public TMP_Text timeLine;
    public TMP_InputField firstInputFieldIF;
    public TMP_InputField secondInputFieldIF;
    public TMP_InputField firstInputFieldELSEIF;
    public TMP_InputField secondInputFieldELSEIF;

    private int _firstInput, _secondInput, _thirdInput, _fourthInput;
    private string input1, input2, input3, input4;

    [Header("TimeConsoleObjects")]
    public GameObject bridgeLeft, bridgeRight, tilemapPresent,tilemapFuture, tilemapPast, flooring, batteryFuture, batteryPast; 

    private void Start()
    {
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
        _firstInput = int.Parse(input1);
        _secondInput = int.Parse(input2);
        _thirdInput = int.Parse(input3);
        _fourthInput = int.Parse(input4);

        if (_firstInput > _secondInput)
        {
            Debug.Log("if statement");
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
            LeanTween.scale(ComputerPanel, Vector2.zero, 0.5f);
            TriggerTutorial.disableMove = true; //enable Move
            TriggerTutorial.disableJump = false; //enable jumping

            batteryFuture.SetActive(true);

        }
        else if (_thirdInput < _fourthInput)
        {
            Debug.Log("else if statement");
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
            LeanTween.scale(ComputerPanel, Vector2.zero, 0.5f);
            TriggerTutorial.disableMove = true; //enable Move
            TriggerTutorial.disableJump = false; //enable jumping

            batteryPast.SetActive(true);
        }
        else
        {
            Debug.Log("else statement");
            timeLine.text = "Timeline: PRESENT";
            bridgeLeft.transform.localPosition = new Vector2(46.12f, -4.21f);
            SetRotation(bridgeLeft,0, -14.04f);
            bridgeRight.transform.localPosition = new Vector2(51.68f, -4.957f);
            SetRotation(bridgeRight, 0, 32.707f);

            flooring.transform.localPosition = new Vector2(60.85f, 3.0581f);
            SetRotation(flooring, 0, 0);

            tilemapPresent.SetActive(true);
            tilemapFuture.SetActive(false);
            tilemapPast.SetActive(false);
            LeanTween.scale(ComputerPanel, Vector2.zero, 0.5f);
            TriggerTutorial.disableMove = true; //enable Move
            TriggerTutorial.disableJump = false; //enable jumping
        }
    }
    // Set specific Y and Z rotation values
    void SetRotation(GameObject platform,float newYRotation, float newZRotation)
    {
        // Set Y and Z rotation, keeping X the same
        platform.transform.eulerAngles = new Vector3(transform.eulerAngles.x, newYRotation, newZRotation);
    }
}
