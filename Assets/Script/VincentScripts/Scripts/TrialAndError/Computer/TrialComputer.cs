using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Drawing;

public class TrialComputer : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletHelloWrite;
    public TMP_Text canvasText;

    public BoxCollider2D disableBox2d;

    string a = null;

    public CounterCount Counter1;

    public GameObject Laser1;
    public GameObject Laser2;
    public GameObject Laser3;

    public GameObject BlueAlarm;

    public BoolLaser BoolLaser1;
    public BoolLaser BoolLaser2;
    public BoolLaser BoolLaser3;

    string firstText = null;
    string secondText = null;
    string thirdText = null;

    [Header("PH2 Accuracy")]
    [SerializeField] int getAccuracy;

    // Start is called before the first frame update
    void Start()
    {
        disableBox2d.GetComponent<BoxCollider2D>().offset = new Vector2 (-9, 0);
        disableBox2d.GetComponent<BoxCollider2D>().size = new Vector2((float)0.1, (float)16.8);
        PlayerPrefs.DeleteKey("accuracy_beginnerLevel1Ph2");

        PlayerPrefs.SetInt("accuracy_beginnerLevel1Ph2", 1);
    }

    // Update is called once per frame
    void Update()
    {
        Person myObj = new Person();
        

        if (Counter1.counter == 1)
        {
            Debug.Log("WLHELLO_COUNTER3");
            //canvasText.text = helloWriteLine;
            //counter++;

            if(a == "HELLOWORLD")
            {
                myObj.Name = "HELLOWORLD";
                string color = "#18eded";
                canvasText.text = "" + "<color=" + color + ">" + myObj.Name + "</color>";
                firstText = myObj.Name;

                Laser1.SetActive(false);
                BoolLaser1.La1 = false;
            }

            else if(a == "7")
            {
                myObj.Name = "7";
                string color = "#18eded";
                canvasText.text = "" + "<color=" + color + ">" + myObj.Name + "</color>";
                firstText = myObj.Name;
            }

            else if(a == "SPACE_HELLOWORLD")
            {
                myObj.Name = "HELLOWORLD\n";
                string color = "#18eded";
                canvasText.text = "" + "<color=" + color + ">" + myObj.Name + "</color>";
                firstText = myObj.Name;


                //TITE = "L1";
            }

            else if (a == "SPACE_7")
            {
                myObj.Name = "7\n";
                string color = "#18eded";
                canvasText.text = "" + "<color=" + color + ">" + myObj.Name + "</color>";
                firstText = myObj.Name;
            }
        }
        if (Counter1.counter == 2)
        {
            Debug.Log("WLHELLO_COUNTER2");
            if (a == "HELLOWORLD")
            {
                Debug.Log("KARL_TITE");
                myObj.Name1 = "HELLOWORLD";
                secondText = myObj.Name1;
                //canvasText.color = Color.red;
                string color = "#18eded";
                canvasText.text = "" + firstText + "<color=" + color + ">" + secondText + "</color>";
                //canvasText.text = "" + TITE + "<color=" + color + ">HELLOWORLD</color>";
            }

            else if(a == "7")
            {
                myObj.Name1 = "7";
                secondText = myObj.Name1;
                //canvasText.color = Color.red;
                string color = "#18eded";
                canvasText.text = "" + firstText + "<color=" + color + ">" + secondText + "</color>";
            }

            else if (a == "SPACE_HELLOWORLD")
            {
                myObj.Name1 = "HELLOWORLD\n";
                secondText = myObj.Name1;
                //canvasText.color = Color.red;
                string color = "#18eded";
                canvasText.text = "" + firstText + "<color=" + color + ">" + secondText + "</color>";

                Laser2.SetActive(false);
                BoolLaser2.La2 = false;
            }

            else if (a == "SPACE_7")
            {
                myObj.Name1 = "7\n";
                secondText = myObj.Name1;
                //canvasText.color = Color.red;
                string color = "#18eded";
                canvasText.text = "" + firstText + "<color=" + color + ">" + secondText + "</color>";


                //PEPE = "L1";
            }
        }
        if (Counter1.counter == 3)
        {
            //disableBox2d.GetComponent<BoxCollider2D>().enabled = true;

            if (a == "HELLOWORLD")
            {
                myObj.Name2 = "HELLOWORLD";
                thirdText = myObj.Name2;
                //canvasText.color = Color.red;
                string color2 = "#03960f";
                string color = "#18eded";
                canvasText.text = "" + firstText + secondText + "<color=" + color + ">" + thirdText + "</color>" + "<color=" + color2 + ">\n\n\n...Program finished with exit code 0</color>";


                //TEPE = "L1";

                //MOVE THE COLLIDER TO BOTTOM
                disableBox2d.GetComponent<BoxCollider2D>().offset = new Vector2((float)-2.2, (float)-26.72);
                disableBox2d.GetComponent<BoxCollider2D>().size = new Vector2((float)8.63, (float)9.63);

                if(BoolLaser1.La1 == false && BoolLaser2.La2 == false && BoolLaser3.La3 == false)
               // if(TITE.Equals(PEPE) && PEPE.Equals(TEPE))
                {
                    Debug.Log("KANTOTERO");

                }
                else
                {
                    Debug.Log("HINDE__KANTOTERO");
                    BlueAlarm.SetActive(false);
                }
            }

            else if (a == "7")
            {
                myObj.Name2 = "7";
                thirdText = myObj.Name2;
                //canvasText.color = Color.red;
                string color2 = "#03960f";
                string color = "#18eded";
                canvasText.text = "" + firstText + secondText + "<color=" + color + ">" + thirdText + "</color>" + "<color=" + color2 + ">\n\n\n...Program finished with exit code 0</color>";

                Laser3.SetActive(false);
                BoolLaser3.La3 = false;

                //MOVE THE COLLIDER TO BOTTOM
                disableBox2d.GetComponent<BoxCollider2D>().offset = new Vector2((float)-2.2, (float)-26.72);
                disableBox2d.GetComponent<BoxCollider2D>().size = new Vector2((float)8.63, (float)9.63);

                if (BoolLaser1.La1 == false && BoolLaser2.La2 == false && BoolLaser3.La3 == false)
                // if(TITE.Equals(PEPE) && PEPE.Equals(TEPE))
                {
                    Debug.Log("KANTOTERO");
                    disableBox2d.GetComponent<BoxCollider2D>().offset = new Vector2(0, (float)-15.56);
                    disableBox2d.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);

                    getAccuracy = PlayerPrefs.GetInt("accuracy_beginnerLevel1Ph2");
                }
                else
                {
                    Debug.Log("HINDE__KANTOTERO");
                    BlueAlarm.SetActive(false);
                }
            }

            else if (a == "SPACE_HELLOWORLD")
            {
                myObj.Name2 = "HELLOWORLD\n";
                thirdText = myObj.Name2;
                //canvasText.color = Color.red;
                string color2 = "#03960f";
                string color = "#18eded";
                canvasText.text = "" + firstText + secondText + "<color=" + color + ">" + thirdText + "</color>" + "<color=" + color2 + ">\n\n\n...Program finished with exit code 0</color>";

                //MOVE THE COLLIDER TO BOTTOM
                disableBox2d.GetComponent<BoxCollider2D>().offset = new Vector2((float)-2.2, (float)-26.72);
                disableBox2d.GetComponent<BoxCollider2D>().size = new Vector2((float)8.63, (float)9.63);

                if (BoolLaser1.La1 == false && BoolLaser2.La2 == false && BoolLaser3.La3 == false)
                // if(TITE.Equals(PEPE) && PEPE.Equals(TEPE))
                {
                    Debug.Log("KANTOTERO");             
                }
                else
                {
                    Debug.Log("HINDE__KANTOTERO");
                    BlueAlarm.SetActive(false);
                }
            }
            else if (a == "SPACE_7")
            {
                myObj.Name2 = "7\n";
                thirdText = myObj.Name2;
                //canvasText.color = Color.red;
                string color2 = "#03960f";
                string color = "#18eded";
                canvasText.text = "" + firstText + secondText + "<color=" + color + ">" + thirdText + "</color>" + "<color=" + color2 + ">\n\n\n...Program finished with exit code 0</color>";

                //MOVE THE COLLIDER TO BOTTOM
                disableBox2d.GetComponent<BoxCollider2D>().offset = new Vector2((float)-2.2, (float)-26.72);
                disableBox2d.GetComponent<BoxCollider2D>().size = new Vector2((float)8.63, (float)9.63);

                if (BoolLaser1.La1 == false && BoolLaser2.La2 == false && BoolLaser3.La3 == false)
                // if(TITE.Equals(PEPE) && PEPE.Equals(TEPE))
                {
                    Debug.Log("KANTOTERO");
                }
                else
                {
                    Debug.Log("HINDE__KANTOTERO");
                    BlueAlarm.SetActive(false);
                }
            }      
        }
    }

    public void IncreaseAccuracy(string key, int increment)
    {
        int currentAccuracy = PlayerPrefs.GetInt(key, 0);
        int newAccuracy = currentAccuracy + increment;

        //Save EXERCISE ACCURACY VALUE
        PlayerPrefs.SetInt(key, newAccuracy);
        PlayerPrefs.Save();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTER BULLET TITE");
        //if the game object we collided with is not equal to the current game object (so anything other than itself)
        if (collision.gameObject.tag == "WHELLO")
        {
            Debug.Log("ENTER W__HELLO");
            a = "HELLOWORLD";
            Counter1.counter++;

            if (Counter1.counter == 4)
            {
                Debug.Log("SUPER_FUCK");

                if (BoolLaser1.La1 == false && BoolLaser2.La2 == false && BoolLaser3.La3 == false)
                // if(TITE.Equals(PEPE) && PEPE.Equals(TEPE))
                {
                    Debug.Log("KANTOTERO");
                }
                else
                {
                    Debug.Log("HINDE__KANTOTERO");
                    BlueAlarm.SetActive(true);

                    disableBox2d.GetComponent<BoxCollider2D>().offset = new Vector2(-9, 0);
                    disableBox2d.GetComponent<BoxCollider2D>().size = new Vector2((float)0.1, (float)16.8);

                    Laser1.SetActive(true);
                    Laser2.SetActive(true);
                    Laser3.SetActive(true);

                    BoolLaser1.La1 = true;
                    BoolLaser2.La2 = true;
                    BoolLaser3.La3 = true;

                    canvasText.text = "__";
                    Counter1.counter = 0;

                    IncreaseAccuracy("accuracy_beginnerLevel1Ph2", 1);
                    
                }            
            }       
        }
        else if(collision.gameObject.tag == "WLHELLO")
        {
            Debug.Log("ENTER WL__HELLO");
            a = "SPACE_HELLOWORLD";
            Counter1.counter++;

            if (Counter1.counter == 4)
            {
                Debug.Log("SUPER_FUCK");

                if (BoolLaser1.La1 == false && BoolLaser2.La2 == false && BoolLaser3.La3 == false)
                // if(TITE.Equals(PEPE) && PEPE.Equals(TEPE))
                {
                    Debug.Log("KANTOTERO");

                }
                else
                {
                    Debug.Log("HINDE__KANTOTERO");
                    BlueAlarm.SetActive(true);

                    disableBox2d.GetComponent<BoxCollider2D>().offset = new Vector2(-9, 0);
                    disableBox2d.GetComponent<BoxCollider2D>().size = new Vector2((float)0.1, (float)16.8);

                    Laser1.SetActive(true);
                    Laser2.SetActive(true);
                    Laser3.SetActive(true);

                    BoolLaser1.La1 = true;
                    BoolLaser2.La2 = true;
                    BoolLaser3.La3 = true;

                    canvasText.text = "__";
                    Counter1.counter = 0;

                    IncreaseAccuracy("accuracy_beginnerLevel1Ph2", 1);
                }
            }
        }

        else if (collision.gameObject.tag == "W7")
        {
            Debug.Log("ENTER W__7");
            a = "7";
            Counter1.counter++;

            if (Counter1.counter == 4)
            {
                Debug.Log("SUPER_FUCK");

                if (BoolLaser1.La1 == false && BoolLaser2.La2 == false && BoolLaser3.La3 == false)
               
                {
                    Debug.Log("KANTOTERO");

                    BlueAlarm.SetActive(true);

                    disableBox2d.GetComponent<BoxCollider2D>().offset = new Vector2(-9, 0);
                    disableBox2d.GetComponent<BoxCollider2D>().size = new Vector2((float)0.1, (float)16.8);

                    Laser1.SetActive(true);
                    Laser2.SetActive(true);
                    Laser3.SetActive(true);

                    BoolLaser1.La1 = true;
                    BoolLaser2.La2 = true;
                    BoolLaser3.La3 = true;

                    canvasText.text = "__";
                    Counter1.counter = 0;

                    IncreaseAccuracy("accuracy_beginnerLevel1Ph2", 1);
                }
                else
                {
                    Debug.Log("HINDE__KANTOTERO");
                    BlueAlarm.SetActive(true);

                    disableBox2d.GetComponent<BoxCollider2D>().offset = new Vector2(-9, 0);
                    disableBox2d.GetComponent<BoxCollider2D>().size = new Vector2((float)0.1, (float)16.8);

                    Laser1.SetActive(true);
                    Laser2.SetActive(true);
                    Laser3.SetActive(true);

                    BoolLaser1.La1 = true;
                    BoolLaser2.La2 = true;
                    BoolLaser3.La3 = true;

                    canvasText.text = "__";
                    Counter1.counter = 0;

                    IncreaseAccuracy("accuracy_beginnerLevel1Ph2", 1);

                }
            }
        }

        else if (collision.gameObject.tag == "WL7")
        {
            Debug.Log("ENTER W__7");
            a = "SPACE_7";
            Counter1.counter++;

            if (Counter1.counter == 4)
            {
                Debug.Log("SUPER_FUCK");

                if (BoolLaser1.La1 == false && BoolLaser2.La2 == false && BoolLaser3.La3 == false)
                // if(TITE.Equals(PEPE) && PEPE.Equals(TEPE))
                {
                    Debug.Log("KANTOTERO");

                }
                else
                {
                    Debug.Log("HINDE__KANTOTERO");
                    BlueAlarm.SetActive(true);

                    disableBox2d.GetComponent<BoxCollider2D>().offset = new Vector2(-9, 0);
                    disableBox2d.GetComponent<BoxCollider2D>().size = new Vector2((float)0.1, (float)16.8);

                    Laser1.SetActive(true);
                    Laser2.SetActive(true);
                    Laser3.SetActive(true);

                    BoolLaser1.La1 = true;
                    BoolLaser2.La2 = true;
                    BoolLaser3.La3 = true;

                    canvasText.text = "__";
                    Counter1.counter = 0;

                    IncreaseAccuracy("accuracy_beginnerLevel1Ph2", 1);
                }
            }
        }       
    }
}

class Person
{
    private string name; // field
    public string Name   // property
    {
        get { return name; }
        set { name = value; }
    }

    private string name1; // field
    public string Name1   // property
    {
        get { return name1; }
        set { name1 = value; }
    }
    private string name2;
    public string Name2   // property
    {
        get { return name2; }
        set { name2 = value; }
    }
}
