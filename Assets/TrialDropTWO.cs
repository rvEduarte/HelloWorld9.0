using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrialDropTWO : MonoBehaviour, IDropHandler
{
    public TextMeshProUGUI line8Hint;
    public ScriptableOutput output;
    public TextMeshProUGUI textOutput;
    public TextMeshProUGUI textLine7;
    public TextMeshProUGUI textLine8;

    public GameObject outputPanel;
    public GameObject computerPanel;

    public GameObject platform1;

    public bool onceZoom;
    private void Start()
    {
        onceZoom = false;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WriteUp"))
        {
            Debug.Log("Write");
            textLine8.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>Write</color>" + "(<color=#c44a3d>\"UP\"</color>);";
            output.SecondOutput = "Console.Write(\"UP\")";
        }
        else if (collision.gameObject.CompareTag("WriteDown"))
        {
            Debug.Log("Write");
            textLine8.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>Write</color>" + "(<color=#c44a3d>\"DOWN\"</color>);";
            output.SecondOutput = "Console.Write(\"DOWN\")";
        }
        else if (collision.gameObject.CompareTag("WriteLineUp"))
        {
            Debug.Log("WriteLineUp");
            textLine8.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>WriteLine</color>" + "(<color=#c44a3d>\"UP\"</color>);";
            output.SecondOutput = "Console.WriteLine(\"UP\")";
        }
        else if (collision.gameObject.CompareTag("WriteLineDown"))
        {
            Debug.Log("WriteLineDown");
            textLine8.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>WriteLine</color>" + "(<color=#c44a3d>\"DOWN\"</color>);";
            output.SecondOutput = "Console.WriteLine(\"DOWN\")";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WriteUp"))
        {
            Debug.Log("WriteUp");
            textLine8.text = "";
            output.SecondOutput = "";

            textOutput.text = "";
            outputPanel.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("WriteDown"))
        {
            Debug.Log("Write");
            textLine8.text = "";
            output.SecondOutput = "";

            textOutput.text = "";
            outputPanel.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("WriteLineUp"))
        {
            Debug.Log("WriteLineUp");
            textLine8.text = "";
            output.SecondOutput = "";

            textOutput.text = "";
            outputPanel.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("WriteLineDown"))
        {
            Debug.Log("WriteLineDown");
            textLine8.text = "";
            output.SecondOutput = "";

            textOutput.text = "";
            outputPanel.SetActive(false);
        }
    }

    public void RunButton()
    {
        outputPanel.SetActive(true);
        if (output.SecondOutput == "Console.Write(\"UP\")")
        {
            onceZoom = true;
            textOutput.text = output.FirstRunOutput + "UP" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            output.SecondRunOutput = "UP";
            line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>Write</color>" + "(<color=#c44a3d>\"UP\"</color>);";

            StartCoroutine(hidePanel(11.34f));
        }
        else if (output.SecondOutput == "Console.Write(\"DOWN\")")
        {
            onceZoom = true;
            textOutput.text = output.FirstRunOutput + "DOWN" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            output.SecondRunOutput = "DOWN";
            line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>Write</color>" + "(<color=#c44a3d>\"DOWN\"</color>);";

            StartCoroutine(hidePanel(-2.075f));
        }
        else if (output.SecondOutput == "Console.WriteLine(\"UP\")")
        {
            onceZoom = true;
            textOutput.text = output.FirstRunOutput + "UP\n" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            output.SecondRunOutput = "UP\n";
            line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>WriteLine</color>" + "(<color=#c44a3d>\"UP\"</color>);";

            StartCoroutine(hidePanel(11.34f));
        }
        else if (output.SecondOutput == "Console.WriteLine(\"DOWN\")")
        {
            onceZoom = true;
            textOutput.text = output.FirstRunOutput + "DOWN\n" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            output.SecondRunOutput = "DOWN\n";
            line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>WriteLine</color>" + "(<color=#c44a3d>\"DOWN\"</color>);";

            StartCoroutine(hidePanel(-2.075f));
        }
        else
        {
            textOutput.text = output.FirstRunOutput;
        }

        /*if (!onceZoom)
        {
            if (output.FirstOutput == "Console.Write(\"UP\")")
            {
                onceZoom = true;
                textOutput.text = "UP" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
                output.FirstRunOutput = "UP";

                StartCoroutine(hidePanel(3.43f));
            }
            else if (output.FirstOutput == "Console.Write(\"DOWN\")")
            {
                onceZoom = true;
                textOutput.text = "DOWN" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
                output.FirstRunOutput = "DOWN";

                StartCoroutine(hidePanel(-5.08f));
            }
            else if (output.FirstOutput == "Console.WriteLine(\"UP\")")
            {
                onceZoom = true;
                textOutput.text = "UP\n" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
                output.FirstRunOutput = "UP\n";

                StartCoroutine(hidePanel(3.43f));
            }
            else if (output.FirstOutput == "Console.WriteLine(\"DOWN\")")
            {
                onceZoom = true;
                textOutput.text = "DOWN\n" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
                output.FirstRunOutput = "DOWN\n";

                StartCoroutine(hidePanel(-5.08f));
            }
            else
            {
                textOutput.text = "<color=#03960f>ERROR!</color>";
            }
        }
        else
        {
            if (output.FirstOutput == "Console.Write(\"UP\")")
            {
                textOutput.text = "UP" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
                output.FirstRunOutput = "UP";

                LeanTween.moveY(platform1, 3.43f, 0.5f);
            }
            else if (output.FirstOutput == "Console.Write(\"DOWN\")")
            {
                textOutput.text = "DOWN" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
                output.FirstRunOutput = "DOWN";

                LeanTween.moveY(platform1, -5.08f, 0.5f);
            }
            else if (output.FirstOutput == "Console.WriteLine(\"UP\")")
            {
                textOutput.text = "UP\n" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
                output.FirstRunOutput = "UP\n";

                
            }
            else if (output.FirstOutput == "Console.WriteLine(\"DOWN\")")
            {
                textOutput.text = "DOWN\n" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
                output.FirstRunOutput = "DOWN\n";

                LeanTween.moveY(platform1, -5.08f, 0.5f);
            }
            else
            {
                textOutput.text = "<color=#03960f>ERROR!</color>";
            }
        }*/
    }
    IEnumerator hidePanel(float number)
    {
        yield return new WaitForSeconds(1);
        LeanTween.scale(computerPanel, Vector2.zero, 0.5f);

        StartCoroutine(MovePlatform(number));
    }
    IEnumerator MovePlatform(float number)
    {
        ShowHideScript.stopMovement = true;
        yield return new WaitForSeconds(0.5f);
        LeanTween.moveY(platform1, number, 1.5f);

        ComputerLevel1Ph1.disableInteract = true;
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
    }
}
