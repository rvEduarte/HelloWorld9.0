using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrialDropPh2THREE : MonoBehaviour, IDropHandler
{
    public CinemachineVirtualCamera vCam;
    public GameObject player;
    public GameObject movingObject1;
    public GameObject movingObject2;
    public GameObject movingObject3;

    public GameObject laserObject;
    public ScriptableOutput output;
    public TextMeshProUGUI textOutput;
    public TextMeshProUGUI textLine9;

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
            textLine9.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>Write</color>" + "(<color=#c44a3d>\"O\"</color>);";
            output.LastOutputPH2 = "Console.Write(\"O\")";
        }
        else if (collision.gameObject.CompareTag("WriteLineUp"))
        {
            Debug.Log("WriteLineUp");
            textLine9.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>WriteLine</color>" + "(<color=#c44a3d>\"O\"</color>);";
            output.LastOutputPH2 = "Console.WriteLine(\"O\")";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WriteUp"))
        {
            Debug.Log("Write");
            textLine9.text = "";
            output.LastOutputPH2 = "";

            textOutput.text = "";
            outputPanel.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("WriteLineUp"))
        {
            Debug.Log("WriteLineUp");
            textLine9.text = "";
            output.LastOutputPH2 = "";

            textOutput.text = "";
            outputPanel.SetActive(false);
        }
    }

    public void ResetButton()
    {
        player.transform.position = new Vector3(66.1f, 3.59f, platform1.transform.position.z);

        movingObject1.SetActive(false);
        movingObject2.SetActive(false);
        movingObject3.SetActive(false);

        LeanTween.scale(computerPanel, Vector2.zero, 0.5f);
        ComputerLevel1Ph1.disableInteract = true;
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping

    }
    public void RunButton()
    {
        outputPanel.SetActive(true);
        if (output.FirstOutputPH2TWO == "Console.Write(\"HE\")" && output.SecondOutputPH2TWO == "Console.Write(\"LL\")" && output.LastOutputPH2 == "Console.Write(\"O\")")
        {
            Debug.Log("");
            onceZoom = true;
            textOutput.text = output.FirstRunOutputPH2 + output.SecondRunOutputPH2 + "O" +"\n\n<color=#03960f>...Program finished with exit code 0</color>";
            //output.SecondRunOutputPH2 = "LL";
            //output.SecondOutputPH2TWO = "Console.Write(\"LL\")";
            //line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>Write</color>" + "(<color=#c44a3d>\"LL\"</color>);";

            StartCoroutine(hidePanel(92.47f, 0.8199998f));
        }
        else if (output.FirstOutputPH2TWO == "Console.Write(\"HE\")" && output.SecondOutputPH2TWO == "Console.Write(\"LL\")" && output.LastOutputPH2 == "Console.WriteLine(\"O\")")
        {
            Debug.Log("");
            onceZoom = true;
            textOutput.text = output.FirstRunOutputPH2 + output.SecondRunOutputPH2 + "O\n" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            //output.SecondRunOutputPH2 = "LL";
            //output.SecondOutputPH2TWO = "Console.Write(\"LL\")";
            //line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>Write</color>" + "(<color=#c44a3d>\"LL\"</color>);";

            StartCoroutine(hidePanel(92.47f, 0.8199998f));
        }
        else if (output.FirstOutputPH2TWO == "Console.Write(\"HE\")" && output.SecondOutputPH2TWO == "Console.WriteLine(\"LL\")" && output.LastOutputPH2 == "Console.Write(\"O\")")
        {
            onceZoom = true;
            textOutput.text = output.FirstRunOutputPH2 + output.SecondRunOutputPH2  + "O" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            //output.SecondRunOutputPH2 = "LL\n";
            //output.SecondOutputPH2TWO = "Console.WriteLine(\"LL\")";
            //line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>Write</color>" + "(<color=#c44a3d>\"LL\"</color>);";

            StartCoroutine(hidePanel(72.38f, -6.16f));
        }
        else if (output.FirstOutputPH2TWO == "Console.Write(\"HE\")" && output.SecondOutputPH2TWO == "Console.WriteLine(\"LL\")" && output.LastOutputPH2 == "Console.WriteLine(\"O\")")
        {
            onceZoom = true;
            textOutput.text = output.FirstRunOutputPH2 + output.SecondRunOutputPH2 + "O\n" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            //output.SecondRunOutputPH2 = "LL\n";
            //output.SecondOutputPH2TWO = "Console.WriteLine(\"LL\")";
            //line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>Write</color>" + "(<color=#c44a3d>\"LL\"</color>);";

            StartCoroutine(hidePanel(72.38f, -6.16f));
        }
        else if (output.FirstOutputPH2TWO == "Console.WriteLine(\"HE\")" && output.SecondOutputPH2TWO == "Console.Write(\"LL\")" && output.LastOutputPH2 == "Console.Write(\"O\")")
        {
            onceZoom = true;
            textOutput.text = output.FirstRunOutputPH2 + output.SecondRunOutputPH2 + "O" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            //output.SecondRunOutputPH2 = "LL";
            //output.SecondOutputPH2TWO = "Console.Write(\"LL\")";
            //line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>WriteLine</color>" + "(<color=#c44a3d>\"LL\"</color>);";

            StartCoroutine(hidePanel(81.85f, -5.97f));
        }
        else if (output.FirstOutputPH2TWO == "Console.WriteLine(\"HE\")" && output.SecondOutputPH2TWO == "Console.Write(\"LL\")" && output.LastOutputPH2 == "Console.WriteLine(\"O\")")
        {
            onceZoom = true;
            textOutput.text = output.FirstRunOutputPH2 + output.SecondRunOutputPH2 + "O\n" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            //output.SecondRunOutputPH2 = "LL";
            //output.SecondOutputPH2TWO = "Console.Write(\"LL\")";
            //line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>WriteLine</color>" + "(<color=#c44a3d>\"LL\"</color>);";

            StartCoroutine(hidePanel(81.85f, -5.97f));
        }
        else if (output.FirstOutputPH2TWO == "Console.WriteLine(\"HE\")" && output.SecondOutputPH2TWO == "Console.WriteLine(\"LL\")" && output.LastOutputPH2 == "Console.Write(\"O\")")
        {
            onceZoom = true;
            textOutput.text = output.FirstRunOutputPH2 + output.SecondRunOutputPH2 + "O" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            //output.SecondRunOutputPH2 = "LL\n";
            //output.SecondOutputPH2TWO = "Console.WriteLine(\"LL\")";
            //line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>WriteLine</color>" + "(<color=#c44a3d>\"LL\"</color>);";

            StartCoroutine(hidePanel(72.43f, -12.85f));
        }
        else if (output.FirstOutputPH2TWO == "Console.WriteLine(\"HE\")" && output.SecondOutputPH2TWO == "Console.WriteLine(\"LL\")" && output.LastOutputPH2 == "Console.WriteLine(\"O\")")
        {
            onceZoom = true;
            textOutput.text = output.FirstRunOutputPH2 + output.SecondRunOutputPH2 + "O\n" + "\n\n<color=#03960f>...Program finished with exit code 0</color>";
            //output.SecondRunOutputPH2 = "LL\n";
            //output.SecondOutputPH2TWO = "Console.WriteLine(\"LL\")";
            //line8Hint.text = "<color=#05f711>Console</color>" + "." + "<color=#fcdc5d>WriteLine</color>" + "(<color=#c44a3d>\"LL\"</color>);";

            StartCoroutine(hidePanel(72.43f, -12.85f));
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

        vCam.Priority = 12;
        laserObject.SetActive(false);
        platform1.transform.position = new Vector3(numberX, numberY, platform1.transform.position.z);
        StartCoroutine(BackCamera());


    }
    IEnumerator BackCamera()
    {
        yield return new WaitForSeconds(5f);
        vCam.Priority = 0;
        ComputerLevel1Ph1.disableInteract = true;
        TriggerTutorial.disableMove = true; //enable Move
        TriggerTutorial.disableJump = false; //enable jumping
    }
}
