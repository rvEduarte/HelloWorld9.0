using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrialDropPh2TWO : MonoBehaviour, IDropHandler
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
        platform1.SetActive(false);
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
            textLine8.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>Write</color>" + "(<color=#c44a3d>\"LL\"</color>);";
            output.SecondOutputPH2 = "Console.Write(\"LL\")";
        }
        else if (collision.gameObject.CompareTag("WriteLineUp"))
        {
            Debug.Log("WriteLineUp");
            textLine8.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>WriteLine</color>" + "(<color=#c44a3d>\"LL\"</color>);";
            output.SecondOutputPH2 = "Console.WriteLine(\"LL\")";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WriteUp"))
        {
            Debug.Log("Write");
            textLine8.text = "";
            output.SecondOutputPH2 = "";

            textOutput.text = "";
            outputPanel.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("WriteLineUp"))
        {
            Debug.Log("WriteLineUp");
            textLine8.text = "";
            output.SecondOutputPH2 = "";

            textOutput.text = "";
            outputPanel.SetActive(false);
        }
    }

    public void RunButton()
    {
        outputPanel.SetActive(true);
        if (output.FirstOutputPH2TWO == "Console.Write(\"HE\")" && output.SecondOutputPH2 == "Console.Write(\"LL\")")
        {
            Debug.Log("");
            onceZoom = true;
            textOutput.text = output.FirstRunOutputPH2 + "LL" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            output.SecondRunOutputPH2 = "LL";
            output.SecondOutputPH2TWO = "Console.Write(\"LL\")";
            line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>Write</color>" + "(<color=#c44a3d>\"LL\"</color>);";

            StartCoroutine(hidePanel(84.71f, 0.82f));
        }
        else if (output.FirstOutputPH2TWO == "Console.Write(\"HE\")" && output.SecondOutputPH2 == "Console.WriteLine(\"LL\")")
        {
            onceZoom = true;
            textOutput.text = output.FirstRunOutputPH2 + "LL" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            output.SecondRunOutputPH2 = "LL\n";
            output.SecondOutputPH2TWO = "Console.WriteLine(\"LL\")";
            line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>WriteLine</color>" + "(<color=#c44a3d>\"LL\"</color>);";

            StartCoroutine(hidePanel(84.71f, 0.82f));
        }
        else if (output.FirstOutputPH2TWO == "Console.WriteLine(\"HE\")" && output.SecondOutputPH2 == "Console.Write(\"LL\")")
        {
            onceZoom = true;
            textOutput.text = output.FirstRunOutputPH2 + "LL\n" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            output.SecondRunOutputPH2 = "LL";
            output.SecondOutputPH2TWO = "Console.Write(\"LL\")";
            line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>Write</color>" + "(<color=#c44a3d>\"LL\"</color>);";

            StartCoroutine(hidePanel(73.82f, -5.86f));
        }
        else if (output.FirstOutputPH2TWO == "Console.WriteLine(\"HE\")" && output.SecondOutputPH2 == "Console.WriteLine(\"LL\")")
        {
            onceZoom = true;
            textOutput.text = output.FirstRunOutputPH2 + "LL\n" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            output.SecondRunOutputPH2 = "LL\n";
            output.SecondOutputPH2TWO = "Console.WriteLine(\"LL\")";
            line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>WriteLine</color>" + "(<color=#c44a3d>\"LL\"</color>);";

            StartCoroutine(hidePanel(73.82f, - 5.86f));
        }
        else
        {
            textOutput.text = output.FirstRunOutputPH2;
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
    IEnumerator hidePanel(float numberX, float numberY)
    {
        yield return new WaitForSeconds(1);
        LeanTween.scale(computerPanel, Vector2.zero, 0.5f);

        StartCoroutine(MovePlatform(numberX, numberY));
    }
    IEnumerator MovePlatform(float numberX, float numberY)
    {
        yield return new WaitForSeconds(0.5f);
        platform1.SetActive(true);
        //LeanTween.moveY(platform1, numberY, 0.1f);
        //LeanTween.moveX(platform1, numberX, 0.1f);

        platform1.transform.position = new Vector3(numberX, numberY, platform1.transform.position.z);

        ComputerLevel1Ph1.disableInteract = true;
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
    }
}
