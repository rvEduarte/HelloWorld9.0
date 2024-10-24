using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartElseController : MonoBehaviour
{
    public ElsePlayerController _elsePlayerController;
    public TMP_Text buttonText;

    public Image button;

    public Sprite green, red;

    public GameObject belowRaycast, belowRaycast1;

    public static bool isStart;

    private bool state;

    private void Start()
    {
        isStart = false;
        state = false;
        buttonText.text = "Start";
        button.sprite = green;
    }

    public void ButtonClick()
    {
        if (state == false) // START
        {
            Debug.Log("START");
            state = true;
            isStart = true;
            buttonText.text = "Stop";
            button.sprite = red;
            belowRaycast.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba
            belowRaycast1.transform.localPosition = new Vector2(0.006f, -0.313f); // naka baba
            ThirdSlotScript.Row1Walk = true;
        }
        else               // STOP
        {
            Debug.Log("STOP");
            state = false;
            isStart = false;
            buttonText.text = "Start";
            button.sprite = green;
            belowRaycast.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba
            belowRaycast1.transform.localPosition = new Vector2(0.006f, 0.074f); // di naka baba
            ThirdSlotScript.Row1Walk = false;

            _elsePlayerController.OnLeftButtonUp();
            _elsePlayerController.OnRightButtonUp();
        }
    }
}
