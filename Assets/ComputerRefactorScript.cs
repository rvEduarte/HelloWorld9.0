using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class ComputerRefactorScript : MonoBehaviour
{
    private const string HELLOWORLD = "HELLOWORLD";
    private const string SPACE_HELLOWORLD = "SPACE_HELLOWORLD";
    private const string SPACE_7 = "SPACE_7";
    private const string COLOR_CODE = "#18eded";
    private const string COLOR_CODE_FINISH = "#03960f";
    private const float COLLIDER_SIZE = 16.8f;
    private const float COLLIDER_OFFSET_X = -9;
    private const float COLLIDER_OFFSET_Y_TOP = -26.72f;
    private const float COLLIDER_OFFSET_Y_BOTTOM = -15.56f;
    private const float COLLIDER_BOTTOM_WIDTH = 8.63f;
    private const float COLLIDER_BOTTOM_HEIGHT = 9.63f;

    [SerializeField]
    private GameObject bulletHelloWrite;

    public TMP_Text canvasText;
    public BoxCollider2D disableBox2D;

    private string currentAction = null;
    public CounterCount counter;
    public GameObject laser1, laser2, laser3, blueAlarm;
    public BoolLaser boolLaser1, boolLaser2, boolLaser3;

    private string firstText, secondText, thirdText;

    private void Start()
    {
        SetCollider(disableBox2D, new Vector2(COLLIDER_OFFSET_X, 0), new Vector2(0.1f, COLLIDER_SIZE));
    }

    private void Update()
    {
        if (counter.counter == 1)
        {
            ProcessTextOutput(counter.counter, currentAction, ref firstText, laser1, boolLaser1);
        }
        else if (counter.counter == 2)
        {
            ProcessTextOutput(counter.counter, currentAction, ref secondText, laser2, boolLaser2);
        }
        else if (counter.counter == 3)
        {
            ProcessFinalOutput(currentAction);
        }
    }

    private void ProcessTextOutput(int count, string action, ref string text, GameObject laser, BoolLaser boolLaser)
    {
        Person myObj = new Person();

        if (action == HELLOWORLD || action == "7")
        {
            myObj.Name = action;
            text = myObj.Name;
            canvasText.text = firstText + "<color=" + COLOR_CODE + ">" + text + "</color>";
        }
        else if (action == SPACE_HELLOWORLD || action == SPACE_7)
        {
            myObj.Name = action == SPACE_HELLOWORLD ? HELLOWORLD + "\n" : "7\n";
            text = myObj.Name;
            canvasText.text = firstText + "<color=" + COLOR_CODE + ">" + text + "</color>";

            laser.SetActive(false);
            boolLaser.La1 = false;
        }
    }

    private void ProcessFinalOutput(string action)
    {
        Person myObj = new Person();

        myObj.Name2 = action == HELLOWORLD ? HELLOWORLD : "7";
        thirdText = myObj.Name2;

        string output = firstText + secondText + "<color=" + COLOR_CODE + ">" + thirdText + "</color>" +
                        "<color=" + COLOR_CODE_FINISH + ">\n\n\n...Program finished with exit code 0</color>";
        canvasText.text = output;

        laser1.SetActive(false);
        laser2.SetActive(false);
        laser3.SetActive(false);
        boolLaser1.La1 = false;
        boolLaser2.La2 = false;
        boolLaser3.La3 = false;

        SetCollider(disableBox2D, new Vector2(-2.2f, COLLIDER_OFFSET_Y_TOP), new Vector2(COLLIDER_BOTTOM_WIDTH, COLLIDER_BOTTOM_HEIGHT));

        if (boolLaser1.La1 == false && boolLaser2.La2 == false && boolLaser3.La3 == false)
        {
            SetCollider(disableBox2D, new Vector2(0, COLLIDER_OFFSET_Y_BOTTOM), new Vector2(1, 1));
        }
        else
        {
            blueAlarm.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentAction = collision.gameObject.tag switch
        {
            "WHELLO" => HELLOWORLD,
            "WLHELLO" => SPACE_HELLOWORLD,
            "W7" => "7",
            "WL7" => SPACE_7,
            _ => currentAction
        };

        if (currentAction != null)
        {
            counter.counter++;
            if (counter.counter == 4)
            {
                ProcessAlarmState();
            }
        }
    }

    private void ProcessAlarmState()
    {
        bool allLasersInactive = !boolLaser1.La1 && !boolLaser2.La2 && !boolLaser3.La3;

        if (!allLasersInactive)
        {
            blueAlarm.SetActive(true);
            SetCollider(disableBox2D, new Vector2(COLLIDER_OFFSET_X, 0), new Vector2(0.1f, COLLIDER_SIZE));

            laser1.SetActive(true);
            laser2.SetActive(true);
            laser3.SetActive(true);

            boolLaser1.La1 = true;
            boolLaser2.La2 = true;
            boolLaser3.La3 = true;

            canvasText.text = "__";
            counter.counter = 0;
        }
    }

    private void SetCollider(BoxCollider2D collider, Vector2 offset, Vector2 size)
    {
        collider.offset = offset;
        collider.size = size;
    }

    private class Person
    {
        public string Name { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
    }
}
